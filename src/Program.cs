using SFML.System;
using SFML.Graphics;
using SFML.Window;
using Zombies.Systems;
using Zombies.GameObjects.Entities;
using Zombies.GameObjects.Components;
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

//Create renderer
RenderStates rndrState = new RenderStates(new Texture(@"C:\Users\drimi\OneDrive\Bureau\Asset\Player.png"));
Renderer renderer = new Renderer(rndrState, window);

//Create Game state
Entity player = new Entity(0);
player.Components.Add(new AABB(player, 100, 100));
player.Components.Add(new GameSprite(player, 100, 100, new Vector2f(0, 0), new Vector2f(100, 0), new Vector2f(100, 100), new Vector2f(0, 100)));
player.Components.Add(new Position(player, 100, 100));
GameState state = new GameState();
state.Entities.Add(player);

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

    //Update game
    if(updateAcc >= updateRate)
    {
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