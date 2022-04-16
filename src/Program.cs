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
InputHandler.GetInstance().Window = window;

//Create renderer
RenderStates rndrState = new RenderStates(new Texture(@"C:\Users\drimi\OneDrive\Bureau\Asset\Player.png"));
Renderer renderer = new Renderer(rndrState, window);

//Create an entity
Entity player = new Entity(0, EntityType.Player);
player.Components.Add(new AABB(100, 100));
player.Components.Add(new GameSprite(100, 100, new Vector2f(0, 0), new Vector2f(100, 0), new Vector2f(100, 100), new Vector2f(0, 100)));
player.Components.Add(new Position(100, 500));
player.LinkComponents();

//Create a game state
GameState state = new GameState();
state.Entities.Add(player);

//Load all scripts
//Get all types in the current executing assembly
Type[] types = Assembly.GetExecutingAssembly().GetTypes();

//Instantiate all sub class of Script
foreach (Type t in types)
{
    if (t.IsSubclassOf(typeof(Script)))
    {
        //Create instance of script
        Script script = (Script)Activator.CreateInstance(t);

        //Add script to compatible entities
        foreach(Entity entity in state.Entities)
        {
            if(entity.Type == script.GetEntityType())
            {
                entity.Components.Add(script);
                entity.LinkComponents();
            }
        }
    }
}

//Play script
state.PlayScriptStart();

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
    InputHandler.GetInstance().Update();

    //Update game
    if(updateAcc >= updateRate)
    {
        ups++;
        state.PlayScriptOnUpdate(updateAcc);
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