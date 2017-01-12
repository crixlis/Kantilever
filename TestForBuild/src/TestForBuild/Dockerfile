FROM microsoft/dotnet:1.0.1-runtime
ENTRYPOINT ["dotnet", "TestForBuild.dll"]
ARG SOURCE=.
WORKDIR /app
EXPOSE 80
COPY $SOURCE /app