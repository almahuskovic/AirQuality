# **AirQuality — Praćenje kvalitete zraka**

**AirQuality** je web aplikacija razvijena u **.NET tehnologiji** koja omogućava praćenje trenutne kvalitete zraka u različitim gradovima svijeta. Podaci se preuzimaju preko **Open-Meteo Air Quality API** servisa, koji obezbjeđuje informacije o indeksu kvalitete zraka i prisustvu štetnih čestica (**PM2.5, PM10, AQI**).

---

## **Glavne funkcionalnosti**

* **Prikaz trenutne kvalitete zraka po gradovima**
* **Interaktivna mapa za vizualni pregled zagađenja**
* **Pretraga gradova i pregled detalja**
* **Kreiranje korisničkog profila i autentifikacija preko ASP.NET Core Identity**
* **Dodavanje omiljenih gradova**
* **Brisanje gradova sa liste favorita**
* **Vizualno prikazivanje vrijednosti i evaluacija kvalitete zraka** (boje i statusi)

---

## **Instalacija i pokretanje**

### 1️⃣ **Kloniranje repozitorija**

```bash
git clone https://github.com/almahuskovic/AirQuality.git
```

### 2️⃣ **Podešavanje konekcije na bazu**

U fajlu **appsettings.json** ažurirati:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=IME_SERVERA;Database=AirQualityDB;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

### 3️⃣ **Primjena migracija baze**

U **Package Manager Console**:

```bash
update-database
```

### 4️⃣ **Pokretanje aplikacije**


---

##  **Tehnologije korištene u projektu**

| Tehnologija                    | Opis                                |
| ------------------------------ | ----------------------------------- |
| **.NET 8.0 / C#**              | Backend logika                      |
| **MVC / Razor Pages**          | Frontend UI                         |
| **Entity Framework Core**      | Rad sa bazom i migracije            |
| **SQL Server**                 | Relacijska baza podataka            |
| **ASP.NET Core Identity**      | Autentifikacija i korisnički nalozi |
| **Open-Meteo Air Quality API** | Izvor podataka o kvaliteti zraka    |

API dokumentacija: [https://open-meteo.com/en/docs/air-quality-api](https://open-meteo.com/en/docs/air-quality-api)


