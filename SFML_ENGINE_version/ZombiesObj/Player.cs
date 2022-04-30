using SFML.System;
using SFML.Window;
using SFML.Audio;
using SFML_Engine.GameObjects;
using SFML_Engine.GameObjects.GraphicObjects;
using SFML_Engine.GameObjects.PhysicObjects;
using SFML_Engine.Systems;
using SFML_Engine.Mathematics;

namespace ZombiesGame
{
    class Player : Character
    {
        float _timeAcc = 0;
        float _rifleCooldown = 0.100f;
        float _pistolCooldown = 0.3f;
        Sound _gunShot = new Sound(new SoundBuffer(@"C:\Users\drimi\OneDrive\Bureau\Asset\Sounds\GunShot.wav"));
        bool _rifle = false;

        //Sprites
        GameSprite _Sprite_Pistol;
        GameSprite _Sprite_Rifle;

        /// <summary>
        /// Constructor
        /// </summary>
        public Player()
        {
            //Set speed
            _speed = 500;
            _health = 500;

            //Set AABB
            _physicObject = new AABB(new Vector2f(500, 500), 100, 100);

            //Set sprites
            _Sprite_Pistol = new GameSprite(0, 16, 16, 16);
            _Sprite_Rifle = new GameSprite(16, 16, 16, 16);

            //Set graphic object
            _graphicObject = _Sprite_Pistol;

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
            if (Inputs.IsClicked(Keyboard.Key.Q))
            {
                if (_rifle)
                {
                    _graphicObject = _Sprite_Pistol;
                    _rifle = false;
                }
                else
                {
                    _graphicObject = _Sprite_Rifle;
                    _rifle = true;
                }
            }
            if (_rifle)
            {
                if (Inputs.IsPressed(Mouse.Button.Left) && _timeAcc >= _rifleCooldown)
                {
                    _timeAcc = 0;
                    Shoot();
                }
            }
            else
            {
                if (Inputs.IsClicked(Mouse.Button.Left) && _timeAcc >= _pistolCooldown)
                {
                    _timeAcc = 0;
                    Shoot();
                }
            }

            //Make player aim at mouse cursor
            Vector2f mousePos = Inputs.GetMousePosition(true);
            Rotation = MathF.Atan2(mousePos.Y - Position.Y, mousePos.X - Position.X);

            //Execute base methode
            base.OnUpdate();
        }

        void Shoot()
        {
            //Play gun shot sound
            _gunShot.Play();

            //Add muzzle flash
            if (_rifle) { GetGameState().AddGameObj(new MuzzleFlash(this,100, -25)); }
            else { GetGameState().AddGameObj(new MuzzleFlash(this, 100, 0)); }

            //Detect shot collision
            Ray shot = new Ray(Position, Inputs.GetMousePosition(true) - Position, 4000);
            List<Vector2f> veclist = new List<Vector2f>();
            foreach(GameObject obj in GetGameState().Objects)
            {
                if(obj.GetType() != typeof(Zombie))
                {
                    continue;
                }

                if(CollisionDetection.AABB_RAY(obj.PhysicObject as AABB, shot, out Vector2f pNear, out Vector2f pFar, out Vector2f normal))
                {
                    (obj as Zombie).Velocity = LinearAlgebra.NormalizeVector(pNear - Position) * 500;
                    if (_rifle)
                    {
                        (obj as Zombie).Health -= 25;
                    }
                    else
                    {
                        (obj as Zombie).Health -= 20;
                    }
                }
            }
        }
    }
}
