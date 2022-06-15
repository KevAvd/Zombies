using System;
using System.Collections.Generic;
using System.Text;
using SFML.Graphics;
using SFML_Engine;

namespace ZombiesGame
{
    class LifeBonus : Buyable
    {
        protected override void Buyed()
        {
            _player.MaxHealth = 5;
            _player.Health = 5;
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
