using System;
using System.Collections.Generic;
using System.Text;
using SFML_Engine;
using SFML.System;

namespace ZombiesGame
{
    class MuzzleFlash : ScriptObject
    {
        float _timeAcc = 0;
        const float _flashDuration = 0.02f;

        public MuzzleFlash(GameObject relative, float x, float y)
        {
            //Set relative
            Relative = relative;

            //Set physic obj
            _physicObject = new AABB(new Vector2f(0, 0), 0, 0);
            _physicObject.State = PhysicState.NOCLIP;

            //Set graphic object
            _graphicHandler.AddGraphicObject("Flash", new GameSprite(64, 16, 16, 16));
            _graphicHandler.SetDefaultSprite("Flash");

            //Set transformable
            Position = new Vector2f(x, y);
            Rotation = 0;
            Scale = new Vector2f(50, 50);
            Origin = new Vector2f(0, 0);
        }

        public override void OnStart()
        {

        }

        public override void OnUpdate()
        {
            _timeAcc += GameTime.DeltaTimeU;

            if (_timeAcc >= _flashDuration)
            {
                Destroy();
            }
        }
    }
}
