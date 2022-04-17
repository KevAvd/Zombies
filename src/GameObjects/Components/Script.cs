using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zombies.GameObjects.Entities;
using Zombies.Enum;

namespace Zombies.GameObjects.Components
{
    abstract class Script : Component
    {
        //Components
        protected AABB _aabb;
        protected GameSprite _sprite;
        protected Position _position;

        /// <summary>
        /// Return all components used in script
        /// </summary>
        /// <returns> Components used in script </returns>
        public Component[] GetUsedComponents()
        {
            //List of used components
            List<Component> usedComponents = new List<Component>();

            //Add non null components to the list
            if (_aabb != null)
            {
                usedComponents.Add(_aabb);
            }
            if (_sprite != null)
            {
                usedComponents.Add(_sprite);
            }
            if (_position != null)
            {
                usedComponents.Add(_position);
            }

            //Add script
            usedComponents.Add(this);

            //Return all used components
            return usedComponents.ToArray();
        }

        /// <summary>
        /// This method is called at the start of the game
        /// </summary>
        public abstract void Start();

        /// <summary>
        /// This method is called every update
        /// </summary>
        /// <param name="dt"> Delta time </param>
        public abstract void OnUpdate(float dt);
    }
}
