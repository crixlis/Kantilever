FROM microsoft/aspnetcore:1.0
ENTRYPOINT ["dotnet", "Webshop.API.dll"]
ENV ASPNETCORE_URLS http://+:5000

ARG SOURCE=.
WORKDIR /app

EXPOSE 5000
COPY $SOURCE /app