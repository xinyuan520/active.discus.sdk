FROM discus.base:latest
WORKDIR /app
EXPOSE 80


COPY /Release /app
ENTRYPOINT ["dotnet", "Discus.User.dll"]