using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zombies.GameObjects.Components;
using Zombies.Enum;

namespace Zombies.GameObjects.Entities
{
    class Entity
    {
        //Property
        int _id;                                              //Entity's id
        EntityType _type;                                     //Entity's type
        List<Component> _components = new List<Component>();  //Entity's components

        /// <summary>
        /// Get ID
        /// </summary>
        public int ID { get => _id; }

        /// <summary>
        /// Get/Set components
        /// </summary>
        public List<Component> Components { get => _components; set => _components = value; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id"> Entity's id </param>
        public Entity(int id)
        {
            _id = id;
        }

        /// <summary>
        /// Link all component to this entity
        /// </summary>
        public void LinkComponents()
        {
            foreach(Component component in _components)
            {
                component.Entity = this;
            }
        }

        /// <summary>
        /// Return all component of choosen type in current game state entities list
        /// </summary>
        /// <param name="componentType"> Type of component to return </param>
        /// <param name="component"> List of founded component </param>
        /// <returns> if at least one component is found return => true else return => false </returns>
        public bool GetComponentOfType(Type componentType, out Component component)
        {
            //Init component with placeholder
            component = new AABB(0, 0);

            //Verify if searched type is a component 
            if (!componentType.IsSubclassOf(typeof(Component)))
            {
                return false;
            }

            //Search wanted component
            foreach (Component comp in _components)
            {
                if (comp.GetType() == componentType)
                {
                    component = comp;
                    return true;
                }
            }

            //If no components has been foun return false
            return false;
        }
    }
}
