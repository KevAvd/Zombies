using System;
using System.Collections.Generic;
using System.Text;
using SFML.Graphics;
using SFML.System;
using SFML_Engine;

namespace ZombiesGame
{
    class SpeedBonus : Buyable
    {
        public SpeedBonus(Vector2f pos)
        {
            //Set price
            _price = 2500;
            _maxNbrOfBuy = 1;

            //Set graphic handler
            _graphicHandler.AddGraphicObject("Base", new GameSprite(0, 80, 16, 16));
            _graphicHandler.SetDefaultSprite("Base");

            //Set physic object
            _physicObject = new AABB(pos, 150, 150);

            //Set transformable
            Position = pos;
            Rotation = 0;
            Origin = new Vector2f(0, 0);
            Scale = new Vector2f(50, 50);
        }

        protected override void Buyed()
        {
            _player.NormalSpeed = 1200;
            _player.SlowSpeed = 800;
            _player.SwitchState(Player.PlayerState.NORMAL);
        }

        protected override void CantAfford()
        {
            Text text = new Text("Not enought money", Renderer.MainFont, 20);
            text.FillColor = Color.Red;
            text.OutlineColor = Color.Black;
            text.OutlineThickness = 5;
            Renderer.RenderText(text);
        }
    }
}
