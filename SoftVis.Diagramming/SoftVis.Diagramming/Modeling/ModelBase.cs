﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Codartis.SoftVis.Modeling
{
    /// <summary>
    /// Abstract base class for models.
    /// </summary>
    public abstract class ModelBase : IModel
    {
        public abstract IEnumerable<IModelEntity> Entities { get; }
        public abstract IEnumerable<IModelRelationship> Relationships { get; }

        public virtual event EventHandler<IModelEntity> EntityAdded;
        public virtual event EventHandler<IModelRelationship> RelationshipAdded;
        public virtual event EventHandler<IModelEntity> EntityRemoved;
        public virtual event EventHandler<IModelRelationship> RelationshipRemoved;

        public IEnumerable<IModelEntity> GetRelatedEntities(IModelEntity entity, RelationshipSpecification specification)
        {
            var typeSpecification = specification.TypeSpecification;

            return specification.Direction == ModelRelationshipDirection.Incoming
                ? entity.IncomingRelationships.Where(i => i.IsOfType(typeSpecification)).Select(i => i.Source)
                : entity.OutgoingRelationships.Where(i => i.IsOfType(typeSpecification)).Select(i => i.Target);
        }
    }
}
