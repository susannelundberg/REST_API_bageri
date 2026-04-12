## Hur man kör projektet

1. Klona repot:
   git clone https://github.com/susannelundberg/REST_API_bageri.git

2. Öppna projektet i VS Code

3. Skapa en "appsettings.json" baserat på "appsettings.Development.json"

4. Fyll i egen connectionstring till databasen

5. Skapa databasen via migrations:
   dotnet ef database update

6. Kör projektet:
   dotnet run
