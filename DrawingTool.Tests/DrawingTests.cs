using DrawingTool.Core;
using NUnit.Framework;

namespace DrawingTool.Tests;

[TestFixture]
public class DrawingTests
{
    [Test]
    public void Composite_ScaleRecursively_DoublesAllDimensions()
    {
        var picture = new Picture();
        var circle = new Circle(10, 10, 5);
        picture.Add(circle);

        picture.Scale(2.0);

        Assert.That(circle.R, Is.EqualTo(10.0));
        Assert.That(circle.Cx, Is.EqualTo(20.0));
    }

    [Test]
    public void Composite_GetBoundingBox_ReturnsUnifiedBox()
    {
        var picture = new Picture();
        picture.Add(new Rectangle(10, 10, 50, 50)); // Box: [10,10,60,60]
        picture.Add(new Circle(100, 100, 10));      // Box: [90,90,110,110]

        var box = picture.GetBoundingBox();

        Assert.That(box.XMin, Is.EqualTo(10.0));
        Assert.That(box.YMin, Is.EqualTo(10.0));
        Assert.That(box.XMax, Is.EqualTo(110.0));
        Assert.That(box.YMax, Is.EqualTo(110.0));
    }

    [Test]
    public void Bridge_SvgCanvas_ContainsCorrectTags()
    {
        var canvas = new SvgCanvas();
        var circle = new Circle(50, 50, 20);

        circle.Draw(canvas);
        var svgResult = canvas.GetSvg();

        Assert.That(svgResult, Does.Contain("<circle"));
        Assert.That(svgResult, Does.Contain("cx=\"50\""));
    }

    [Test]
    public void Proxy_MoveOnLockedShape_ThrowsInvalidOperationException()
    {
        var circle = new Circle(10, 10, 5);
        var proxy = new ReadOnlyShapeProxy(circle);

        Assert.Throws<InvalidOperationException>(() => proxy.Move(5, 5));
    }

    [Test]
    public void Proxy_DrawOnLockedShape_DelegatesSuccessfully()
    {
        var circle = new Circle(10, 10, 5);
        var proxy = new ReadOnlyShapeProxy(circle);
        var canvas = new ConsoleCanvas();

        // Nu trebuie sa arunce nicio exceptie, doar delegare normala
        Assert.DoesNotThrow(() => proxy.Draw(canvas));
    }
}