using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZombiesGame.PhysicObjects;
using SFML.System;
using SFML.Graphics;

namespace ZombiesGame.GameObjects
{
    abstract class GameObject
    {
        //Object's Property
        protected PhysicObject _physicObject;                             //Physic object
        protected Vertex[] _vertices;                                     //Vertices
        protected Transformable _transformable = new Transformable();     //Transformable

        /// <summary>
        /// Get AABB
        /// </summary>
        public PhysicObject PhysicObject { get => _physicObject; }

        /// <summary>
        /// Get vertices
        /// </summary>
        public Vertex[] Vertices { get => _vertices;}

        /// <summary>
        /// Get/Set transformable
        /// </summary>
        public Transformable Transformable { get => _transformable; set => _transformable = value; }

        /// <summary>
        /// Get/Set position
        /// </summary>
        public Vector2f Position
        {
            get { return _transformable.Position; }
            set 
            {
                _transformable.Position = value;
                _physicObject.UpdatePosition(_transformable.Position);
            }
        }

        /// <summary>
        /// Get/Set scale
        /// </summary>
        public Vector2f Scale
        {
            get { return _transformable.Scale; }
            set { _transformable.Scale = value; }
        }

        /// <summary>
        /// Get/Set rotation
        /// </summary>
        public float Rotation
        {
            get { return _transformable.Rotation; }
            set { _transformable.Rotation = value; }
        }

        /// <summary>
        /// Get/Set origin
        /// </summary>
        public Vector2f Origin
        {
            get { return _transformable.Origin; }
            set { _transformable.Origin = value; }
        }
    }
}
