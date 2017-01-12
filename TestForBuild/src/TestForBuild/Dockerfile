FROM microsoft/dotnet:runtime
ENTRYPOINT ["dotnet", "TestForBuild.dll"]
ARG SOURCE=.
WORKDIR /app
EXPOSE 80
COPY $SOURCE /app