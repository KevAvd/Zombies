using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZombiesGame.Components;
using SFML.System;
using SFML.Graphics;

namespace ZombiesGame.GameObjects.Characters
{
    class Character : GameObject
    {
        //Character's property
        protected int _health;              //Health

        public int Health { get => _health; set => _health = value; }

        public override void Start()
        {

        }

        public override void Update()
        {

        }
    }
}
