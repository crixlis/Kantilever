

dotnet publish -o bin/Publish
docker build --tag="kantilever/webshopbeheerfrontend:1.0" .
docker run -d -p 1200:5000 kantilever/webshopbeheerfrontend:1.0
