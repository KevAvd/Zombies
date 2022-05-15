using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
using SFML_Engine.GameObjects.PhysicObjects;
using SFML_Engine.Mathematics;

namespace SFML_Engine.Systems
{
    static class CollisionDetection
    {
        /// <summary>
        /// Detect collision between AABB and a ray
        /// </summary>
        /// <param name="box1"> AABB to check </param>
        /// <param name="ray"> Ray to check </param>
        /// <param name="pNear"> Near collision point </param>
        /// <param name="pFar"> Fat collision point </param>
        /// <param name="normal"> Surface normal of the near collision point </param>
        /// <returns> True if collision </returns>
        public static bool AABB_RAY(AABB box1, Ray ray, out Vector2f pNear, out Vector2f pFar, out Vector2f normal)
        {
            float swap;                               //Used for swaping two values
            Vector2f[] aabbCoords = box1.GetPoints(); //Contains AABB's coords
            Vector2f[] rayCoords = ray.GetPoints();   //Contains ray's coords
            Vector2f d = rayCoords[1] - rayCoords[0]; //Ray distance
            Vector2f tNear = new Vector2f((aabbCoords[0].X - rayCoords[0].X) / d.X, (aabbCoords[0].Y - rayCoords[0].Y) / d.Y);   //time to near collision
            Vector2f tFar = new Vector2f((aabbCoords[2].X - rayCoords[0].X) / d.X, (aabbCoords[2].Y - rayCoords[0].Y) / d.Y);    //time to far collision

            //Init out parametres
            pNear = new Vector2f(0, 0);
            pFar = new Vector2f(0, 0);
            normal = new Vector2f(0, 0);

            //Sort values
            if (tNear.X > tFar.X)
            {
                swap = tNear.X;
                tNear.X = tFar.X;
                tFar.X = swap;
            }
            if (tNear.Y > tFar.Y) 
            {
                swap = tNear.Y;
                tNear.Y = tFar.Y;
                tFar.Y = swap;
            }

            //Check if there is a collision
            if (tNear.X > tFar.Y || tNear.Y > tFar.X)  { return false; }
            if (Math.Min(tFar.X, tFar.Y) < 0)  { return false; }
            float tHitNear = Math.Max(tNear.X, tNear.Y);
            float tHitFar = Math.Min(tFar.X, tFar.Y);
            if (tHitNear > 1) { return false; }

            //Get collision point
            pNear = rayCoords[0] + tHitNear * d;
            pFar = rayCoords[0] + tHitFar * d;

            //Find surface normal of the AABB at collision point
            if(tNear.X > tNear.Y)
            {
                if(d.X < 0)
                {
                    normal = new Vector2f(1, 0);
                }
                else
                {
                    normal = new Vector2f(-1, 0);
                }
            }
            else if (tNear.X < tNear.Y)
            {
                if(d.Y < 0)
                {
                    normal = new Vector2f(0, 1);
                }
                else
                {
                    normal = new Vector2f(0, -1);
                }
            }

            return true;
        } 

        /// <summary>
        /// Detect collision between two AABBs
        /// </summary>
        /// <param name="box1"> AABB to check </param>
        /// <param name="box2"> AABB to check </param>
        /// <returns> True if collision </returns>
        public static bool AABB_AABB(AABB box1, AABB box2)
        {
            //Get AABBs positions
            Vector2f[] p1 = box1.GetPoints();
            Vector2f[] p2 = box2.GetPoints();

            //Check for collision
            if (p1[0].X > p2[2].X)
            {
                return false;
            }
            if (p1[2].X < p2[0].X)
            {
                return false;
            }
            if (p1[0].Y > p2[2].Y)
            {
                return false;
            }
            if (p1[2].Y < p2[0].Y)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Detect collision between two circles
        /// </summary>
        /// <param name="circle1"> Circle to check </param>
        /// <param name="circle2"> Circle to check </param>
        /// <returns> True if collision </returns>
        public static bool CIRCLE_CIRCLE(Circle circle1, Circle circle2)
        {
            //Get circles positions
            Vector2f pos1 = circle1.GetPoints()[0];
            Vector2f pos2 = circle2.GetPoints()[0];

            //Calculate sum of both radius
            float radius_sum = circle1.Radius + circle2.Radius;

            //Calculate distance between both circles
            float d = GameMath.GetVectorLength(pos2 - pos1);

            //Check for collision
            if(d <= radius_sum)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Detect collision between Circle and AABB
        /// </summary>
        /// <param name="circle"> Circle to check </param>
        /// <param name="box"> AABB to check </param>
        /// <returns> True if collision </returns>
        public static bool CIRCLE_AABB(Circle circle, AABB box)
        {
            //Not implemented
            return false;
        }
    }
}
