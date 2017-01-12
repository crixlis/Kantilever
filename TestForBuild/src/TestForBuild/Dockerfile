FROM microsoft/dotnet:runtime
ENTRYPOINT ["dotnet", "TestBuildWebApp.dll"]
ARG SOURCE=.
WORKDIR /app
EXPOSE 80
COPY $SOURCE /app