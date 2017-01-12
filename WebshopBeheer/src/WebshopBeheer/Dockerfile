FROM microsoft/aspnetcore:1.0

ENV HOME=/app
ENV ASPNETCORE_URLS http://+:5000

WORKDIR $HOME
COPY ./bin/Publish/ $HOME

EXPOSE 5000

ENTRYPOINT ["dotnet", "WebshopBeheer.dll"]
