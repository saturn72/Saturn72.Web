
basePluginFolder = WScript.Arguments(0)
destPluginFolder = WScript.Arguments(1)
pluginFile = WScript.Arguments(2)

DeletePluginsBinFolder destPluginFolder
CopyPluginBinaries basePluginFolder, destPluginFolder, pluginFile

WScript.Quit 0

Public Function DeletePluginsBinFolder(pluginFolder)
	Set fso = CreateObject("Scripting.FileSystemObject")
	pathToDelete = fso.BuildPath(destPluginFolder, "bin")
	if(fso.FolderExists(pathToDelete)) Then fso.DeleteFolder pathToDelete, true
	if(fso.FolderExists(pluginFolder)) Then fso.DeleteFolder pluginFolder, true
End Function

Public Function CopyPluginBinaries(basePluginFolder, destPluginFolder, pluginFile)
	CreateFolderPath destPluginFolder
	'Get required plugins
	pluginNames = ReadPluginList(pluginFile)
	'Copy plugins to destination folder

	For Each plugin In pluginNames
		Set fso = CreateObject("Scripting.FileSystemObject")
		src = fso.BuildPath(basePluginFolder, plugin)
		dst = fso.BuildPath(destPluginFolder, plugin)	
		CopyFolder src, dst
	Next 

End Function

Public Function CopyFolder(strSource, strDest)
	Set fso = CreateObject("Scripting.FileSystemObject")
	
	If Not(fso.FolderExists(strSource)) Then 
		MsgBox "Cannot find folder " & strSource
		Exit Function 
	End If		
	fso.CopyFolder strSource, strDest, true
End Function

Public Function ReadPluginList(fileName)
	Const ForReading = 1	
	Set fso = CreateObject("Scripting.FileSystemObject")	
	'Check if file exist
	If Not (fso.FileExists(fileName)) Then 
		MsgBox "Could not find file " & fileName
		Exit Function 
	End If
	
	'Open file
	Set pluginFile = fso.OpenTextFile(fileName, ForReading)
	Set pluginNames = CreateObject("Scripting.Dictionary")
	key = 0
	
	Do Until pluginFile.AtEndOfStream
		line = pluginFile.ReadLine
		
		If line <> "" Then
			pluginNames.Add key,line
			key = key+1
		End If
	Loop
	ReadPluginList = pluginNames.Items
	
	'GC
	pluginFile.Close
End Function


Public Function CreateFolderPath(folderPath)
	Set fso = CreateObject("Scripting.FileSystemObject")
	
	If Not fso.FolderExists(folderPath) Then 
		CreateFolderPath fso.GetParentFolderName(folderPath)
		fso.CreateFolder folderPath
	End If
End Function