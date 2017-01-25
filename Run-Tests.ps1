param(
    $rootURL
)

foreach ($folder in $(Get-ChildItem -Path $rootURL | Where-Object{ $_.PSIsContainer -and $_.Name -ne "Documentatie" -and $_.Name -ne "WebshopFrontend" })) {
    $source = Join-Path $folder "src"
    if(Test-Path $source) {
        foreach($item in $(Get-ChildItem -Path $source)) {
            if($item.Name -like "*.Test" ) {
                Set-Location $($item.FullName)
                dotnet test --no-build "$($item.FullName)" -xml "$($item.Name)-TestBuild.xml"
            }
        }
    }            
}    
