﻿using System;
using Microsoft.CodeAnalysis;

namespace Codartis.SoftVis.VisualStudioIntegration.Commands.ShellTriggered
{
    /// <summary>
    /// Adds the current symbol (the one at the caret) and its related entities to the diagram.
    /// Shows the diagram if it was not visible.
    /// </summary>
    internal sealed class AddToDiagramWithRelatedEntitiesCommand : ShellTriggeredCommandBase
    {
        public AddToDiagramWithRelatedEntitiesCommand(IPackageServices packageServices)
            :base(VsctConstants.SoftVisCommandSetGuid, VsctConstants.AddToDiagramWithRelatedEntitiesCommand, packageServices)
        {
        }

        public override async void Execute(object sender, EventArgs e)
        {
            var workspaceServices = PackageServices.GetWorkspaceServices();
            var namedTypeSymbol = await workspaceServices.GetCurrentSymbol() as INamedTypeSymbol;
            if (namedTypeSymbol == null)
                return;

            var modelBuilder = PackageServices.GetModelServices();
            var modelEntity = modelBuilder.GetOrAddRoslynSymbol(namedTypeSymbol);
            if (modelEntity == null)
                return;

            var diagramServices = PackageServices.GetDiagramServices();
            diagramServices.ShowModelEntityWithRelatedEntities(modelEntity);

            var uiServices = PackageServices.GetUIServices();
            uiServices.ShowDiagramWindow();
            uiServices.FitDiagramToView();
        }
    }
}
