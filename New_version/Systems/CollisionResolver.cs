using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZombiesGame.PhysicObjects;
using ZombiesGame.GameObjects.Characters;
using ZombiesGame.GameObjects;
using ZombiesGame.Enums;

namespace ZombiesGame.Systems
{
    static class CollisionResolver
    {
        /// <summary>
        /// Resovle collision
        /// </summary>
        /// <param name="toResolve"> Collision to resolve </param>
        public static void Resolve(Collision toResolve)
        {
            CollisionType type = toResolve.Type;
            GameObject obj1 = toResolve.Obj1;
            GameObject obj2 = toResolve.Obj2;

            if(type == CollisionType.AABB_AABB && 
               obj1.GetType().IsSubclassOf(typeof(Character)) &&
               obj2.GetType().IsSubclassOf(typeof(Character)))
            {
                CHARACTER_AABBs_Solver(obj1, obj2);
            }
        }

        /// <summary>
        /// Solve collision between two characters AABBs
        /// </summary>
        private static void CHARACTER_AABBs_Solver(GameObject obj1, GameObject obj2)
        {
            
        }
    }
}
