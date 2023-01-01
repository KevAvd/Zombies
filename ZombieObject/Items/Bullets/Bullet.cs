using System;
using System.Collections.Generic;
using System.Text;
using SFML_Engine;
using SFML.System;
using SFML.Graphics;

namespace ZombiesGame
{
    class Bullet : GameObject
    {
        int _dammage;

        /// <summary>
        /// Get dammage
        /// </summary>
        public int Dammage { get => _dammage;}

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="dir"></param>
        /// <param name="range"></param>
        /// <param name="dammage"></param>
        public Bullet(Vector2f pos, Vector2f dir, float range, int dammage)
        {
            _physicObject = new Ray(pos, dir, range);
            _dammage = dammage;
        }
    }
}
