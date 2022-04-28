using SFML.System;
using SFML.Window;
using SFML.Audio;
using SFML_Engine.GameObjects.GraphicObjects;
using SFML_Engine.GameObjects.PhysicObjects;
using SFML_Engine.Systems;
using SFML_Engine.Mathematics;

namespace ZombiesGame
{
    class Player : Character
    {
        float _timeAcc = 0;
        float _weaponCooldown = 0.3f;
        Sound _gunShot = new Sound(new SoundBuffer(@"C:\Users\drimi\OneDrive\Bureau\Asset\Sounds\GunShot.wav"));

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

            //Set graphic object
            Animation animation = new Animation();
            animation.AddFrame(
                new Vector2f(0, 32),
                new Vector2f(16, 32),
                new Vector2f(16, 48),
                new Vector2f(0, 48)
            );
            animation.AddFrame(
                new Vector2f(0, 16),
                new Vector2f(16, 16),
                new Vector2f(16, 32),
                new Vector2f(0, 32)
            );
            _graphicObject = new GraphicObject("Switching", animation);

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
                _graphicObject.GetAnimation().NextFrame();
            }
            if (Inputs.IsClicked(Mouse.Button.Left) && _timeAcc >= _weaponCooldown)
            {
                _timeAcc = 0;
                Shoot();
            }

            //Make player aim at mouse cursor
            Vector2f mousePos = Inputs.GetMousePosition(true);
            Rotation = MathF.Atan2(mousePos.Y - Position.Y, mousePos.X - Position.X);

            //Execute base methode
            base.OnUpdate();
        }

        void Shoot()
        {
            _gunShot.Play();
            Ray shot = new Ray(Position, Inputs.GetMousePosition(true) - Position, 4000);
            List<Vector2f> veclist = new List<Vector2f>();
            foreach(Character z in GetGameState().Objects)
            {
                if(z.GetType() != typeof(Zombie))
                {
                    continue;
                }

                if(CollisionDetection.AABB_RAY(z.PhysicObject as AABB, shot, out Vector2f pNear, out Vector2f pFar, out Vector2f normal))
                {
                    z.Velocity = LinearAlgebra.NormalizeVector(pNear - Position) * 500;
                    z.Health -= 20;
                }
            }
        }
    }
}
