using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZombiesGame.GraphicObjects
{
    class GraphicObject
    {
        //Property
        Dictionary<string, Animation> _animations = new Dictionary<string, Animation>(); //All animations
        string currentAnim;                                                              //Keeps tracks of current animation

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name"> Animation name </param>
        /// <param name="anim"> Animation </param>
        public GraphicObject(string name, Animation anim)
        {
            currentAnim = name;
            AddAnimation(name, anim);
        }

        /// <summary>
        /// Add an animation
        /// </summary>
        /// <param name="name"> Animation name </param>
        /// <param name="anim"> Animation </param>
        public void AddAnimation(string name, Animation anim)
        {
            _animations.Add(name, anim);
        }

        /// <summary>
        /// Set the current animation
        /// </summary>
        /// <param name="name"> Animation name </param>
        public void SetAnimation(string name)
        {
            if (_animations.ContainsKey(name))
            {
                currentAnim = name;
            }
        }

        /// <summary>
        /// Get the current animation
        /// </summary>
        /// <returns> Animation </returns>
        public Animation GetAnimation()
        {
            return _animations[currentAnim];
        }
    }
}
