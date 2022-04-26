using System;
using System.Collections.Generic;
using System.Reflection;
using SFML.System;
using SFML.Graphics;
using SFML.Window;
using ZombiesGame.Enums;
using ZombiesGame.Systems;
using ZombiesGame.Mathematics;
using ZombiesGame.GameObjects;
using ZombiesGame.PhysicObjects;
using ZombiesGame.GameObjects.Characters;
using ZombiesGame.GameObjects.Items;

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
Renderer.State = new RenderStates(new Texture(@"C:\Users\drimi\OneDrive\Bureau\Asset\test.png"));
Renderer.Target = window;
Inputs.Window = window;
GameTime.SetFrameRate(144);
GameTime.SetUpdateRate(200);

//Creates GameObjects
Inventory inventory = new Inventory();
inventory.Firearm1 = new AK47();
inventory.Firearm2 = new M1911();
inventory.Melee = new Knife();
List<GameObject> gameObjects = new List<GameObject>();
Player player = new Player();
gameObjects.Add(player);
gameObjects.Add(new Zombie(0, 0));
gameObjects.Add(new Zombie(1920, 1080));
gameObjects.Add(new Zombie(1920, 0));
gameObjects.Add(new Zombie(0, 1080));

//Temp variable
int nbrOfZombieToSpawn = 4;
int zombieCount = 0;

//Start clock
GameTime.StartClock();

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
    if (Inputs.IsClicked(Keyboard.Key.K))
    {
        Renderer.ToggleAABB();
    }

    
    foreach(GameObject obj in gameObjects)
    {
        //Updates animations
        obj.GraphicObject.GetAnimation().Update();

        //Update game objects
        if (obj.GetType() == typeof(Zombie))
        {
            zombieCount++;
            HandleZombie((Zombie)obj, player);
        }
        else if (obj.GetType() == typeof(Player))
        {
            HandlePlayer((Player)obj);
        }

        //Handles collision
        if (obj.GetType().IsSubclassOf(typeof(Character)))
        {
            foreach(Character c in gameObjects)
            {
                if (obj.Equals(c))
                {
                    continue;
                }

                if (CollisionDetection.AABB_AABB((AABB)obj.PhysicObject, (AABB)c.PhysicObject))
                {
                    Ray ray = new Ray(obj.Transformable.Position, c.Transformable.Position);
                    CollisionDetection.AABB_RAY((AABB)obj.PhysicObject, ray, out Vector2f pNear1, out Vector2f pFar1, out Vector2f normal1);
                    CollisionDetection.AABB_RAY((AABB)c.PhysicObject, ray, out Vector2f pNear2, out Vector2f pFar2, out Vector2f normal2);
                    Vector2f toMove = pNear2 - pFar1;
                    toMove /= 2f;
                    obj.Position += toMove;
                    c.Position -= toMove;
                }
            }
        }
    }

    //Remove all dead
    for(int i = gameObjects.Count - 1; i >= 0; i--)
    {
        if (gameObjects[i].GetType().IsSubclassOf(typeof(Character)) && (gameObjects[i] as Character).State == ObjectState.DEAD)
        {
            gameObjects.Remove(gameObjects[i]);
        }
    }
    
    if(zombieCount == 0)
    {
        nbrOfZombieToSpawn+= 10;
        for(int i = 0; i < nbrOfZombieToSpawn; i++)
        {
            gameObjects.Add(new Zombie(i * 100 + 50, 50));
        }
    }

    zombieCount = 0;
}

void OnRender()
{
    Renderer.Render(gameObjects.ToArray());
}

void HandlePlayer(Player p)
{
    //Move player
    p.Movement = new Vector2f(0, 0);
    if (Inputs.IsPressed(Keyboard.Key.W))
    {
        p.Movement += new Vector2f(0, -1);
    }
    if (Inputs.IsPressed(Keyboard.Key.A))
    {
        p.Movement += new Vector2f(-1, 0);
    }
    if (Inputs.IsPressed(Keyboard.Key.S))
    {
        p.Movement += new Vector2f(0, 1);
    }
    if (Inputs.IsPressed(Keyboard.Key.D))
    {
        p.Movement += new Vector2f(1, 0);
    }

    p.Movement = LinearAlgebra.NormalizeVector(p.Movement);
    p.Position += p.Movement * p.Speed * GameTime.DeltaTimeU;

    //Make player aim at mouse cursor
    Vector2f mousePos = Inputs.GetMousePosition(true);
    p.Rotation = MathF.Atan2(mousePos.Y - p.Position.Y, mousePos.X - p.Position.X) + (99 * 180 / MathF.PI);

    //Handles weapon attack
    if (Inputs.IsClicked(Mouse.Button.Left))
    {
        Weapon weapon = inventory.GetCurrentWeapon();
        Ray ray = new Ray(p.Position, mousePos - p.Position, weapon.Range);
        Renderer.DrawRay(ray, Color.Red);
        foreach(Character c in gameObjects)
        {
            if (p.Equals(c))
            {
                continue;
            }

            if(CollisionDetection.AABB_RAY((AABB)c.PhysicObject, ray, out Vector2f pNear, out Vector2f pFar, out Vector2f normal))
            {
                c.State = ObjectState.DEAD;
            }
        }
    }
}

void HandleZombie(Zombie z, Player p)
{
    //Move zombie
    z.Movement = LinearAlgebra.NormalizeVector(p.Position - z.Position);
    z.Position += z.Movement * z.Speed * GameTime.DeltaTimeU;

    //Make zombie aim at player
    Vector2f playerVec = new Vector2f(p.Position.X, p.Position.Y);
    z.Rotation = MathF.Atan2(playerVec.Y - z.Position.Y, playerVec.X - z.Position.X) + (99 * 180 / MathF.PI);

}