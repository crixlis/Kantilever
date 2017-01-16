call dotnet publish -o $(Build.Repository.LocalPath)\WebshopBeheer\src\WebshopBeheer\bin\publish -c release
call dotnet publish -o .\BestelService\src\BestelService\bin\publish -c release
call dotnet publish -o .\WebshopAPI\src\Webshop.API\bin\publish -c release
call dotnet publish -o .\WebshopAPI\src\Webshop.Listener\bin\publish -c release
call dotnet publish -o .\FactuurService\src\FactuurService\bin\publish -c release
