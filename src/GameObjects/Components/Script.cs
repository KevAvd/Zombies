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
        protected AABB COMP_AABB;
        protected GameSprite COMP_Sprite;
        protected Position COMP_Position;

        //Entity's property
        protected string ENTITY_Name;

        //Game state
        protected GameState GAME_State;

        /// <summary>
        /// Get/Set game state
        /// </summary>
        public GameState State { get => GAME_State; set => GAME_State = value; }

        /// <summary>
        /// Return all components used in script
        /// </summary>
        /// <returns> Components used in script </returns>
        public Component[] GetUsedComponents(out string name)
        {
            //List of used components
            List<Component> usedComponents = new List<Component>();

            //Add non null components to the list
            if (COMP_AABB != null)
            {
                usedComponents.Add(COMP_AABB);
            }
            if (COMP_Sprite != null)
            {
                usedComponents.Add(COMP_Sprite);
            }
            if (COMP_Position != null)
            {
                usedComponents.Add(COMP_Position);
            }

            //Set name
            name = ENTITY_Name;

            //Add script
            usedComponents.Add(this);

            //Return all used components
            return usedComponents.ToArray();
        }

        /// <summary>
        /// This method is called every update
        /// </summary>
        /// <param name="dt"> Delta time </param>
        public abstract void OnUpdate(float dt);

        /// <summary>
        /// This method is called at the start of the game
        /// </summary>
        public abstract void Start();
    }
}
