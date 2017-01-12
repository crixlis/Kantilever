FROM microsoft/aspnetcore:1.0.1
ENTRYPOINT ["dotnet", "WebApplication1.dll"]
ARG source=publish
WORKDIR /app
EXPOSE 1999:80
COPY $source .
