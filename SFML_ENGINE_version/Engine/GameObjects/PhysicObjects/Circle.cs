using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;

namespace SFML_Engine.GameObjects.PhysicObjects
{
    class Circle : PhysicObject
    {
        float _radius;                  //Circle's radius

        /// <summary>
        /// Get/Set radius
        /// </summary>
        public float Radius { get => _radius; set => _radius = value; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="pos"> Position </param>
        /// <param name="radius"> Radius </param>
        public Circle(Vector2f pos, float radius)
        {
            _vertices = new Vertex[1];
            _radius = radius;
            UpdatePosition(pos);
        }

        public override void UpdatePosition(Vector2f pos)
        {
            _vertices[0].Position = pos;
        }
    }
}
