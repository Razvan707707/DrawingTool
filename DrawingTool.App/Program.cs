using DrawingTool.Core;

Console.WriteLine("=== Testare Sistem Grafic (Composite + Bridge + Proxy) ===\n");

// Creăm Scena Principală (Composite Nivel 1)
var rootScene = new Picture();
rootScene.Add(new Line(0, 0, 10, 10));

// Creăm o sub-pictură (Composite Nivel 2)
var houseGrup = new Picture();
houseGrup.Add(new Rectangle(50, 50, 100, 100)); // Baza casei
houseGrup.Add(new Circle(100, 100, 20));       // Fereastra casei

// Adăugăm sub-pictura în scena principală
rootScene.Add(houseGrup);

// Randerizare prin BRIDGE pe Consolă
Console.WriteLine("1. Randare pe Canvas-ul de consola:");
var consoleCanvas = new ConsoleCanvas();
rootScene.Draw(consoleCanvas);

// Scalăm întreaga scenă uniform (Propagare prin COMPOSITE)
Console.WriteLine("\n2. Scalam scena de 2 ori si exportam in SVG:");
rootScene.Scale(2.0);

var svgCanvas = new SvgCanvas();
rootScene.Draw(svgCanvas);
Console.WriteLine(svgCanvas.GetSvg());

// Testare PROXY
Console.WriteLine("\n3. Testare element blocat (Proxy):");
IShape lockedCircle = new ReadOnlyShapeProxy(new Circle(200, 200, 30));
lockedCircle.Draw(consoleCanvas); // Permis

try
{
    lockedCircle.Move(10, 10); // Va arunca excepție
}
catch (Exception ex)
{
    Console.WriteLine($"[PROXY ALERT] Operatie blocata cu succes: {ex.Message}");
}