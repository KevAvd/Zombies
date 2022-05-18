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
    class Player : Character
    {
        //Time
        float _timeAcc = 0;
        float _rifleCooldown = 0.100f;
        float _pistolCooldown = 0.3f;

        //Weapon property
        Weapon _weapon;

        //Ammos
        int _pistolAmmo = 14;
        int _rifleAmmo = 0;
        int _shotgunAmmo = 0;

        //Sprites
        GameSprite _Sprite_Pistol;
        GameSprite _Sprite_idle;
        GameSprite _Sprite_Rifle;

        //Animations
        Animation _Anim_PistolReload;
        Animation _Anim_RifleReload;

        //State
        PlayerState _playerState;

        //Enums
        public enum PlayerState
        {
            NORMAL,
            SPRINTING,
            RELOADING
        }

        /// <summary>
        /// Get/Set weapon
        /// </summary>
        public Weapon Weapon 
        { 
            get { return _weapon; }
            set
            {
                if(value.GetType() == typeof(Pistol))
                {
                    _graphicObject = _Sprite_Pistol;
                }
                else
                {
                    _graphicObject = _Sprite_Rifle;
                }
                _weapon = value;
            }
        }

        /// <summary>
        /// Get/Set pistol ammunition
        /// </summary>
        public int PistolAmmo { get => _pistolAmmo; set => _pistolAmmo = value; }

        /// <summary>
        /// Get/Set rifle ammunition
        /// </summary>
        public int RifleAmmo { get => _rifleAmmo; set => _rifleAmmo = value; }

        /// <summary>
        /// Get/Set shotgun ammunition
        /// </summary>
        public int ShotgunAmmo { get => _shotgunAmmo; set => _shotgunAmmo = value; }

        /// <summary>
        /// Get player state
        /// </summary>
        public PlayerState State { get => _playerState;}

        /// <summary>
        /// Constructor
        /// </summary>
        public Player()
        {
            //Set speed
            _speed = 500;
            _health = 5;

            //Set AABB
            _physicObject = new AABB(new Vector2f(500, 500), 100, 100);

            //Set sprites
            _Sprite_Pistol = new GameSprite(0, 0, 16, 16);
            _Sprite_Rifle = new GameSprite(16, 0, 16, 16);
            _Sprite_idle = new GameSprite(48, 48, 16, 16);

            //Set animations
            _Anim_PistolReload = new Animation(200);
            _Anim_PistolReload.AddFrame(0, 0, 16, 16);
            _Anim_PistolReload.AddFrame(0, 16, 16, 16);
            _Anim_PistolReload.AddFrame(0, 32, 16, 16);
            _Anim_PistolReload.AddFrame(0, 16, 16, 16);
            _Anim_RifleReload = new Animation(200);
            _Anim_RifleReload.AddFrame(16, 0, 16, 16);
            _Anim_RifleReload.AddFrame(16, 16, 16, 16);
            _Anim_RifleReload.AddFrame(16, 32, 16, 16);
            _Anim_RifleReload.AddFrame(16, 16, 16, 16);

            //Set graphic object
            _graphicObject = _Sprite_idle;

            //Set transformable
            _transformable.Position = new Vector2f(500, 500);
            _transformable.Rotation = 0;
            _transformable.Scale = new Vector2f(50, 50);
            _transformable.Origin = new Vector2f(0, 0);
        }

        public override void OnStart()
        {

        }

        public override void OnUpdate()
        {
            //Update time accumulator
            _timeAcc += GameTime.DeltaTimeU;

            //Move player
            _movement = new Vector2f(0, 0);
            if (Inputs.IsPressed(Keyboard.Key.W))
            {
                _movement += new Vector2f(0, -1);
            }
            if (Inputs.IsPressed(Keyboard.Key.A))
            {
                _movement += new Vector2f(-1, 0);
            }
            if (Inputs.IsPressed(Keyboard.Key.S))
            {
                _movement += new Vector2f(0, 1);
            }
            if (Inputs.IsPressed(Keyboard.Key.D))
            {
                _movement += new Vector2f(1, 0);
            }
            if (Inputs.IsPressed(Mouse.Button.Left) && _weapon != null && _graphicObject.GetType() == typeof(GameSprite))
            {
                _weapon.Shoot(Mouse.Button.Left);
            }
            if (Inputs.IsClicked(Keyboard.Key.R) && _weapon.Reload())
            {
                if (_weapon.GetType() == typeof(Pistol)) { _Anim_PistolReload.Restart(); _graphicObject = _Anim_PistolReload; }
                else { _Anim_RifleReload.Restart(); _graphicObject = _Anim_RifleReload; }
            }

            //Handle animation
            if(_graphicObject.GetType() == typeof(Animation) && (_graphicObject as Animation).Count >= 1)
            {
                if(_weapon.GetType() == typeof(Pistol))
                {
                    _graphicObject = _Sprite_Pistol;
                }
                else
                {
                    _graphicObject = _Sprite_Rifle;
                }
            }

            //UI
            if (_weapon != null)
            {
                if (_weapon.GetType() == typeof(Pistol))
                {
                    Renderer.RenderText($"{_weapon.Ammo} / {_pistolAmmo}", 45, new Vector2f(1300, 990));
                }
                else if (_weapon.GetType() == typeof(Rifle))
                {
                    Renderer.RenderText($"{_weapon.Ammo} / {_rifleAmmo}", 45, new Vector2f(1300, 990));
                }
                else
                {
                    Renderer.RenderText($"{_weapon.Ammo} / {_shotgunAmmo}", 45, new Vector2f(1300, 990));
                }
            }

            //Make player aim at mouse cursor
            Vector2f mousePos = Inputs.GetMousePosition(true);
            Rotation = MathF.Atan2(mousePos.Y - Position.Y, mousePos.X - Position.X);

            //Execute base methode
            base.OnUpdate();
        }
    }
}
