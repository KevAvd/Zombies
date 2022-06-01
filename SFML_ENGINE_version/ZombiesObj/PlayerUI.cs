using SFML.System;
using SFML.Window;
using SFML.Audio;
using SFML_Engine.GameObjects;
using SFML_Engine.GameObjects.GraphicObjects;
using SFML_Engine.GameObjects.PhysicObjects;
using SFML_Engine.Systems;
using SFML_Engine.Mathematics;
using System;
using System.Collections.Generic;

namespace ZombiesGame
{
    class PlayerUI : ScriptObject
    {
        Player _player;         //Reference to player

        public PlayerUI(Player p)
        {
            _player = p;

            Frame ui = new Frame();
            _graphicObject = ui;
        }

        public override void OnStart()
        {

        }

        public override void OnUpdate()
        {
            //Create new frame
            Frame ui = new Frame();

            //Add heart
            for (int i = 0; i < _player.Health; i++)
            {
                ui.AddSprite(new Vector2f(i * 100 + 20, 20), new Vector2f(0, 64), new Vector2f(100, 100), new Vector2f(16, 16));
            }

            //Add UI
            for (int i = 0; i < 10; i++)
            {
                ui.AddSprite(new Vector2f(i * 200, 880), new Vector2f(64, 64), new Vector2f(200, 200), new Vector2f(16, 16));
            }

            //Add Weapon UI
            if (_player.Weapon != null)
            {
                if (_player.Weapon.GetType() == typeof(Pistol))
                {
                    ui.AddSprite(new Vector2f(250, 940), new Vector2f(32, 0), new Vector2f(150, 150), new Vector2f(16, 16));
                }
                else if (_player.Weapon.GetType() == typeof(Rifle))
                {
                    ui.AddSprite(new Vector2f(250, 940), new Vector2f(64, 0), new Vector2f(150, 150), new Vector2f(16, 16));
                }
                else
                {
                    ui.AddSprite(new Vector2f(250, 940), new Vector2f(48, 0), new Vector2f(150, 150), new Vector2f(16, 16));
                }
            }

            //Set graphic object
            _graphicObject = ui;
            _graphicObject.State = SFML_Engine.Enums.GraphicState.UI;

            //Set text
            Renderer.RenderText("Ammo:", 42, new Vector2f(1480, 880));
            Renderer.RenderText("Current weapon:", 42, new Vector2f(40, 880));

            //Weapon's ammunition
            if (_player.Weapon != null)
            {
                if (_player.Weapon.GetType() == typeof(Pistol))
                {
                    Renderer.RenderText($"{_player.Weapon.Ammo} / {_player.PistolAmmo}", 45, new Vector2f(1400, 990));
                }
                else if (_player.Weapon.GetType() == typeof(Rifle))
                {
                    Renderer.RenderText($"{_player.Weapon.Ammo} / {_player.RifleAmmo}", 45, new Vector2f(1400, 990));
                }
                else
                {
                    Renderer.RenderText($"{_player.Weapon.Ammo} / {_player.ShotgunAmmo}", 45, new Vector2f(1400, 990));
                }
            }
        }
    }
}
