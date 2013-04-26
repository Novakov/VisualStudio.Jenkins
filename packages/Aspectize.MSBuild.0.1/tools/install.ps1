param($installPath, $toolsPath, $package, $project)

$projectElement = [Microsoft.Build.Construction.ProjectRootElement]::Open($project.FileName)

$targetsFile = [System.IO.Path]::Combine($toolsPath, 'Aspectize.targets')

$projectUri = new-object Uri('file://' + $project.FullName)
$targetUri = new-object Uri('file://' + $targetsFile)
$relativePath = $projectUri.MakeRelativeUri($targetUri).ToString().Replace([System.IO.Path]::AltDirectorySeparatorChar, [System.IO.Path]::DirectorySeparatorChar)

$projectElement.AddImport($relativePath)

$projectElement.Save()