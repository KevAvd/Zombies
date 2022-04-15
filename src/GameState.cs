using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zombies.GameObjects.Entities;
using Zombies.GameObjects.Components;

namespace Zombies
{
    class GameState
    {
        //Property
        List<Entity> _entities = new List<Entity>();     //All entities of current state

        internal List<Entity> Entities { get => _entities; set => _entities = value; }

        /// <summary>
        /// Return all component of choosen type in current game state entities list
        /// </summary>
        /// <param name="componentType"> Type of component to return </param>
        /// <param name="components"> List of founded component </param>
        /// <returns> if at least one component is found return => true else return => false </returns>
        public bool GetAllComponentOfType(Type componentType, out Component[] components)
        {
            //List of founded components
            List<Component> cmpnts = new List<Component>();

            //Verify if searched type is a component 
            if (!componentType.IsSubclassOf(typeof(Component)))
            {
                components = cmpnts.ToArray();
                return false;
            }

            //Get all components of choosen type
            foreach(Entity entity in _entities)
            {
                foreach(Component comp in entity.Components)
                {
                    if(comp.GetType() == componentType)
                    {
                        cmpnts.Add(comp);
                    }
                }
            }

            //Fill out array with founded components
            components = cmpnts.ToArray();

            //If any components is founded return true
            if(cmpnts.Count > 0)
            {
                return true;
            }
            
            //If no components has been foun return false
            return false;
        }
    }
}
