using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zombies.GameObjects.Entities;

namespace Zombies.GameObjects.Components
{
    abstract class Component
    {
        //Property
        protected Entity _entity;                   //Component's entity

        /// <summary>
        /// Get component's entity
        /// </summary>
        public Entity Entity { get => _entity; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="entity"> Component's entity </param>
        public Component(Entity entity)
        {
            _entity = entity;
        }
    }
}
