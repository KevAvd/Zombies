using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZombiesGame.Components;
using SFML.Graphics;

namespace ZombiesGame.GameObjects
{
    abstract class GameObject
    {
        //Object's Property
        protected AABB _aabb;                       //Axis aligned bounding box
        protected Vertex[] _vertices;               //Vertices
        protected Transformable _transformable;     //Transformable

        /// <summary>
        /// Get AABB
        /// </summary>
        public AABB AABB { get => _aabb;}

        /// <summary>
        /// Get vertices
        /// </summary>
        public Vertex[] Vertices { get => _vertices;}

        /// <summary>
        /// Get/Set transformable
        /// </summary>
        public Transformable Transformable { get => _transformable; set => _transformable = value; }

        /// <summary>
        /// Called at the start of the game
        /// </summary>
        public abstract void Start();

        /// <summary>
        /// Called every frame
        /// </summary>
        public abstract void Update();
    }
}
