using SFML.System;
using SFML.Graphics;

namespace SFML_Engine
{
    abstract class PhysicObject
    {
        //Property
        protected Vertex[] _vertices;                   //Physic object's vertices

        //State
        PhysicState _phyState = PhysicState.NORMAL;     //Physic object's state

        /// <summary>
        /// Get vertices
        /// </summary>
        public Vertex[] Vertices { get => _vertices; }

        /// <summary>
        /// Get/Set physic object state
        /// </summary>
        public PhysicState State { get => _phyState; set => _phyState = value; }

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
