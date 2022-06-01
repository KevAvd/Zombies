using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
using SFML.Graphics;
using SFML_Engine.Systems;
using SFML_Engine.Enums;

namespace SFML_Engine.GameObjects.GraphicObjects
{
    internal class Animation : GraphicObject
    {
        //Property
        List<Vertex> _vertices = new List<Vertex>();        //All vertices (4 by sprite)
        int _index = 0;                                     //Keep tracks of current frame
        float _timeAcc = 0;                                 //Accumulate time
        float _duration;                                    //Duration of one frame
        int _increment = 1;                                 //Used for incrementing the index when loop animating
        int _animCount = 0;                                 //Count number of time the animation has been played
        AnimationType _type;                                //Type of animation

        /// <summary>
        /// Get/Set duration
        /// </summary>
        public float Duration { get => _duration; set => _duration = value; }

        /// <summary>
        /// Get/Set type
        /// </summary>
        internal AnimationType Type { get => _type; set { _type = value; _timeAcc = 0; } }

        /// <summary>
        /// Get animation count
        /// </summary>
        public int Count { get => _animCount; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="duration"> Duration of one frame in millisecond </param>
        public Animation(float duration)
        {
            _duration = duration / 1000f;
            _type = AnimationType.NORMAL;
        }

        /// <summary>
        /// Add frame
        /// </summary>
        /// <param name="texCoord1"> Texture coordinates Top-Left </param>
        /// <param name="texCoord2"> Texture coordinates Top-Right </param>
        /// <param name="texCoord3"> Texture coordinates Bottom-Right </param>
        /// <param name="texCoord4"> Texture coordinates Bottom-Left </param>
        public override void AddSprite(float x, float y, float w, float h)
        {
            _vertices.Add(new Vertex(new Vector2f(-1, -1), new Color(255, 255, 255, 255), new Vector2f(x + 0, y + 0)));
            _vertices.Add(new Vertex(new Vector2f( 1, -1), new Color(255, 255, 255, 255), new Vector2f(x + w, y + 0)));
            _vertices.Add(new Vertex(new Vector2f( 1,  1), new Color(255, 255, 255, 255), new Vector2f(x + w, y + h)));
            _vertices.Add(new Vertex(new Vector2f(-1,  1), new Color(255, 255, 255, 255), new Vector2f(x + 0, y + h)));
        }

        /// <summary>
        /// Get all the vertices that compose the actual frame
        /// </summary>
        /// <returns> Vertex array </returns>
        public override Vertex[] GetVertices()
        {
            return new Vertex[]
            {
                _vertices[_index * 4 + 0],
                _vertices[_index * 4 + 1],
                _vertices[_index * 4 + 2],
                _vertices[_index * 4 + 3],
            };
        }

        /// <summary>
        /// Get the number of frame in this graphic object
        /// </summary>
        /// <returns> Number of frame </returns>
        public int FrameCount()
        {
            return _vertices.Count / 4;
        }

        /// <summary>
        /// Update the animation
        /// </summary>
        public void Update()
        {
            _timeAcc += GameTime.DeltaTimeU;

            if(_timeAcc >= _duration)
            {
                switch (_type)
                {
                    case AnimationType.NORMAL: NextFrame(); break;
                    case AnimationType.REVERSE: PreviousFrame(); break;
                    case AnimationType.LOOP: LoopFrame(); break;
                }
                _timeAcc = 0;
            }
        }

        /// <summary>
        /// Go to next frame
        /// </summary>
        public void NextFrame()
        {
            _index++;

            if(_index + 1 > FrameCount())
            {
                _index = 0;
                _animCount++;
            }
        }

        /// <summary>
        /// Go to previous frame
        /// </summary>
        public void PreviousFrame()
        {
            _index--;

            if (_index < 0)
            {
                _index = FrameCount() - 1;
                _animCount++;
            }
        }

        /// <summary>
        /// Loop trough each frame
        /// </summary>
        public void LoopFrame()
        {
            if( _index + 1 == FrameCount())
            {
                _increment = -1;
            }
            else if(_index == 0)
            {
                _increment = 1;
                _animCount++;
            }

            _index += _increment;
        }

        /// <summary>
        /// Restart animation
        /// </summary>
        public void Restart()
        {
            _animCount = 0;

            switch (_type)
            {
                case AnimationType.LOOP:
                case AnimationType.NORMAL: _index = 0; return;
                case AnimationType.REVERSE: _index = FrameCount() - 1; return;
            }
        }
    }
}
