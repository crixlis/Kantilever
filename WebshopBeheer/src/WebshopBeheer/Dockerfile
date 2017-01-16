FROM microsoft/aspnetcore:1.0
ENV HOME=/app
ENV ASPNETCORE_URLS http://+:5000
ARG SOURCE=.

WORKDIR $HOME
COPY $SOURCE /app

EXPOSE 5000

ENTRYPOINT ["dotnet", "WebshopBeheer.dll"]