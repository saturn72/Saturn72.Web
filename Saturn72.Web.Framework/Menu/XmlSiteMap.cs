using System;
using System.IO;
using System.Xml;
using Saturn72.Core;
using Saturn72.Core.Infrastructure;
using Saturn72.Core.Services.Localization;

namespace Saturn72.Web.Framework.Menu
{
    public class XmlSiteMap
    {
        public XmlSiteMap()
        {
            RootNode = new SiteMapNode();
        }

        public SiteMapNode RootNode { get; set; }

        public virtual void LoadFrom(string physicalPath)
        {
            var webHelper = EngineContext.Current.Resolve<IWebHelper>();
            var filePath = webHelper.MapPath(physicalPath);
            var content = File.ReadAllText(filePath);

            if (string.IsNullOrEmpty(content)) return;

            using (var sr = new StringReader(content))
            {
                using (var xr = XmlReader.Create(sr,
                    new XmlReaderSettings
                    {
                        CloseInput = true,
                        IgnoreWhitespace = true,
                        IgnoreComments = true,
                        IgnoreProcessingInstructions = true
                    }))
                {
                    var doc = new XmlDocument();
                    doc.Load(xr);

                    if ((doc.DocumentElement == null) || !doc.HasChildNodes) return;

                    var xmlRootNode = doc.DocumentElement.FirstChild;
                    Iterate(RootNode, xmlRootNode);
                }
            }
        }

        private static void Iterate(SiteMapNode siteMapNode, XmlNode xmlNode)
        {
            PopulateNode(siteMapNode, xmlNode);

            foreach (XmlNode xmlChildNode in xmlNode.ChildNodes)
            {
                if (xmlChildNode.LocalName.Equals("siteMapNode", StringComparison.InvariantCultureIgnoreCase))
                {
                    var siteMapChildNode = new SiteMapNode();
                    siteMapNode.ChildNodes.Add(siteMapChildNode);

                    Iterate(siteMapChildNode, xmlChildNode);
                }
            }
        }

        private static void PopulateNode(SiteMapNode siteMapNode, XmlNode xmlNode)
        {
            //title
            var saturn72Resource = GetStringValueFromAttribute(xmlNode, "saturn72Resource");
            if (!string.IsNullOrEmpty(saturn72Resource))
            {
                var localizationService = EngineContext.Current.Resolve<ILocalizationService>();
                siteMapNode.Title = localizationService.GetResource(saturn72Resource);
            }
            else
            {
                siteMapNode.Title = GetStringValueFromAttribute(xmlNode, "title");
            }

            //routes, url
            string controllerName = GetStringValueFromAttribute(xmlNode, "controller");
            string actionName = GetStringValueFromAttribute(xmlNode, "action");
            string url = GetStringValueFromAttribute(xmlNode, "url");
            if (!string.IsNullOrEmpty(controllerName) && !string.IsNullOrEmpty(actionName))
            {
                siteMapNode.ControllerName = controllerName;
                siteMapNode.ActionName = actionName;

                //apply admin area as described here - http://www.nopcommerce.com/boards/t/20478/broken-menus-in-admin-area-whilst-trying-to-make-a-plugin-admin-page.aspx
               // siteMapNode.RouteValues = new RouteValueDictionary { { "area", "Admin" } };
            }
            else if (!string.IsNullOrEmpty(url))
            {
                siteMapNode.Url = url;
            }

            //image URL
            siteMapNode.ImageUrl = GetStringValueFromAttribute(xmlNode, "ImageUrl");

            //permission name
            var permissionNames = GetStringValueFromAttribute(xmlNode, "PermissionNames");
            if (!string.IsNullOrEmpty(permissionNames))
            {
                //TODO: enabler this section after implementing permission service
                //var permissionService = EngineContext.Current.Resolve<IPermissionService>();
                //siteMapNode.Visible = permissionNames.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                //   .Any(permissionName => permissionService.Authorize(permissionName.Trim()));

                siteMapNode.Visible = true;
            }
            else
            {
                siteMapNode.Visible = true;
            }
        }

        private static string GetStringValueFromAttribute(XmlNode node, string attributeName)
        {
            string value = null;

            if (node.Attributes != null && node.Attributes.Count > 0)
            {
                var attribute = node.Attributes[attributeName];

                if (attribute != null)
                {
                    value = attribute.Value;
                }
            }

            return value;
        }
    }
}
