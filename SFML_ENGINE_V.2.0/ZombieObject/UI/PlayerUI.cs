using SFML.System;
using SFML.Window;
using SFML.Audio;
using SFML_Engine;
using System;

namespace ZombiesGame
{
    class PlayerUI : ScriptObject
    {
        Player _player;         //Reference to player

        public PlayerUI(Player p)
        {
            _player = p;

            _graphicHandler.GraphicState = GraphicState.UI;
        }

        public override void OnStart()
        {

        }

        public override void OnUpdate()
        {
            //Get frame
            _graphicHandler.Frame = new Frame();
            _graphicHandler.SetFrameAsCurrent();
            Frame ui = _graphicHandler.Frame;
            Vector2f viewOffset = GameState.Game.Window.GetView().Center - GameState.Game.Window.DefaultView.Center;

            //Add heart
            for (int i = 0; i < _player.Health; i++)
            {
                if (i > 2)
                {
                    ui.AddSprite(new Vector2f(i * 100 + 20, 20) + viewOffset, new Vector2f(16, 64), new Vector2f(100, 100), new Vector2f(16, 16));
                }
                else
                {
                    ui.AddSprite(new Vector2f(i * 100 + 20, 20) + viewOffset, new Vector2f(0, 64), new Vector2f(100, 100), new Vector2f(16, 16));
                }
            }

            //Add UI
            for (int i = 0; i < 10; i++)
            {
                ui.AddSprite(new Vector2f(i * 200, 880) + viewOffset, new Vector2f(64, 64), new Vector2f(200, 200), new Vector2f(16, 16));
            }

            //Add Weapon UI
            if (_player.Weapon != null)
            {
                if (_player.Weapon.GetType() == typeof(Pistol))
                {
                    ui.AddSprite(new Vector2f(250, 940) + viewOffset, new Vector2f(32, 0), new Vector2f(150, 150), new Vector2f(16, 16));
                }
                else if (_player.Weapon.GetType() == typeof(RifleAuto) || _player.Weapon.GetType() == typeof(RifleBurst))
                {
                    ui.AddSprite(new Vector2f(250, 940) + viewOffset, new Vector2f(64, 0), new Vector2f(150, 150), new Vector2f(16, 16));
                }
                else
                {
                    ui.AddSprite(new Vector2f(250, 940) + viewOffset, new Vector2f(48, 0), new Vector2f(150, 150), new Vector2f(16, 16));
                }
            }

            //Set text
            Renderer.RenderText("Ammo:", 42, new Vector2f(1480, 880) + viewOffset);
            Renderer.RenderText("Points:", 42, new Vector2f(800, 880) + viewOffset);
            Renderer.RenderText($"{_player.Money}", 42, new Vector2f(800, 990) + viewOffset);
            Renderer.RenderText("Current weapon:", 42, new Vector2f(40, 880) + viewOffset);

            //Weapon's ammunition
            if (_player.Weapon != null)
            {
                if (_player.Weapon.GetType() == typeof(Pistol))
                {
                    Renderer.RenderText($"{_player.Weapon.Ammo} / {_player.PistolBullet}", 45, new Vector2f(1400, 990) + viewOffset);
                }
                else if (_player.Weapon.GetType() == typeof(RifleAuto) || _player.Weapon.GetType() == typeof(RifleBurst))
                {
                    Renderer.RenderText($"{_player.Weapon.Ammo} / {_player.RifleBullet}", 45, new Vector2f(1400, 990) + viewOffset);
                }
                else
                {
                    Renderer.RenderText($"{_player.Weapon.Ammo} / {_player.Shell}", 45, new Vector2f(1400, 990) + viewOffset);
                }
            }
        }
    }
}