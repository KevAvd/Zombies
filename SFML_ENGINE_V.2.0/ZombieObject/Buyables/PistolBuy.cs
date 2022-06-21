using System;
using System.Collections.Generic;
using System.Text;
using SFML.Graphics;
using SFML.System;
using SFML_Engine;

namespace ZombiesGame
{
    class PistolBuy : Buyable
    {
        public PistolBuy(Vector2f pos)
        {
            //Set price
            _price = 1000;
            _maxNbrOfBuy = 10;

            //Set graphic handler
            _graphicHandler.AddGraphicObject("Base", new GameSprite(32, 80, 16, 16));
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
            _player.SwitchWeapon(new Pistol());
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
