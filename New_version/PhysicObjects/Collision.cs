using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZombiesGame.Enums;
using ZombiesGame.GameObjects;

namespace ZombiesGame.PhysicObjects
{
    internal class Collision
    {
        //Collision property
        CollisionType _type;                 //Type of the collision
        GameObject _obj1;                    //Object that collided
        GameObject _obj2;                    //Object that collided

        /// <summary>
        /// Get type of collision
        /// </summary>
        public CollisionType Type { get => _type; }

        /// <summary>
        /// Get object that collided (1)
        /// </summary>
        public GameObject Obj1 { get => _obj1; }

        /// <summary>
        /// Get object that collided (2)
        /// </summary>
        public GameObject Obj2 { get => _obj2; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="type"> Type of collision </param>
        /// <param name="obj1"> Object that collided </param>
        /// <param name="obj2"> Object that collided </param>
        public Collision(CollisionType type, GameObject obj1, GameObject obj2)
        {
            _type = type;
            _obj1 = obj1;
            _obj2 = obj2;
        }
    }
}
