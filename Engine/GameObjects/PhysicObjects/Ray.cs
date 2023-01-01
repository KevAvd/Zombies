using SFML.Graphics;
using SFML.System;
using System;

namespace SFML_Engine
{
    class Ray : PhysicObject
    {
        //Ray's property
        Vector2f _direction;            //Direction vector
        float _length;                  //Ray's length
        float _rotation;                //Ray's rotation
        RayType _type;                  //Ray's type


        /// <summary>
        /// Get length
        /// </summary>
        public float Length { get => _length; } 

        /// <summary>
        /// Get rotation
        /// </summary>
        public float Rotation { get => _rotation; }

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
            _direction = GameMath.NormalizeVector(direction);
            _rotation = (float)Math.Atan2(direction.Y - origin.Y, direction.X - origin.X);
            _length = length;
            _type = RayType.DIRECTION;
            UpdatePosition(origin);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="origin"> Ray's origin </param>
        /// <param name="angle"> Ray's angle in radian </param>
        /// <param name="length"> Ray's length </param>
        public Ray(Vector2f origin, float angle, float length)
        {
            _vertices = new Vertex[2];
            _vertices[0].Position = origin;
            _vertices[1].Position = origin + GameMath.VectorRotation(new Vector2f(length, 0), angle);
            _rotation = angle;
            _type = RayType.HITPOINT;
            _length = length;
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
            _rotation = (float)Math.Atan2(hitPoint.Y - origin.Y, hitPoint.X - origin.X);
            _type = RayType.HITPOINT;
            UpdatePosition(origin);
        }

        public override void UpdatePosition(Vector2f pos)
        {
            _vertices[0].Position = pos;

            if(_type == RayType.HITPOINT)
            {
                Vector2f direction = _vertices[1].Position - _vertices[0].Position;
                _length = GameMath.GetVectorLength(direction);
                _direction = GameMath.NormalizeVector(direction);
                return;
            }

            _vertices[0].Position = pos;
            _vertices[1].Position = _direction * _length + _vertices[0].Position;
        }
    }
}
