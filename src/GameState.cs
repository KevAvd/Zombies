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
                components = new Component[0];
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

        /// <summary>
        /// Return all component that is a subtype of choosen type in current game state entities list
        /// </summary>
        /// <param name="componentType"> Type to search </param>
        /// <param name="components"> List of founded component </param>
        /// <returns> if at least one component is found return => true else return => false </returns>
        public bool GetAllComponentOfSubType(Type componentType, out Component[] components)
        {
            //List of founded components
            List<Component> cmpnts = new List<Component>();

            //Verify if searched type is a component 
            if (!componentType.IsSubclassOf(typeof(Component)))
            {
                components = new Component[0];
                return false;
            }

            //Get all components of choosen type
            foreach (Entity entity in _entities)
            {
                foreach (Component comp in entity.Components)
                {
                    if (comp.GetType().IsSubclassOf(componentType))
                    {
                        cmpnts.Add(comp);
                    }
                }
            }

            //Fill out array with founded components
            components = cmpnts.ToArray();

            //If any components is founded return true
            if (cmpnts.Count > 0)
            {
                return true;
            }

            //If no components has been foun return false
            return false;
        }

        /// <summary>
        /// Play all "Start" script in current entity list
        /// </summary>
        public void PlayScriptStart()
        {
            if(!GetAllComponentOfSubType(typeof(Script), out Component[] components))
            {
                return;
            }

            foreach(Script script in components)
            {
                script.Start();
            }
        }

        /// <summary>
        /// Play all "OnUpdate" script in current entity list
        /// </summary>
        /// <param name="dt"> Delta time </param>
        public void PlayScriptOnUpdate(float dt)
        {
            if (!GetAllComponentOfSubType(typeof(Script), out Component[] components))
            {
                return;
            }

            foreach (Script script in components)
            {
                script.OnUpdate(dt);
            }
        }
    }
}
