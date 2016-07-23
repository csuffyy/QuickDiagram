﻿using System.Collections.Generic;
using Codartis.SoftVis.Geometry;
using Codartis.SoftVis.UI.Extensibility;
using Codartis.SoftVis.UI.Geometry;
using Codartis.SoftVis.VisualStudioIntegration.Diagramming;
using Codartis.SoftVis.VisualStudioIntegration.Modeling;

namespace Codartis.SoftVis.VisualStudioIntegration.UI
{
    /// <summary>
    /// Extends the built-in diagram behaviour for the roslyn-based model elements.
    /// </summary>
    internal class CustomDiagramBehaviourProvider : DefaultDiagramBehaviourProvider
    {
        private const double ButtonShiftX = ButtonRadius * 1.1;

        private static readonly RelatedEntityButtonDescriptor ImplementedInterfacesDescriptor =
            new RelatedEntityButtonDescriptor(
                CustomRelationshipSpecifications.ImplementedInterface, CustomConnectorTypes.Implementation,
                new RectRelativeLocation(RectAlignment.TopMiddle, new Point2D(ButtonShiftX, ButtonOverlapParentBy)));

        private static readonly RelatedEntityButtonDescriptor ImplementerTypesDescriptor =
            new RelatedEntityButtonDescriptor(
                CustomRelationshipSpecifications.ImplementerType, CustomConnectorTypes.Implementation,
                new RectRelativeLocation(RectAlignment.BottomMiddle, new Point2D(ButtonShiftX, -ButtonOverlapParentBy)));

        public override IEnumerable<RelatedEntityButtonDescriptor> GetRelatedEntityButtonDescriptors()
        {
            yield return BaseTypesDescriptor.WithRelativeLocationTranslate(new Point2D(-ButtonShiftX, ButtonOverlapParentBy));
            yield return SubtypesDescriptor.WithRelativeLocationTranslate(new Point2D(-ButtonShiftX, -ButtonOverlapParentBy));
            yield return ImplementedInterfacesDescriptor;
            yield return ImplementerTypesDescriptor;
        }
    }
}
