using SFML.System;
using SFML.Graphics;
using SFML_Engine.Enums;

namespace SFML_Engine.GameObjects.PhysicObjects
{
    abstract class PhysicObject
    {
        //Property
        protected Vertex[] _vertices;                   //Physic object's vertices

        //State
        PhysicState _phyState;                          //Physic object's state

        /// <summary>
        /// Get vertices
        /// </summary>
        public Vertex[] Vertices { get => _vertices; }

        /// <summary>
        /// Get/Set physic object state
        /// </summary>
        internal PhysicState State { get => _phyState; set => _phyState = value; }

        /// <summary>
        /// Get all points that represent the physic object
        /// </summary>
        /// <returns> Array of 2d vectors </returns>
        public Vector2f[] GetPoints()
        {
            Vector2f[] points = new Vector2f[_vertices.Length];

            for (int i = 0; i < points.Length; i++)
            {
                points[i] = _vertices[i].Position;
            }

            return points;
        }

        /// <summary>
        /// Update the physic object's position
        /// </summary>
        public abstract void UpdatePosition(Vector2f pos);
    }
}
