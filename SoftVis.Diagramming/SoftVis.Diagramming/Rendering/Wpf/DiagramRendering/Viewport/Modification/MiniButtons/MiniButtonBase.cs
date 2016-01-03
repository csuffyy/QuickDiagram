using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;
using Codartis.SoftVis.Rendering.Extensibility;
using Codartis.SoftVis.Rendering.Wpf.DiagramRendering.Shapes;

namespace Codartis.SoftVis.Rendering.Wpf.DiagramRendering.Viewport.Modification.MiniButtons
{
    /// <summary>
    /// Abstract base class for adorners that work as a button.
    /// </summary>
    internal abstract class MiniButtonBase : Adorner
    {
        protected const double MiniButtonRadius = DefaultDiagramBehaviourProvider.MiniButtonRadius;
        private const double ButtonFrameThickness = 1d;

        public DiagramShapeControlBase AdornedShape { get; }

        protected MiniButtonBase(DiagramShapeControlBase adornedShape, Visibility initialVisibility = Visibility.Collapsed)
            : base(adornedShape)
        {
            AdornedShape = adornedShape;
            Visibility = initialVisibility;
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            var center = GetButtonCenterRelativeToAdornedControl();
            DrawFrame(drawingContext, center);
            DrawPicture(drawingContext, center);
        }

        protected abstract Point GetButtonCenterRelativeToAdornedControl();

        protected abstract void DrawPicture(DrawingContext drawingContext, Point center);

        private void DrawFrame(DrawingContext drawingContext, Point center)
        {
            var pen = new Pen(AdornedShape.Foreground, ButtonFrameThickness);
            var brush = AdornedShape.Background;

            drawingContext.DrawEllipse(brush, pen, center, MiniButtonRadius, MiniButtonRadius);
        }
    }
}