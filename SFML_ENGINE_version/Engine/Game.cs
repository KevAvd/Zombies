using SFML.Graphics;
using SFML.Window;
using SFML_Engine.Systems;
using SFML_Engine.GameObjects;
using SFML_Engine.GameObjects.GraphicObjects;
using System.Reflection;
using System.Collections.Generic;
using System;
using System.Linq;

namespace SFML_Engine
{
    class Game
    {
        //Property
        RenderWindow _window;                               //Game's window
        GameState _state;                                   //Current game state
        GameState _nextState;                               //Next game state
        List<GameState> _states = new List<GameState>();    //All game state
        bool _changingState = false;                        //Indicate if the state is changing

        /// <summary>
        /// Constructor
        /// </summary>
        public Game()
        {
            _window = new RenderWindow(VideoMode.DesktopMode, "SFML.NET game !!!", Styles.Fullscreen);
        }

        /// <summary>
        /// Start the game
        /// </summary>
        public void Start()
        {
            //Frame rate property
            int fps = 0;
            int ups = 0;

            //Init systems
            Renderer.State = new RenderStates(new Texture(@"C:\Users\pq34bsi\Desktop\Zombies\Assets\SpriteSheet.png"));
            Renderer.Target = _window;
            Inputs.Window = _window;
            GameTime.SetFrameRate(144);
            GameTime.SetUpdateRate(200);

            //Instantiate all sub class of game state
            foreach (Type t in Assembly.GetExecutingAssembly().GetTypes())
            {
                if (t.IsSubclassOf(typeof(GameState)))
                {
                    GameState state = Activator.CreateInstance(t) as GameState;
                    state.Name = t.Name;
                    state.Game = this;
                    state.OnStart();
                    _states.Add(state);
                    if(state.Name.ToLower() == "main") { _state = state; }
                }
            }

            //Close game if there's no state
            if(_states.Count() == 0)
            {
                _window.Close();
            }

            //Check if state is null
            if(_state == null)
            {
                _state = _states[0];
            }

            //Start clock
            GameTime.StartClock();

            //Play start script of all script object
            foreach(ScriptObject sobj in _state.Objects)
            {
                sobj.OnStart();
            }

            //GameLoop
            while (_window.IsOpen)
            {
                //Get elapsed time
                GameTime.RestartClock();

                //Dispatch window's events
                _window.DispatchEvents();

                //Update game
                if (GameTime.DeltaTimeU >= GameTime.UpdateRate)
                {
                    //Update inputs
                    Inputs.Update();

                    //Update objects
                    foreach(GameObject obj in _state.Objects)
                    {
                        //Update animation
                        if(obj.GraphicObject.GetType() == typeof(Animation))
                        {
                            (obj.GraphicObject as Animation).Update();
                        }

                        //Update script
                        if (obj.GetType().IsSubclassOf(typeof(ScriptObject)))
                        {
                            (obj as ScriptObject).OnUpdate();
                        }
                    }

                    //Update state
                    _state.OnUpdate();

                    //Update per second
                    ups++;
                    GameTime.ResetUpdateAcc();
                }

                //Render game
                if (GameTime.DeltaTimeF >= GameTime.FrameRate)
                {
                    //Render all game object
                    Renderer.Render(_state.Objects);
                    _window.Display();

                    //Frame per second
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

                //Remove destroyed object
                _state.RemoveDestroyedObj();

                //Change state
                if (_changingState)
                {
                    _state = _nextState;
                    _changingState = false;
                }
            }
        }

        /// <summary>
        /// Change game state
        /// </summary>
        /// <param name="name"></param>
        public void ChangeState(string name)
        {
            foreach(GameState state in _states)
            {
                if (state.Name == name)
                {
                    _changingState = true;
                    _nextState = state;
                }
            }
        }
    }
}
