namespace DrawingTool.Core;

public struct BoundingBox
{
    public double XMin { get; set; }
    public double YMin { get; set; }
    public double XMax { get; set; }
    public double YMax { get; set; }

    public static BoundingBox FromPoints(double x1, double y1, double x2, double y2)
    {
        return new BoundingBox
        {
            XMin = Math.Min(x1, x2),
            YMin = Math.Min(y1, y2),
            XMax = Math.Max(x1, x2),
            YMax = Math.Max(y1, y2)
        };
    }
}

public interface ICanvas
{
    void DrawLine(double x1, double y1, double x2, double y2);
    void DrawCircle(double cx, double cy, double r);
    void DrawRect(double x, double y, double w, double h);
    void DrawEllipse(double cx, double cy, double rx, double ry);
}

public interface IShape
{
    void Draw(ICanvas canvas);
    void Move(double dx, double dy);
    void Scale(double factor);
    BoundingBox GetBoundingBox();
}