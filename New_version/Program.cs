using System;
using System.Collections.Generic;
using System.Reflection;
using SFML.System;
using SFML.Graphics;
using SFML.Window;
using ZombiesGame.Systems;
using ZombiesGame.GameObjects;
using ZombiesGame.PhysicObjects;
using ZombiesGame.GameObjects.Characters;

//Window's resolution
uint xResolution = 1920;
uint yResolution = 1080;

//Frame rate property
int fps = 0;

//Update rate property
int ups = 0;

//Create window
RenderWindow window = new RenderWindow(new VideoMode(xResolution, yResolution), "Zombies");

//Init systems
Renderer.State = new RenderStates(new Texture(@"C:\Users\drimi\OneDrive\Bureau\Asset\Player.png"));
Renderer.Target = window;
Inputs.Window = window;
GameTime.SetFrameRate(144);
GameTime.SetUpdateRate(200);

//Creates GameObjects
List<GameObject> gameObjects = new List<GameObject>();
Player player = new Player();
gameObjects.Add(player);
gameObjects.Add(new Zombie(player, 0, 0));
gameObjects.Add(new Zombie(player, 1920, 1080));
gameObjects.Add(new Zombie(player, 1920, 0));
gameObjects.Add(new Zombie(player, 0, 1080));

//Start clock
GameTime.StartClock();

//Call start
foreach (GameObject obj in gameObjects)
{
    obj.Start();
}

//GameLoop
while (window.IsOpen)
{
    //Get elapsed time
    GameTime.RestartClock();

    //Dispatch window's events
    window.DispatchEvents();

    //Update game
    if (GameTime.DeltaTimeU >= GameTime.UpdateRate)
    {
        //Update inputs
        Inputs.Update();
        OnUpdate();
        ups++;
        GameTime.ResetUpdateAcc();
    }

    //Render game
    if (GameTime.DeltaTimeF >= GameTime.FrameRate)
    {
        OnRender();
        window.Display();
        fps++;
        GameTime.ResetFrameAcc();
    }

    //Reset accumulator
    if (GameTime.Accumulator >= 1)
    {
        Console.SetCursorPosition(0, 0);
        Console.WriteLine($"FPS: {fps}\nUPS: {ups}");
        GameTime.ResetAccumulator();
        fps = 0;
        ups = 0;
    }
}

void OnUpdate()
{
    foreach (GameObject obj in gameObjects)
    {
        obj.Update();
    }

    if (Inputs.IsClicked(Keyboard.Key.K))
    {
        Renderer.ToggleAABB();
    }

    foreach (GameObject obj1 in gameObjects)
    {
        foreach (GameObject obj2 in gameObjects)
        {
            if (obj1.Equals(obj2))
            {
                continue;
            }

            if (CollisionDetection.AABB_AABB(obj1.AABB, obj2.AABB))
            {
                Ray ray = new Ray(obj1.Transformable.Position, obj2.Transformable.Position);
                CollisionDetection.AABB_RAY(obj1.AABB, ray, out Vector2f pNear1, out Vector2f pFar1, out Vector2f normal1);
                CollisionDetection.AABB_RAY(obj2.AABB, ray, out Vector2f pNear2, out Vector2f pFar2, out Vector2f normal2);
                Vector2f toMove = pNear2 - pFar1;
                toMove /= 2f;
                obj1.Transformable.Position += toMove;
                obj2.Transformable.Position -= toMove;
                obj1.AABB.UpdatePosition(obj1.Transformable.Position);
                obj2.AABB.UpdatePosition(obj2.Transformable.Position);
            }
        }
    }
}

void OnRender()
{
    Renderer.Render(gameObjects.ToArray());
}