using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
using SFML.Graphics;

namespace SFML_Engine
{
    abstract class GameObject
    {
        //Object's Property
        protected PhysicObject _physicObject;                             //Physic object
        protected GraphicHandler _graphicHandler = new GraphicHandler();  //Handles graphics for the object
        protected Transformable _transformable = new Transformable();     //Transformable
        protected GameObject _relative;                                   //Relative game object
        GameState _state;                                                 //Game state container
        bool _destroyed = false;                                          //Indicates if an object is destroyed
        bool _IsRelative = false;                                         //Indicates if an object transfromable is relative to another one
        Vector2f _relativePosition;                                       //Position relative to another game object

        /// <summary>
        /// Get/Set physic object
        /// </summary>
        public PhysicObject PhysicObject { get => _physicObject; set => _physicObject = value; }

        /// <summary>
        /// Get/Set graphic object
        /// </summary>
        public GraphicHandler GraphicHandler { get => _graphicHandler; set => _graphicHandler = value; }

        /// <summary>
        /// Get/Set transformable
        /// </summary>
        public Transformable Transformable { get => _transformable; set => _transformable = value; }

        /// <summary>
        /// Get/Set relative object
        /// </summary>
        public GameObject Relative { get => _relative; set { _relative = value; _IsRelative = true; } }

        /// <summary>
        /// Get/Set Game state
        /// </summary>
        public GameState GameState { get => _state; set => _state = value; }

        /// <summary>
        /// Get relative position
        /// </summary>
        public Vector2f RelativePosition { get => _relativePosition; }

        /// <summary>
        /// Get/Set position
        /// </summary>
        public Vector2f Position
        {
            get { return _transformable.Position; }
            set 
            {
                _transformable.Position = value;

                if (_IsRelative)
                {
                    UpdateRelativePosition(value);
                    return;
                }

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

        /// <summary>
        /// Destroys this object
        /// </summary>
        public void Destroy()
        {
            _destroyed = true;
        }

        /// <summary>
        /// Indicate if this object is destroyed
        /// </summary>
        /// <returns></returns>
        public bool IsDestroyed()
        {
            return _destroyed;
        }

        /// <summary>
        /// This object transformable will no longer be relative to another object transformable
        /// </summary>
        public void RemoveRelative()
        {
            _IsRelative = false;
        }

        /// <summary>
        /// Indicate if this object is destroyed
        /// </summary>
        /// <returns></returns>
        public bool IsRelative()
        {
            return _IsRelative;
        }

        /// <summary>
        /// Update the relative position of the object
        /// </summary>
        /// <param name="pos"> Position </param>
        public void UpdateRelativePosition(Vector2f pos)
        {
            _relativePosition = GameMath.VectorRotation(pos, _relative.Rotation) + _relative.Position;
            _physicObject.UpdatePosition(_relativePosition);
        }
    }
}
