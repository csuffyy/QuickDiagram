﻿using System;
using Codartis.SoftVis.Diagramming.Shapes;

namespace Codartis.SoftVis.VisualStudioIntegration.Events
{
    internal class DiagramNodeActivatedEventArgs : EventArgs
    {
        public DiagramNode DiagramNode { get; }

        public DiagramNodeActivatedEventArgs(DiagramNode diagramNode)
        {
            DiagramNode = diagramNode;
        }
    }
}