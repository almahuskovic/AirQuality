AirQuality je web aplikacija razvijena u .NET tehnologiji, koja omogućava praćenje trenutne kvalitete zraka u različitim gradovima svijeta. Podaci se preuzimaju preko Open-Meteo Air Quality API servisa, koji obezbjeđuje informacije o indeksu kvalitete zraka i prisustvu štetnih čestica (PM2.5, PM10, AQI ).

Glavne funkcionalnosti

Prikaz trenutne kvalitete zraka po gradovima
Interaktivna mapa za pregled stanja zagađenosti
Pretraga gradova i pregled detalja
Kreiranje korisničkog profila i autentifikacija putem ASP.NET Core Identity
Dodavanje omiljenih gradova
Brisanje gradova sa liste favorita
Vizualno prikazivanje vrijednosti i evaluacija stanja zraka pomoću prikladnih oznaka/boja

Instalacija i pokretanje

1. Kloniranje repozitorija
git clone https://github.com/almahuskovic/AirQuality.git
2. Podešavanje konekcije na bazu
U fajlu appsettings.json ažurirati:
"ConnectionStrings": {
  "DefaultConnection": "Server=IME_SERVERA;Database=AirQualityDB;Trusted_Connection=True;TrustServerCertificate=True;"
}
3. Primjena migracija
add-migration InitialMigration
update-database
  
Tehnologije korištene u projektu

.NET 8.0 Core / C#
MVC/ Razor Pages (UI)/n
Entity Framework Core
SQL Server
Open-Meteo Air Quality API(https://open-meteo.com/en/docs/air-quality-api)
Identity za autentifikaciju i upravljanje korisnicima


