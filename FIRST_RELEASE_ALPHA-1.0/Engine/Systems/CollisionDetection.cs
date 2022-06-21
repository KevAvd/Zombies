using SFML.System;
using System;

namespace SFML_Engine
{
    static class CollisionDetection
    {
        /// <summary>
        /// Detect collision between AABB and a ray
        /// </summary>
        /// <param name="box1"> AABB to check </param>
        /// <param name="ray"> Ray to check </param>
        /// <param name="pNear"> Near collision point </param>
        /// <param name="pFar"> Far collision point </param>
        /// <param name="normal"> Surface normal of the near collision point </param>
        /// <returns> True if collision </returns>
        public static bool AABB_RAY(AABB box1, Ray ray, out Vector2f pNear, out Vector2f pFar, out Vector2f normal)
        {
            //Init
            pNear = new Vector2f(0, 0);               //Near collision point
            pFar = new Vector2f(0, 0);                //Far collision point
            normal = new Vector2f(0, 0);              //Surface normal of the near collision point
            float swap;                               //Used for swaping two values
            Vector2f[] aabbCoords = box1.GetPoints(); //Contains AABB's coords
            Vector2f[] rayCoords = ray.GetPoints();   //Contains ray's coords
            Vector2f d = rayCoords[1] - rayCoords[0]; //Ray distance
            Vector2f tNear = new Vector2f((aabbCoords[0].X - rayCoords[0].X) / d.X, (aabbCoords[0].Y - rayCoords[0].Y) / d.Y);   //time to near collision
            Vector2f tFar = new Vector2f((aabbCoords[2].X - rayCoords[0].X) / d.X, (aabbCoords[2].Y - rayCoords[0].Y) / d.Y);    //time to far collision

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
            if (tNear.X > tNear.Y) { normal = d.X < 0 ? new Vector2f(1, 0) : new Vector2f(-1, 0); }
            else if (tNear.X < tNear.Y) { normal = d.Y < 0 ? new Vector2f(0, 1) : new Vector2f(0, -1); }

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
        public static bool CIRCLE_AABB(Circle circle, AABB box, out Vector2f pNear, out Vector2f normal)
        {
            //Init
            pNear = new Vector2f(0, 0);
            normal = new Vector2f(0, 0);
            float half_w = box.Width / 2.0f;
            float half_h = box.Height / 2.0f;
            Vector2f circlePosition = circle.GetPoints()[0];
            Vector2f AABBPosition = box.GetPoints()[0] + new Vector2f(half_w, half_h);
            float dx = circlePosition.X - AABBPosition.X; 
            float dy = circlePosition.Y - AABBPosition.Y;

            //Calculate position of contact point
            if (Math.Abs(dx) > half_w) { dx = dx < 0 ? half_w * -1 : half_w; }
            if (Math.Abs(dy) > half_h) { dy = dy < 0 ? half_h * -1 : half_h; }
            pNear = AABBPosition + new Vector2f(dx, dy);

            //Check if circle is intersecting with the contact point
            if(GameMath.GetVectorLength(circlePosition - pNear) > circle.Radius)
            {
                return false;
            }

            //Find surface normal of the AABB at collision point
            if (pNear.X == AABBPosition.X - half_w)
            {
                normal = new Vector2f(-1, 0);
            }
            else if(pNear.X == AABBPosition.X + half_w)
            {
                normal = new Vector2f(1, 0);
            }
            else if(pNear.Y == AABBPosition.Y - half_h)
            {
                normal = new Vector2f(0, -1);
            }
            else if(pNear.Y == AABBPosition.Y + half_h)
            {
                normal = new Vector2f(0, 1);
            }

            return true;
        }

        /// <summary>
        /// Detect collision between a circle and a ray
        /// </summary>
        /// <param name="circle"> Circle to check </param>
        /// <param name="ray"> Ray to check </param>
        /// <param name="pNear"> Near collision point </param>
        /// <returns> True if collision </returns>
        public static bool CIRCLE_RAY(Circle circle, Ray ray, out Vector2f pNear, out Vector2f normal)
        {
            Vector2f circlePosition = circle.GetPoints()[0];
            Vector2f d = circle.GetPoints()[0] - ray.GetPoints()[0];
            Vector2f start = ray.GetPoints()[0];
            Vector2f end = ray.GetPoints()[1];
            Vector2f line = end - start;
            pNear = GameMath.ProjectVector(d, line) + start;
            normal = GameMath.NormalizeVector(circlePosition - pNear);

            if(GameMath.GetVectorLength(start - circlePosition) < circle.Radius ||
               GameMath.GetVectorLength(end - circlePosition) < circle.Radius)
            {
                return true;
            }

            if(GameMath.GetVectorLength(circlePosition - pNear) > circle.Radius)
            {
                return false;
            }

            if(GameMath.GetVectorLength(pNear - start) > GameMath.GetVectorLength(line) ||
               GameMath.GetVectorLength(pNear - end) > GameMath.GetVectorLength(line))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Detect collision between two physic object
        /// </summary>
        /// <param name="obj1"> Physic object to check </param>
        /// <param name="obj2"> Physic object to check </param>
        /// <param name="collision"> Type of collision </param>
        /// <returns> True if collision </returns>
        public static bool GAMEOBJ_GAMEOBJ(GameObject obj1, GameObject obj2, out Collision collision)
        {
            //Init
            bool collided = false;
            PhysicObject physobj1 = obj1.PhysicObject;
            PhysicObject physobj2 = obj2.PhysicObject;
            collision = new Collision(obj1, obj2, Collision.CollisionType.NULL);

            if(physobj1.GetType() == typeof(AABB))
            {
                if(physobj2.GetType() == typeof(AABB))
                {
                    collided = AABB_AABB(physobj1 as AABB, physobj2 as AABB);
                    collision = new Collision(obj1, obj2, Collision.CollisionType.AABB_AABB);
                }

                else if (physobj2.GetType() == typeof(Ray))
                {
                    collided = AABB_RAY(physobj1 as AABB, physobj2 as Ray, out Vector2f pNear, out Vector2f pFar, out Vector2f normal);
                    collision = new Collision(obj1, obj2, pNear, pFar, normal, Collision.CollisionType.AABB_RAY);
                }

                else if (physobj2.GetType() == typeof(Circle))
                {
                    collided = CIRCLE_AABB(physobj2 as Circle, physobj1 as AABB, out Vector2f pNear, out Vector2f normal);
                    collision = new Collision(obj1, obj2, pNear, normal, Collision.CollisionType.AABB_CIRCLE);
                }
            }

            else if(physobj1.GetType() == typeof(Ray))
            {
                if(physobj2.GetType() == typeof(AABB))
                {
                    collided = AABB_RAY(physobj2 as AABB, physobj1 as Ray, out Vector2f pNear, out Vector2f pFar, out Vector2f normal);
                    collision = new Collision(obj1, obj2, pNear, pFar, normal, Collision.CollisionType.AABB_RAY);
                }

                if(physobj2.GetType() == typeof(Circle))
                {
                    collided = CIRCLE_RAY(physobj2 as Circle, physobj1 as Ray, out Vector2f pNear, out Vector2f normal);
                    collision = new Collision(obj1, obj2, pNear, normal, Collision.CollisionType.CIRCLE_RAY);
                }
            }

            else if(physobj1.GetType() == typeof(Circle))
            {
                if(physobj2.GetType() == typeof(Circle))
                {
                    collided = CIRCLE_CIRCLE(physobj1 as Circle, physobj2 as Circle);
                    collision = new Collision(obj1, obj2, Collision.CollisionType.CIRCLE_CIRCLE);
                }

                else if(physobj2.GetType() == typeof(AABB))
                {
                    collided = CIRCLE_AABB(physobj1 as Circle, physobj2 as AABB, out Vector2f pNear, out Vector2f normal);
                    collision = new Collision(obj1, obj2, pNear, normal, Collision.CollisionType.AABB_CIRCLE);
                }

                else if (physobj2.GetType() == typeof(Ray))
                {
                    collided = CIRCLE_RAY(physobj1 as Circle, physobj2 as Ray, out Vector2f pNear, out Vector2f normal);
                    collision = new Collision(obj1, obj2, pNear, normal, Collision.CollisionType.CIRCLE_RAY);
                }
            }

            return collided;
        }
    }
}
