using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
using SFML.Graphics;
using ZombiesGame.GameObjects;

namespace ZombiesGame.PhysicObjects
{
    abstract class PhysicObject
    {
        protected Vertex[] _vertices;                                                 //Physic object's vertices
        protected GameObject _obj;                                                    //GameObject related to this AABB

        /// <summary>
        /// Get vertices
        /// </summary>
        public Vertex[] Vertices { get => _vertices; }

        /// <summary>
        /// Get game object
        /// </summary>
        public GameObject GameObject { get => _obj; }

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
