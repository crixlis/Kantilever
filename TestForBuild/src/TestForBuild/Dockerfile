FROM microsoft/aspnetcore:1.0.1
ENTRYPOINT ["dotnet", "TestBuildWebApp.dll"]
ARG SOURCE=.
WORKDIR /app
EXPOSE 80
COPY $SOURCE /app