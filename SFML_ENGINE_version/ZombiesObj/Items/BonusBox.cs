using System;
using System.Collections.Generic;
using System.Text;
using SFML_Engine.GameObjects;
using System;
using System.Collections.Generic;
using System.Text;
using SFML_Engine.GameObjects;
using SFML_Engine.Systems;
using SFML_Engine.Enums;
using SFML_Engine.Mathematics;
using SFML_Engine.GameObjects.PhysicObjects;
using SFML_Engine.GameObjects.GraphicObjects;
using SFML.System;
using SFML.Window;
using SFML.Audio;

namespace ZombiesGame
{
    abstract class BonusBox : ScriptObject
    {
        //Player
        protected Player _player;             //Reference to player

        //BonusBox
        protected int _price;                 //Bonus price

        public override void OnStart()
        {
            foreach (GameObject obj in GetGameState().Objects)
            {
                if (obj.GetType() == typeof(Player))
                {
                    _player = obj as Player;
                    break;
                }
            }
        }

        public override void OnUpdate()
        {
            if(CollisionDetection.AABB_AABB(_physicObject as AABB, _player.PhysicObject as AABB) && Inputs.IsClicked(Keyboard.Key.E))
            {
                Bonus();
            }
        }

        /// <summary>
        /// Activate the bonus
        /// </summary>
        protected abstract void Bonus();
    }
}
