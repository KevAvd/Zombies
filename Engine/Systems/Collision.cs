using SFML.System;

namespace SFML_Engine
{
    class Collision
    {
        GameObject _obj1;
        GameObject _obj2;
        Vector2f _pNear;
        Vector2f _pFar;
        Vector2f _normal;
        CollisionType _collisionType;

        /// <summary>
        /// Get collided game object
        /// </summary>
        internal GameObject Obj1 { get => _obj1; }

        /// <summary>
        /// Get collided game object
        /// </summary>
        internal GameObject Obj2 { get => _obj2; }

        /// <summary>
        /// Get collision type
        /// </summary>
        internal CollisionType Type { get => _collisionType; }

        /// <summary>
        /// Get near collision point vector
        /// </summary>
        public Vector2f PNear { get => _pNear; }

        /// <summary>
        /// Get far collision point vector
        /// </summary>
        public Vector2f PFar { get => _pFar; }

        /// <summary>
        /// Get normal of the collided surface
        /// </summary>
        public Vector2f Normal { get => _normal; }

        public enum CollisionType
        {
            AABB_AABB,
            AABB_RAY,
            AABB_CIRCLE,
            CIRCLE_CIRCLE,
            CIRCLE_RAY,
            NULL
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="obj1"> Collided object </param>
        /// <param name="obj2"> Collided object </param>
        /// <param name="type"> Type of collision </param>
        public Collision(GameObject obj1, GameObject obj2, CollisionType type)
        {
            _obj1 = obj1;
            _obj2 = obj2;
            _collisionType = type;
            _pNear = new Vector2f(0, 0);
            _pFar = new Vector2f(0, 0);
            _normal = new Vector2f(0, 0);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="obj1"> Collided object </param>
        /// <param name="obj2"> Collided object </param>
        /// <param name="type"> Type of collision </param>
        public Collision(GameObject obj1, GameObject obj2, Vector2f pNear, Vector2f normal, CollisionType type)
        {
            _obj1 = obj1;
            _obj2 = obj2;
            _collisionType = type;
            _pNear = pNear;
            _normal = normal;
            _pFar = new Vector2f(0, 0);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="obj1"> Collided object </param>
        /// <param name="obj2"> Collided object </param>
        /// <param name="type"> Type of collision </param>
        public Collision(GameObject obj1, GameObject obj2, Vector2f pNear, Vector2f pFar, Vector2f normal, CollisionType type)
        {
            _obj1 = obj1;
            _obj2 = obj2;
            _collisionType = type;
            _pNear = pNear;
            _pFar = pFar;
            _normal = normal;
        }
    }
}
