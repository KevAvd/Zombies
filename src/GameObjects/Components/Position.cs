using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zombies.GameObjects.Entities;

namespace Zombies.GameObjects.Components
{
    class Position : Component
    {
        //Property
        float _x;               //X axis position
        float _y;               //Y axis position
        float _heading;         //Heading angle in radian

        /// <summary>
        /// Get/Set X position
        /// </summary>
        public float X { get => _x; set => _x = value; }

        /// <summary>
        /// Get/Set Y position
        /// </summary>
        public float Y { get => _y; set => _y = value; }

        /// <summary>
        /// Get/Set heading angle (radian)
        /// </summary>
        public float Heading { get => _heading; set => _heading = value; }
    }
}
