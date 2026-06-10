using System.Text;

namespace DrawingTool.Core;

// --- BRIDGE: Concrete Implementors ---

public class ConsoleCanvas : ICanvas
{
    public void DrawLine(double x1, double y1, double x2, double y2) => Console.WriteLine($"[Console] Line: ({x1},{y1}) to ({x2},{y2})");
    public void DrawCircle(double cx, double cy, double r) => Console.WriteLine($"[Console] Circle: Center({cx},{cy}), Radius({r})");
    public void DrawRect(double x, double y, double w, double h) => Console.WriteLine($"[Console] Rectangle: Origin({x},{y}), Width({w}), Height({h})");
    public void DrawEllipse(double cx, double cy, double rx, double ry) => Console.WriteLine($"[Console] Ellipse: Center({cx},{cy}), Rx({rx}), Ry({ry})");
}

public class SvgCanvas : ICanvas
{
    private readonly StringBuilder _svgBuilder = new();

    public void DrawLine(double x1, double y1, double x2, double y2) =>
        _svgBuilder.AppendLine($"  <line x1=\"{x1}\" y1=\"{y1}\" x2=\"{x2}\" y2=\"{y2}\" stroke=\"black\" stroke-width=\"2\" />");

    public void DrawCircle(double cx, double cy, double r) =>
        _svgBuilder.AppendLine($"  <circle cx=\"{cx}\" cy=\"{cy}\" r=\"{r}\" stroke=\"blue\" fill=\"none\" stroke-width=\"2\" />");

    public void DrawRect(double x, double y, double w, double h) =>
        _svgBuilder.AppendLine($"  <rect x=\"{x}\" y=\"{y}\" width=\"{w}\" height=\"{h}\" stroke=\"green\" fill=\"none\" stroke-width=\"2\" />");

    public void DrawEllipse(double cx, double cy, double rx, double ry) =>
        _svgBuilder.AppendLine($"  <ellipse cx=\"{cx}\" cy=\"{cy}\" rx=\"{rx}\" ry=\"{ry}\" stroke=\"red\" fill=\"none\" stroke-width=\"2\" />");

    public string GetSvg()
    {
        var result = new StringBuilder();
        result.AppendLine("<svg xmlns=\"http://www.w3.org/2000/svg\" version=\"1.1\" width=\"800\" height=\"600\">");
        result.Append(_svgBuilder.ToString());
        result.AppendLine("</svg>");
        return result.ToString();
    }
}

// --- PROXY: Read-Only Shape ---

public class ReadOnlyShapeProxy : IShape
{
    private readonly IShape _innerShape;

    public ReadOnlyShapeProxy(IShape innerShape) => _innerShape = innerShape;

    public void Draw(ICanvas canvas) => _innerShape.Draw(canvas);
    public BoundingBox GetBoundingBox() => _innerShape.GetBoundingBox();

    public void Move(double dx, double dy) => throw new InvalidOperationException("Shape is locked");
    public void Scale(double factor) => throw new InvalidOperationException("Shape is locked");
}