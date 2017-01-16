FROM microsoft/aspnetcore:1.0
ENTRYPOINT ["dotnet", "Webshop.API.dll"]

ARG SOURCE=.
WORKDIR /app

EXPOSE 5672
COPY $SOURCE /app