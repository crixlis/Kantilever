FROM microsoft/dotnet:1.0.3-runtime
ENTRYPOINT ["dotnet", "Webshop.Listener.dll"]

ARG SOURCE=.
WORKDIR /app

EXPOSE 5672
COPY $SOURCE /app