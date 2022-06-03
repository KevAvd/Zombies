using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML_Engine.GameObjects.PhysicObjects;
using SFML_Engine.GameObjects.GraphicObjects;
using SFML_Engine.Enums;
using SFML.System;
using SFML.Graphics;

namespace SFML_Engine.GameObjects
{
    abstract class GameObject
    {
        //Object's Property
        protected PhysicObject _physicObject;                             //Physic object
        protected GraphicObject _graphicObject;                           //Vertices
        protected Transformable _transformable = new Transformable();     //Transformable
        protected GameObject _relative;                                   //Relative game object
        protected GraphicState _graphicState = GraphicState.LAYER_1;      //Graphic state of the object
        protected PhysicState _physicState = PhysicState.NOCLIP;          //Physic state of the object
        GameState _state;                                                 //Game state container
        bool _destroyed = false;                                          //Indicates if an object is destroyed
        bool _IsRelative = false;                                         //Indicates if an object transfromable is relative to another one

        /// <summary>
        /// Get/Set physic object
        /// </summary>
        public PhysicObject PhysicObject { get => _physicObject; set => _physicObject = value; }

        /// <summary>
        /// Get/Set graphic object
        /// </summary>
        public GraphicObject GraphicObject { get => _graphicObject; set => _graphicObject = value; }

        /// <summary>
        /// Get/Set transformable
        /// </summary>
        public Transformable Transformable { get => _transformable; set => _transformable = value; }

        /// <summary>
        /// Get/Set graphic state
        /// </summary>
        public GraphicState GraphicState { get => _graphicState; set => _graphicState = value; }

        /// <summary>
        /// Get/Set physic state
        /// </summary>
        public PhysicState PhysicState { get => _physicState; set => _physicState = value; }

        /// <summary>
        /// Get/Set relative object
        /// </summary>
        public GameObject Relative { get => _relative; set { _relative = value; _IsRelative = true; } }

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
        /// Get the gamestate that contains this object
        /// </summary>
        /// <returns> GameState </returns>
        public GameState GetGameState()
        {
            return _state;
        }

        /// <summary>
        /// Set this object's game state
        /// </summary>
        public void SetGameState(GameState state)
        {
            _state = state;
        }
    }
}
