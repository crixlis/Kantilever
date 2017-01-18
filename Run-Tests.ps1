param(
    $rootURL
)

foreach ($folder in $(Get-ChildItem -Path $rootURL)) {
    if ($folder.Name -like "*.Test") {
        dotnet test -xml TEST-TestBuild.xml
    }
}
