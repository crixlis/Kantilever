cd %1\WebshopBeheer\src\WebshopBeheer
call dotnet publish -o .\bin\publish -c release
cd %1\BestelService\src\BestelService
call dotnet publish -o .\bin\publish -c release
cd %1\WebshopAPI\src\Webshop.API
call dotnet publish -o .\bin\publish -c release
cd %1\WebshopAPI\src\Webshop.Listener
call dotnet publish -o .\bin\publish -c release
cd %1\FactuurService\src\FactuurService
call dotnet publish -o .\bin\publish -c release

