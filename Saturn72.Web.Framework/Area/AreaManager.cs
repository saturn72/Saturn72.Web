using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Web;
using System.Web.Compilation;
using Saturn72.Core.ComponentModel;
using Saturn72.Extensions;
using Saturn72.Web.Framework.Area;

[assembly: PreApplicationStartMethod(typeof (AreaManager), "Initialize")]
namespace Saturn72.Web.Framework.Area
{
    public class AreaManager
    {
        private static readonly ReaderWriterLockSlim Locker = new ReaderWriterLockSlim();
        private static AreaSettings _areaSettings;

        public static void Initialize()
        {
            _areaSettings = AreaSettings.LoadStartupSettings();

            using (new WriteLockDisposable(Locker))
            {
                PrepareFolders();
                var areaNames = IoHelper.GetDirectoryInfo(_areaSettings.MainAreaFolder).GetDirectories();
                areaNames.ForEachItem(DeployArea);
            }
        }

        protected static void DeployArea(DirectoryInfo dirInfo)
        {
            var binDir = new DirectoryInfo(Path.Combine(_areaSettings.ShadowCopyFolder, dirInfo.Name));
            IoHelper.CreateDirectoryIfNotExists(binDir.FullName);
            var areaFiles = dirInfo.GetFiles("*.dll", SearchOption.AllDirectories).ToList();
            var allCurrentBinFiles = binDir.GetFiles("*.dll", SearchOption.AllDirectories).ToList();

            areaFiles
                .Where(f => !allCurrentBinFiles.Select(x => x.FullName).Contains(f.FullName))
                .ForEachItem(f => IoHelper.CopyFile(f.FullName, binDir.FullName));

            allCurrentBinFiles = binDir.GetFiles("*.dll", SearchOption.AllDirectories).ToList();

            var mainAreaFile =
                allCurrentBinFiles.FirstOrDefault(x => x.Name.EndsWith(".Area.dll", StringComparison.OrdinalIgnoreCase));
            if (mainAreaFile.IsNull())
                return;
            PerformFileDeploy(mainAreaFile);
        }

        private static void PerformFileDeploy(FileInfo areaFile)
        {
            VerifyNotNullArea(areaFile);
            var shadowCopyArea = AspNetExt.GetAspNetDeploymentPath(areaFile, _areaSettings.ShadowCopyFolder);
            var shadowCopiedAssembly = Assembly.Load(AssemblyName.GetAssemblyName(shadowCopyArea.FullName));
            //add the reference to the build manager
            Debug.WriteLine("Adding to BuildManager: '{0}'", shadowCopiedAssembly.FullName);
            BuildManager.AddReferencedAssembly(shadowCopiedAssembly);
        }

        private static void VerifyNotNullArea(FileInfo area)
        {
            Guard.NotNull(area);
            Guard.NotNull(area.Directory);
            Guard.NotNull(area.Directory.Parent, () =>
            {
                var message = "The area directory for the {0} file exists in a folder outside of the allowed saturn72 folder heirarchy"
                    .AsFormat(area.Name);
                throw new InvalidOperationException(message);
            });
        }

        private static void PrepareFolders()
        {
            //shadow copy
            var shadowCopyFolder = _areaSettings.ShadowCopyFolder;
            if (!IoHelper.DirectoryExists(shadowCopyFolder))
                Trace.WriteLine("Could not find areas folder in path: " + shadowCopyFolder);
            IoHelper.CreateDirectoryIfNotExists(shadowCopyFolder);
            if (_areaSettings.ClearAreasDirectoryOnStartup)
            {
                var binFiles = IoHelper.GetDirectoryInfo(shadowCopyFolder)
                    .GetFiles("*", SearchOption.AllDirectories);
                //clear out shadow copied plugins
                foreach (var f in binFiles)
                {
                    Debug.WriteLine("Deleting " + f.Name);
                    try
                    {
                        File.Delete(f.FullName);
                    }
                    catch (Exception exc)
                    {
                        Debug.WriteLine("Error deleting file " + f.Name + ". Exception: " + exc);
                    }
                }
            }

            //Areas
            IoHelper.CreateDirectoryIfNotExists(_areaSettings.MainAreaFolder);
        }

    }
}