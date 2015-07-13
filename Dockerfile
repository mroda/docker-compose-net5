FROM microsoft/aspnet:vs-1.0.0-beta4

COPY . /app
WORKDIR /app
RUN ["dnu", "restore"]

EXPOSE 5005
ENTRYPOINT ["dnx", "./src/EmptyMVC", "kestrel"]