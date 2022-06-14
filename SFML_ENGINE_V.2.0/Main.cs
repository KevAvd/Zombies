using System;
using System.Collections.Generic;
using System.Text;
using SFML_Engine;
using SFML.System;
using SFML.Window;

namespace ZombiesGame
{
    class Main : GameState
    {
        Player _player = new Player(new Vector2f(1920 / 2, 1080 / 2));

        /// <summary>
        /// Get player
        /// </summary>
        public Player Player { get => _player; }

        public override void OnStart()
        {
            AddGameObj(_player);
            AddGameObj(new Zombie(new Vector2f(1920 / 2, 1080 / 2)));
        }

        public override void OnUpdate()
        {
            if (Inputs.IsClicked(Keyboard.Key.K))
            {
                Renderer.TogglePhysObj();
            }
        }
    }
}
