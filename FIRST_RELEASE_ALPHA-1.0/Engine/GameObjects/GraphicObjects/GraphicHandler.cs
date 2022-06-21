using SFML.Graphics;
using System;
using System.Collections.Generic;

namespace SFML_Engine
{
    class GraphicHandler
    {
        Dictionary<string, GraphicObject> _graphicObj = new Dictionary<string, GraphicObject>(); //Contains all graphic objects
        GraphicObject _currentGrphObj;                                                           //Current displayed graphic object
        GraphicObject _defaultGrphObj;                                                           //Default sprite (Becomes current graphic objet after end of animation)
        int _animationCount;                                                                     //Number of time to play animation
        GraphicState _graphicState = GraphicState.LAYER_1;                                       //Graphic state
        Frame _frame = new Frame();                                                              //Contains a frame

        /// <summary>
        /// Get current graphic object
        /// </summary>
        public GraphicObject CurrentGrphObj { get => _currentGrphObj; }

        /// <summary>
        /// Get default sprite
        /// </summary>
        public GraphicObject DefaultGrphObj { get => _defaultGrphObj; }

        /// <summary>
        /// Get/Set graphic state
        /// </summary>
        public GraphicState GraphicState { get => _graphicState; set => _graphicState = value; }

        /// <summary>
        /// Get/Set frame
        /// </summary>
        public Frame Frame { get => _frame; set => _frame = value; }

        /// <summary>
        /// Add a graphic object
        /// </summary>
        /// <param name="name"> Name (Key) </param>
        /// <param name="obj"> Graphic object to add </param>
        /// <returns> True if object has been added </returns>
        public bool AddGraphicObject(string name, GraphicObject obj)
        {
            if (_graphicObj.ContainsKey(name))
            {
                return false;
            }

            if (_graphicObj.ContainsValue(obj))
            {
                return false;
            }

            _graphicObj.Add(name, obj);
            return true;
        }

        /// <summary>
        /// Play an animation
        /// </summary>
        /// <param name="name"> Name (key) </param>
        /// <param name="nbrOfTime"> Number of time to play the animation </param>
        /// <returns> True if animation is played correctly </returns>
        public bool PlayAnimation(string name, int nbrOfTime, AnimationType animType = AnimationType.NORMAL)
        {
            if(_graphicObj.TryGetValue(name, out GraphicObject value) && value.GetType() == typeof(Animation))
            {
                _animationCount = nbrOfTime;
                (value as Animation).Restart();
                (value as Animation).Type = animType;
                _currentGrphObj = value;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Try get graphic object
        /// </summary>
        /// <param name="name"> Name of the graphic object </param>
        /// <param name="obj"> Graphic object </param>
        /// <returns> True if a graphic object has been found </returns>
        public bool GetGraphicObject(string name, out GraphicObject obj)
        {
            if(_graphicObj.TryGetValue(name, out obj))
            {
                return true;
            }

            return false;
        }
        /// <summary>
        /// Set the default sprite
        /// </summary>
        /// <param name="name"> Name (key) </param>
        /// <returns> True if default sprite exist in this graphic handler </returns>
        public bool SetDefaultSprite(string name)
        {
            if (_graphicObj.TryGetValue(name, out GraphicObject value) && value.GetType() == typeof(GameSprite))
            {
                _defaultGrphObj = value as GameSprite;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Set frame as current graphic object 
        /// </summary>
        public void SetFrameAsCurrent()
        {
            _currentGrphObj = _frame;
            _defaultGrphObj = _frame;
        }

        /// <summary>
        /// Update graphic handler
        /// </summary>
        public void Update()
        {
            if(_currentGrphObj != null && _currentGrphObj.GetType() == typeof(Animation) && (_currentGrphObj as Animation).Count == _animationCount)
            {
                SetDefaultSpriteToCurrent();
            }
        }

        /// <summary>
        /// Set the default sprite to current
        /// </summary>
        public void SetDefaultSpriteToCurrent()
        {
            if(_defaultGrphObj != null)
            {
                _currentGrphObj = _defaultGrphObj;
            }
        }
    }
}
