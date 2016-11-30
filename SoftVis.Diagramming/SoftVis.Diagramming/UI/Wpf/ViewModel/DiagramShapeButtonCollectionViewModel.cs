﻿using System.Collections.Generic;
using System.Linq;
using Codartis.SoftVis.Diagramming;
using Codartis.SoftVis.Util.UI.Wpf.Collections;

namespace Codartis.SoftVis.UI.Wpf.ViewModel
{
    /// <summary>
    /// Creates and manages the diagram button viewmodels.
    /// </summary>
    internal class DiagramShapeButtonCollectionViewModel : DiagramViewModelBase
    { 
        public ObservableImmutableList<DiagramShapeButtonViewModelBase> DiagramNodeButtonViewModels { get; }

        public DiagramShapeButtonCollectionViewModel(IArrangedDiagram diagram)
            : base(diagram)
        {
            DiagramNodeButtonViewModels = new ObservableImmutableList<DiagramShapeButtonViewModelBase>(CreateButtons());
        }

        public void AssignButtonsTo(DiagramShapeViewModelBase diagramShapeViewModel)
        {
            foreach (var buttonViewModel in DiagramNodeButtonViewModels)
                buttonViewModel.AssociateWith(diagramShapeViewModel);
        }

        public bool AreButtonsAssignedTo(DiagramShapeViewModelBase diagramShapeViewModel)
        {
            return DiagramNodeButtonViewModels.Any(i => i.AssociatedDiagramShapeViewModel == diagramShapeViewModel);
        }

        public void HideButtons()
        {
            foreach (var buttonViewModel in DiagramNodeButtonViewModels)
                buttonViewModel.Hide();
        }

        private IEnumerable<DiagramShapeButtonViewModelBase> CreateButtons()
        {
            yield return new CloseShapeButtonViewModel(Diagram);

            foreach (var entityRelationType in Diagram.GetEntityRelationTypes())
                yield return new ShowRelatedNodeButtonViewModel(Diagram, entityRelationType);
        }
    }
}
