using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
using SFML_Engine.GameObjects;
using SFML_Engine.GameObjects.GraphicObjects;
using SFML_Engine.Systems;

namespace ZombiesGame
{
    class MuzzleFlash : ScriptObject
    {
        float _timeAcc = 0;
        const float _flashDuration = 0.02f;

        //Sprite
        GameSprite _sprite_Flash;

        public MuzzleFlash(GameObject relative, float x, float y)
        {
            //Set relative
            Relative = relative;

            //Set sprite
            _sprite_Flash = new GameSprite(64, 16, 16, 16);

            //Set graphic object
            _graphicObject = _sprite_Flash;

            //Set transformable
            _transformable.Position = new Vector2f(x, y);
            _transformable.Rotation = 0;
            _transformable.Scale = new Vector2f(50, 50);
            _transformable.Origin = new Vector2f(0, 0);
        }

        public override void OnStart()
        {
            
        }

        public override void OnUpdate()
        {
            _timeAcc += GameTime.DeltaTimeU;

            if(_timeAcc >= _flashDuration)
            {
                Destroy();
            }
        }
    }
}
