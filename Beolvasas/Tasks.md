# Feladat megoldás lépsrõl- lépésre
## 1. lépés: A feladat megértése
Készíts egy programot, amely képes feldolgozni egy CSV fájlt, 
és az adatait egy MySQL adatbázis táblába írja be.
A 'naprandszer.csv' fájl tartalmazza a szökséges adatokat.
A program készítése során figyeljünk a következõkre:
- A CSV fájl elsõ sora a fejléc, amely tartalmazza az oszlopneveket.
- A CSV fájl minden sora egy-egy rekordot képvisel.
- Nyisd meg 'naprandszer.csv' fájlt, olvasd be az adatokat, 
  és írd be azokat a MySQL adatbázisba.

## A bemeneti fájl jellemzõi:
- A fájl neve: 'naprendszer.csv'
- A fájl formátuma: CSV (Comma-Separated Values)
- Az adatokat (';') karakter választja el egymástól.)
- Karakterkódolás: UTF-8
- Az elsõ sor a fejléc, amely tartalmazza az oszlopneveket.
- A tizedes számok vesszõvel vannak elválasztva (pl. 3,14).

## A MySQL adatbázis adatai:
- Az adatbázis neve: 'naprendszer'
- Az adatbázis tabla neve: 'bolygok'

Ahhoz hogy az adatbázist létrehozzuk el kell indítani az adatbázis szervert, jelen esetben a XAMP sefítségével.
A MySQL adatbázis létrehozásához a következõ SQL parancsot kell kiadni:
Adatbázis parancs a MySQL adatbázis létrehozásához:


### A CSV fájl oszlopai
| Sorszám | CSV-oszlop | Javasolt C# típus | Javasolt MySQL-típus | Jelentés |
|---:|---|---|---|---|
| 1. | `bolygo_id` | `int` | `INT` | A bolygó egyedi azonosítója |
| 2. | `neve` | `string` | `VARCHAR(50)` | A bolygó neve |
| 3. | `tipus_id` | `int` | `INT` | A bolygó típusának azonosítója |
| 4. | `tomeg` | `decimal` | `DECIMAL(10,3)` | A bolygó tömege a Föld tömegéhez viszonyítva |
| 5. | `egyenlitori_sugar` | `decimal` | `DECIMAL(10,3)` | Egyenlítõi sugár a Föld sugarához viszonyítva |
| 6. | `holdak_szama` | `int` | `INT` | Az ismert holdak száma |
| 7. | `nap_tavolsag` | `decimal` | `DECIMAL(10,3)` | Naptól mért távolság relatív értéke |
| 8. | `fold_tavolsag` | `decimal` | `DECIMAL(10,3)` | Földtõl mért távolság relatív értéke |
| 9. | `leiras` | `string` | `TEXT` | Rövid szöveges leírás |
| 10. | `url` | `string` | `VARCHAR(500)` | A bolygó képének webcíme |

### Létre kell a Visual Studio 2022-ben egy új C# projektet 
- amelynek a neve legyen 'X_Y_CON_Task_Sample'.
- A projekt típusa legyen 'Console Application'. .NET Framework 4.8 legyen kiválasztva.
- Léterhoztunk egy új mappát 'Preferences' néven, amelyben a 'naprendszer.csv' fájl található.
- A 'naprendszer.csv' fájl -> Copy Always beállítás legyen kiválasztva, 
hogy a fájl a build során a kimeneti mappába kerüljön.

### A CSV-fájl ellenõrzése és beolvasása
A CSV-fájl beolvasásához a következõ lépéseket kell tenni
- [ ] Létrehozzuk futási idõben a Preferences mapap helyét
- [ ] 'Assembly.GetExecutingAssembly().Location' segítségével meghatározzuk a futtatható fájl helyét.
- [ ] A fájl elérési útját a 'Path.Combine()' metódussal hozzuk létre.
- [ ] Ellenõrizd, hogy a fájl látezik-e a 'File.Exists' metódussal."
- [ ] Ha létezik olvasd be a fájl sorait UTF-8 kódolással a 'File.ReadAllLines' metódussal.
- [ ] A beolvasott sorokat a 'string[]' típusú tömbben tároljuk.

### A CSV-fájl feldolgozása
- [ ] Bontsd fel a beolvasott sorokat a ';' karakter mentén a 'string.Split()' metódussal.
- [ ] Az elsõ sort (fejléc) hagyd ki, és a többi sort dolgozd fel.
- [ ] A feldolgozott adatokat tárold egy listában, amelynek típusa legyen 'List<Bolygo>'.
- [ ] A 'Bolygo' osztályt hozd létre a következõ tulajdonságokkal:
  - `int BolygoId`
  - `string Neve`
  - `int TipusId`
  - `decimal Tomeg`
  - `decimal EgyenlitoriSugar`
  - `int HoldakSzama`
  - `decimal NapTavolsag`
  - `decimal FoldTavolsag`
  - `string Leiras`
  - `string Url`
### Adatkonverziók használata
 - [ ] A CSV-fájlban található adatokat a megfelelõ típusokra 
 - [ ] konvertáljuk az egész számokat 'Convert.ToInt32',metódus segítségével.
 - [ ] A tizedes számokat decimal.Tryparse metódussal konvertáljuk, figyelve a ',' karakterre.
 - [ ] A tizedes vesszõt a 'CultureInfo.InvariantCulture' segítségével kezeljük.
 - [ ] Hiba esetén a program futása ne álljon meg, hanem a hibás sort hagyjuk ki, 
 és írjunk ki egy hibaüzenetet a konzolra.
 - [ ] 
Példa tizedes szám feldolgozására:
```csharp
var _culture = CultureInfo.InvariantCulture;
bool sikeres = decimal.TryParse(tomegString, NumberStyles.Any, 
     _culture, out decimal tomeg);
```

### Adatok tartalmi ellenõrzése
- [ ] A `bolygo_id_` csak pozitív egész szám és 0 lehet. Egyedi!! és legyen értéke
- [ ] A `neve` nem lehet üres. Szöveg típusú. hossza: Max: karakter
- [ ] a `tipus_id` csak pozitív egész szám és 0 lehet. Egyedi!! és legyen értéke
- [ ] A `tomeg` Lebegõ pontos szám, nem kell egyedi
- [ ] A `egyenlito_sugar` Lebegõ pontos szám, nem kell egyedi
- [ ] A `nap_tavolsag` Lebegõ pontos szám, nem kell egyedi
- [ ] A `fold_tavolsag` Lebegõ pontos szám, nem kell egyedi
- [ ] A `holdak_szama` csak pozitiv egész szám és 0 lehet.

- Döntés hibás adat esetén: Írja ki a hibás adatot és sort, de menjen tovább a következõ sorra

