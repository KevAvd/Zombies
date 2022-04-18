using System;
using System.Collections.Generic;
using System.Reflection;
using SFML.System;
using SFML.Graphics;
using SFML.Window;
using ZombiesGame.Systems;
using ZombiesGame.GameObjects;
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

//Create renderer
RenderStates rndrState = new RenderStates(new Texture(@"C:\Users\drimi\OneDrive\Bureau\Asset\Player.png"));
Renderer renderer = new Renderer(rndrState, window);

//Creates GameObjects
List<GameObject> gameObjects = new List<GameObject>();
Player player = new Player();
gameObjects.Add(player);
gameObjects.Add(new Zombie(player, 0, 0));
gameObjects.Add(new Zombie(player, 1920, 1080));
gameObjects.Add(new Zombie(player, 1920, 0));
gameObjects.Add(new Zombie(player, 0, 1080));

//Init Inputs
Inputs.Window = window;

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

    //Update Inputs
    Inputs.Update();

    //Update game
    if (GameTime.UpdateDeltaTime >= GameTime.UpdateRate)
    {
        foreach(GameObject obj in gameObjects)
        {
            obj.Update();
        }
        ups++;
        GameTime.ResetUpdateAcc();
    }

    //Render game
    if (GameTime.FrameDeltaTime >= GameTime.FrameRate)
    {
        renderer.Render(gameObjects.ToArray());
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