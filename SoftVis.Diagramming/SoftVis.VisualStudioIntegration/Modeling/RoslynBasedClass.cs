﻿using System.Collections.Generic;
using System.Linq;
using Codartis.SoftVis.Modeling;
using Codartis.SoftVis.VisualStudioIntegration.Modeling.Building;
using Codartis.SoftVis.VisualStudioIntegration.WorkspaceContext;
using Microsoft.CodeAnalysis;

namespace Codartis.SoftVis.VisualStudioIntegration.Modeling
{
    /// <summary>
    /// A model entity created from a Roslyn class symbol.
    /// </summary>
    internal class RoslynBasedClass : RoslynBasedModelEntity
    {
        internal RoslynBasedClass(INamedTypeSymbol namedTypeSymbol)
            : base(namedTypeSymbol, TypeKind.Class)
        {
        }

        public override int Priority => 4;
        public override bool IsAbstract => RoslynSymbol.IsAbstract;

        public RoslynBasedClass BaseClass
        {
            get
            {
                return OutgoingRelationships.FirstOrDefault(i => i.IsGeneralization())?.Target as RoslynBasedClass;
            }
        }

        public IEnumerable<RoslynBasedInterface> ImplementedInterfaces
        {
            get
            {
                return OutgoingRelationships.Where(i => i.IsInterfaceImplementation())
                    .Select(i => i.Target).OfType<RoslynBasedInterface>();
            }
        }

        public IEnumerable<RoslynBasedClass> DerivedClasses
        {
            get
            {
                return IncomingRelationships.Where(i => i.IsGeneralization())
                    .Select(i => i.Source).OfType<RoslynBasedClass>();
            }
        }

        public override IEnumerable<RelatedRoslynSymbols> FindRelatedSymbols(IWorkspaceServices workspaceServices, INamedTypeSymbol roslynSymbol)
        {
            EnsureSymbolTypeKind(roslynSymbol, TypeKind.Class);

            return GetDerivedTypeSymbols(workspaceServices, roslynSymbol);
        }

        private IEnumerable<RelatedRoslynSymbols> GetDerivedTypeSymbols(IWorkspaceServices workspaceServices, INamedTypeSymbol classSymbol)
        {
            var workspace = workspaceServices.GetWorkspace();
            return FindDerivedTypesAsync(workspace, classSymbol);
        }

        private static IEnumerable<RelatedRoslynSymbols> FindDerivedTypesAsync(Workspace workspace, INamedTypeSymbol classSymbol)
        {
            foreach (var project in workspace.CurrentSolution.Projects)
            {
                var compilation = project.GetCompilationAsync().Result;
                var visitor = new DerivedTypesFinderVisitor(classSymbol);

                compilation.Assembly.Accept(visitor);
                foreach (var descendant in visitor.DerivedTypeSymbols)
                    yield return new RelatedRoslynSymbols(classSymbol, descendant, RelationshipSpecifications.Subtype);
            }
        }

    }
}
