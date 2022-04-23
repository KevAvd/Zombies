using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using ZombiesGame.Mathematics;

namespace ZombiesGame.PhysicObjects
{
    class Ray : PhysicObject
    {
        //Ray's property
        Vector2f _direction;            //Direction vector
        float _length;                  //Ray's length
        RayType _type;                  //Ray's type

        /// <summary>
        /// Get/Set direction vector
        /// </summary>
        public Vector2f Direction
        {
            get { return _direction; }
            set { if (_type == RayType.DIRECTION) { _direction = LinearAlgebra.NormalizeVector(value); } }
        }

        /// <summary>
        /// Get/Set length
        /// </summary>
        public float Length 
        { 
            get { return _length; }
            set { if (_type == RayType.DIRECTION) { _length = value; } }
        }

        /// <summary>
        /// Get/Set hitpoint
        /// </summary>
        public Vector2f HitPoint
        {
            get { return _vertices[1].Position; }
            set { if (_type == RayType.HITPOINT) { _vertices[1].Position = value; } }
        }

        enum RayType
        {
            HITPOINT,
            DIRECTION
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="origin"> Ray's origin </param>
        /// <param name="direction"> Ray's direction </param>
        /// <param name="length"> Ray's length </param>
        public Ray(Vector2f origin, Vector2f direction, float length)
        {
            _vertices = new Vertex[2];
            _direction = LinearAlgebra.NormalizeVector(direction);
            _length = length;
            _type = RayType.DIRECTION;
            UpdatePosition(origin);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="origin"> Ray's origin </param>
        /// <param name="direction"> Ray's hitPoint </param>
        /// <param name="length"> Ray's length </param>
        public Ray(Vector2f origin, Vector2f hitPoint)
        {
            _vertices = new Vertex[2];
            _vertices[0].Position = origin;
            _vertices[1].Position = hitPoint;
            _type = RayType.HITPOINT;
            UpdatePosition(origin);
        }

        public override void UpdatePosition(Vector2f pos)
        {
            _vertices[0].Position = pos;

            if(_type == RayType.HITPOINT)
            {
                Vector2f direction = _vertices[1].Position - _vertices[0].Position;
                _length = LinearAlgebra.GetVectorLength(direction);
                _direction = LinearAlgebra.NormalizeVector(direction);
                return;
            }

            _vertices[0].Position = pos;
            _vertices[1].Position = _direction * _length + _vertices[0].Position;
        }
    }
}
