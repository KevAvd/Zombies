using System;
using System.Collections.Generic;
using System.Reflection;
using SFML.System;
using SFML.Graphics;
using SFML.Window;
using Zombies.Systems;
using Zombies.GameObjects.Entities;
using Zombies.GameObjects.Components;
using Zombies.Enum;
using Zombies;

//Window's resolution
uint xResolution = 1920;
uint yResolution = 1080;

//Gameloop's variables
Clock clk = new Clock();
float deltaTime = 0;
float accumulator = 0;

//Frame rate property
int fps = 0;
float frameRate = 1f/144f;
float frameAcc = 0;

//Update rate property
int ups = 0;
float updateRate = 1f/200f;
float updateAcc = 0;

//Create window
RenderWindow window = new RenderWindow(new VideoMode(xResolution, yResolution), "Zombies");

//Init singleton InputHandler
Inputs.Window = window;

//Create renderer
RenderStates rndrState = new RenderStates(new Texture(@"C:\Users\drimi\OneDrive\Bureau\Asset\Player.png"));
Renderer renderer = new Renderer(rndrState, window);

//Create game state
GameState state = new GameState();

//Load Scripts
Type[] types = Assembly.GetExecutingAssembly().GetTypes();
foreach (Type t in types)
{
    if (t.IsSubclassOf(typeof(Script)))
    {
        state.CreateEntity((Activator.CreateInstance(t) as Script).GetUsedComponents());
    }
}

//Play start script
state.PlayStartScript();

//GameLoop
while (window.IsOpen)
{
    //Get elapsed time
    deltaTime = clk.Restart().AsSeconds();

    //Update accumulators
    accumulator += deltaTime;
    frameAcc += deltaTime;
    updateAcc += deltaTime;

    //Dispatch window's events
    window.DispatchEvents();

    //Update key input
    Inputs.Update();

    //Update game
    if(updateAcc >= updateRate)
    {
        state.PlayOnUpdateScript(updateAcc);
        ups++;
        updateAcc = 0;
    }

    //Render game
    if(frameAcc >= frameRate)
    {
        renderer.Render(state);
        window.Display();
        fps++;
        frameAcc = 0;
    }

    //Reset accumulator
    if(accumulator >= 1)
    {
        Console.SetCursorPosition(0, 0);
        Console.WriteLine($"FPS: {fps}\nUPS: {ups}");
        accumulator = 0;
        fps = 0;
        ups = 0;
    }
}