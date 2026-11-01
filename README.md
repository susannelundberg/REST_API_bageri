# Bageri-API

Ett REST API för ett bageri, byggt som skolprojekt. API:et hanterar bageriets leverantörer, råvaror, försäljningsprodukter, kunder (butiker) och ordrar.
Projektet visar hur man designar ett REST API med relationsdatabas: entiteter med relationer, DTO:er, migrationer, seeddata och containerisering med Docker.

## Vad API:et gör

- **Leverantörer och råvaror:** Man kan lägga till leverantörer och produkter (råvaror). Kopplingen mellan dem, med pris per kg och artikelnummer, går att skapa och prisuppdatera. Man kan se vilka produkter en leverantör säljer och vilka leverantörer som säljer en viss produkt.
- **Försäljningsprodukter:** Bageriets färdiga produkter, med pris, vikt, antal per förpackning samt tillverknings- och bäst före-datum. Priset går att uppdatera.
- **Kunder:** Butiker som beställer av bageriet. Kundens beställningshistorik visas när man hämtar en kund, och kontaktpersonen går att uppdatera.
- **Ordrar:** En order skapas med en kund och en lista med produkter och antal. Delsummor och totalpris beräknas av API:et. Ordrar kan hämtas som lista, på ordernummer eller på datum.

## Teknik

- ASP.NET Core Web API (.NET 10)
- Entity Framework Core med migrationer och MySQL
- Seeddata som läses in från JSON-filer vid start
- Docker: `docker-compose.yml` för databasen och en `dockerfile` för att bygga API:et

## Avgränsningar

Det här är ett övningsprojekt och inte en färdig produktionstjänst. API:et saknar bland annat autentisering och behörighetsstyrning, och det har inget frontend.


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
