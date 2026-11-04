# CSv fájlok feldolgozása és adatbázisba importálása

## Feladat leírása

Console alkalmazás készítése, amely egy CSV fájlban tárolt adatokat felfolgoz és adatbázisba másolja.

## A bemeneti CSV fájl jellemzõi

- A fájl neve: `naprendszer.csv`
- karakterkódolás: UTF-8
- Elválasztó karakter: `;` Splitelésnél figyelembe kell venni
- Az elsõ sor a fejléc, amely a mezõneveket tartalmazza
- A tizedes jegyeknél a `,` karaktert használjuk

### A CSV fájl oszlopai

| Sorszám | CSV oszlop név | Javasolt C# típus | Javasolt MySQL típus | Jelentés |
|---------|----------------|-------------------|----------------------|----------|
| 1. | **bolygo_id** | `int` | `INT` | A bolygó egyedi azonosítója |
| 2. | **neve** | `string` | `VARCHAR` | A bolygó neve |
| 3. | **tipus_id** | `int` | `INT` | A bolygó típusának az azonosítója |
| 4. | **tomeg** | `double` | `DECIMAL(10,3)` | A bolygó tömege |
| 5. | **egyenlitori_sugar** | `double` | `DECIMAL(10,3)` | A bolygó egyenlitöri sugara |
| 6. | **holdak_szama** | `int` | `INT` | A bolygó holdjainak száma |
| 7. | **nap_tavolsag** | `double` | `DECIMAL(10,3)` | A bolygó távolsága a naptól |
| 8. | **fold_tavolsag** | `double` | `DECIMAL(10,3)` | A bolygó távolsága a földtõl |
| 9. | **leiras** | `string` | `MEDIUMTEXT` | A bolygó rövid leírása |
| 10. | **url** | `string` | `MEDIUMTEXT` | A bolygó képének a linkje |

## Megvalósítandó mûveletek

### 1. projekt létrehozása
- [ ] Hozz létre egy új C# konzolos projektet
- [ ] A projekt neve: `CS_I_CON_CSVFELDOLG`
- [ ] A projekt .NET verziója: .NET Framework 4.8
- [ ] A `naprendszer.csv` fájlt importáld be a projektbe, úgy, hogy készíts egy Preferences mappát és az alá tedd a fájlt. A fájl tulajdonságainál állítsd be, hogy a fálj másolódjon a kimeneti mappába.

### 2. CSV fájl feldolgzása
- [ ] A program indításakor a `naprendszer.csv` fájlt olvassa be
- [ ] Határozzuk meg futási idõben hogy a CSV fájlt honnan kell beolvasni
- [ ] `Assembly.GetExecutingAssembly().Location` segítségével határozzuk meg a futtatható fálj helyét
- [ ] A `GetDirectoryName()` metóduis segítségével határozzuk meg az EXE mappáját
- [ ] A fájl elérési utját a mappához fûzzük hozzá. A `Path.Combine` metódussal
- [ ] Ellenõrizd le, hogy tényleg ott van e a fájl
- [ ] Ha nincs akkor írj ki egy hibaüzenetet és lépj ki a programból
- [ ] Ha a fájl a helyén van, akkor olvassuk be a fájl tartalmát a `File.ReadAllLines` metódussal és tároljuk el egy `string` tömbben

### CSV fájl adatainak feldolgozása, konvertálása
- [ ] A fájl elsõ sorát, a fejlécet hagyjuk ki
- [ ] String -> int, decimal, string típusokra konvertáljuk az adatokat

```csharp
var culture = new CultureInfo("hu-HU");
bool sikeres = TryParse(string s, culture, out int result);
```

- [ ] A konvertálás során `decimal` típusnál a `CultureInfo`-t állítsuk be `hu-HU`-ra, hogy a tizedes jel `,` legyen 

## Beolvasott és átalakított adatok tárolása

- [ ] A beolvasott és átalakított adatokat tároljuk el egy `List<Bolygo>` típusú listában
- [ ] A `Bolygo` osztályt a következõ tulajdonságokkal hozd létre:
```csharp
public class Bolygo {
	public int BolygoId { get; set; }
	public string Neve { get; set; }
	public int TipusId { get; set; }
	public decimal Tomeg { get; set; }
	public decimal EgyenlitoSugar { get; set; }
	public int Holdakszama { get; set; }
	public decimal NapTavolsag { get; set; }
	public decimal FoldTavolsag { get; set; }
	public string Leiras { get; set; }
	public string URL { get; set; }
}
```