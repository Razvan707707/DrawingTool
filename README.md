# DrawingTool — Instrument Grafic Scenariizat (Laborator 7)

Acest proiect demonstrează utilizarea combinată a trei **Design Patterns Structurale** importante (`Composite`, `Bridge`, `Proxy`) într-un subsistem de procesare și randare a formelor geometrice în .NET 8.

## 🏛️ Arhitectura și Pattern-urile Structurale

### 1. Composite
* **Unde este folosit:** `IShape` reprezintă componenta de bază, primitivele (`Line`, `Circle`, `Rectangle`, `Ellipse`) sunt elementele frunză (Leaf), iar clasa `Picture` este nodul compus (Composite).
* **Ce problemă rezolvă:** Permite tratarea unitară a obiectelor individuale și a grupurilor de obiecte. Clientul poate muta,scala sau desena o întreagă scenă imbricată pe n niveluri printr-un singur apel de metodă (`rootScene.Draw()`), acțiunea propagându-se recursiv în tot arborele.

### 2. Bridge
* **Unde este folosit:** `IShape` acționează ca Abstracția, iar `ICanvas` reprezintă Implementatorul, având ca derivări concrete `ConsoleCanvas` și `SvgCanvas`.
* **Ce problemă rezolvă:** Separă abstracția modelului grafic de mecanismul efectiv de randare. Formele geometrice nu conțin logica specifică de scriere pe disc sau afișare text; ele primesc un `ICanvas` prin metodă și apelează API-ul acestuia. Astfel, putem adăuga formate noi de export (ex: `PdfCanvas`) fără a modifica clasele formelor.

### 3. Proxy
* **Unde este folosit:** Clasa `ReadOnlyShapeProxy` învelește o instanță de `IShape`.
* **Ce problemă rezolvă:** Controlează și restricționează accesul la obiectul real. Proxy-ul permite operațiunile sigure de citire/vizualizare (`Draw`, `GetBoundingBox`), dar blochează acțiunile de modificare a stării (`Move`, `Scale`), aruncând o excepție de tipul `InvalidOperationException` dacă forma este marcată ca fiind blocată (Read-Only).

---

## 🎨 Exemplu de Scenă și Output SVG Generat

```csharp
// Construirea unei scene imbricate (Composite)
var rootScene = new Picture();
rootScene.Add(new Line(0, 0, 10, 10));

var houseGroup = new Picture();
houseGroup.Add(new Rectangle(50, 50, 100, 100));
houseGroup.Add(new Circle(100, 100, 20));

rootScene.Add(houseGroup);
rootScene.Scale(2.0); // Scalare recursivă uniformă
