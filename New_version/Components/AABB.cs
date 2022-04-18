using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZombiesGame.Components
{
    internal class AABB
    {
        //Property
        float _width;                                                       //Box collider width
        float _height;                                                      //Box collider height

        /// <summary>
        /// Get/Set width
        /// </summary>
        public float Width { get => _width; set => _width = value; }

        /// <summary>
        /// Get/Set height
        /// </summary>
        public float Height { get => _height; set => _height = value; }     //Get-Set height

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="w"> Box collider width </param>
        /// <param name="h"> Box collider height </param>
        public AABB(float w, float h)
        {
            _width = w;
            _height = h;
        }
    }
}
