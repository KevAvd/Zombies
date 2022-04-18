using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
using Zombies.GameObjects.Entities;

namespace Zombies.GameObjects.Components
{
    class GameSprite : Component
    {
        //Property
        float _width;                                                   //Sprite width
        float _height;                                                  //Sprite height
        Vector2f[] _txtCoords = new Vector2f[4];                        //Texture coordinates

        /// <summary>
        /// Get/Set width
        /// </summary>
        public float Width { get => _width; set => _width = value; }

        /// <summary>
        /// Get/Set height
        /// </summary>
        public float Height { get => _height; set => _height = value; }

        /// <summary>
        /// Get/Set texture coordinates
        /// </summary>
        public Vector2f[] TxtCoords { get => _txtCoords; set => _txtCoords = value; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="w"> Sprite width </param>
        /// <param name="h"> Sprite height </param>
        /// <param name="txtCoords1"> Texture coordinates 1 </param>
        /// <param name="txtCoords2"> Texture coordinates 2 </param>
        /// <param name="txtCoords3"> Texture coordinates 3 </param>
        /// <param name="txtCoords4"> Texture coordinates 4 </param>
        public GameSprite(float w, float h, Vector2f txtCoords1, Vector2f txtCoords2, Vector2f txtCoords3, Vector2f txtCoords4)
        {
            _width = w;
            _height = h;
            _txtCoords[0] = txtCoords1;
            _txtCoords[1] = txtCoords2;
            _txtCoords[2] = txtCoords3;
            _txtCoords[3] = txtCoords4;
        }
    }
}
