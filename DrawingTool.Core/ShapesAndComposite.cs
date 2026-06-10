namespace DrawingTool.Core;

// --- LEAF ELEMENTS (Primitivele) ---

public class Line : IShape
{
    public double X1 { get; set; }
    public double Y1 { get; set; }
    public double X2 { get; set; }
    public double Y2 { get; set; }

    public Line(double x1, double y1, double x2, double y2) { X1 = x1; Y1 = y1; X2 = x2; Y2 = y2; }

    public void Draw(ICanvas canvas) => canvas.DrawLine(X1, Y1, X2, Y2);
    public void Move(double dx, double dy) { X1 += dx; Y1 += dy; X2 += dx; Y2 += dy; }
    public void Scale(double factor) { X1 *= factor; Y1 *= factor; X2 *= factor; Y2 *= factor; }
    public BoundingBox GetBoundingBox() => BoundingBox.FromPoints(X1, Y1, X2, Y2);
}

public class Circle : IShape
{
    public double Cx { get; set; }
    public double Cy { get; set; }
    public double R { get; set; }

    public Circle(double cx, double cy, double r) { Cx = cx; Cy = cy; R = r; }

    public void Draw(ICanvas canvas) => canvas.DrawCircle(Cx, Cy, R);
    public void Move(double dx, double dy) { Cx += dx; Cy += dy; }
    public void Scale(double factor) { Cx *= factor; Cy *= factor; R *= factor; }
    public BoundingBox GetBoundingBox() => BoundingBox.FromPoints(Cx - R, Cy - R, Cx + R, Cy + R);
}

public class Rectangle : IShape
{
    public double X { get; set; }
    public double Y { get; set; }
    public double W { get; set; }
    public double H { get; set; }

    public Rectangle(double x, double y, double w, double h) { X = x; Y = y; W = w; H = h; }

    public void Draw(ICanvas canvas) => canvas.DrawRect(X, Y, W, H);
    public void Move(double dx, double dy) { X += dx; Y += dy; }
    public void Scale(double factor) { X *= factor; Y *= factor; W *= factor; H *= factor; }
    public BoundingBox GetBoundingBox() => BoundingBox.FromPoints(X, Y, X + W, Y + H);
}

public class Ellipse : IShape
{
    public double Cx { get; set; }
    public double Cy { get; set; }
    public double Rx { get; set; }
    public double Ry { get; set; }

    public Ellipse(double cx, double cy, double rx, double ry) { Cx = cx; Cy = cy; Rx = rx; Ry = ry; }

    public void Draw(ICanvas canvas) => canvas.DrawEllipse(Cx, Cy, Rx, Ry);
    public void Move(double dx, double dy) { Cx += dx; Cy += dy; }
    public void Scale(double factor) { Cx *= factor; Cy *= factor; Rx *= factor; Ry *= factor; }
    public BoundingBox GetBoundingBox() => BoundingBox.FromPoints(Cx - Rx, Cy - Ry, Cx + Rx, Cy + Ry);
}

// --- COMPOSITE ELEMENT (Grupul de forme) ---

public class Picture : IShape
{
    private readonly List<IShape> _shapes = new();

    public void Add(IShape shape) => _shapes.Add(shape);
    public void Remove(IShape shape) => _shapes.Remove(shape);

    public void Draw(ICanvas canvas) { foreach (var shape in _shapes) shape.Draw(canvas); }
    public void Move(double dx, double dy) { foreach (var shape in _shapes) shape.Move(dx, dy); }
    public void Scale(double factor) { foreach (var shape in _shapes) shape.Scale(factor); }

    public BoundingBox GetBoundingBox()
    {
        if (_shapes.Count == 0) return new BoundingBox();

        double xMin = double.MaxValue, yMin = double.MaxValue;
        double xMax = double.MinValue, yMax = double.MinValue;

        foreach (var shape in _shapes)
        {
            var box = shape.GetBoundingBox();
            if (box.XMin < xMin) xMin = box.XMin;
            if (box.YMin < yMin) yMin = box.YMin;
            if (box.XMax > xMax) xMax = box.XMax;
            if (box.YMax > yMax) yMax = box.YMax;
        }

        return new BoundingBox { XMin = xMin, YMin = yMin, XMax = xMax, YMax = yMax };
    }
}