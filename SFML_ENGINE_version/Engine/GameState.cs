using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML_Engine.GameObjects;

namespace SFML_Engine
{
    abstract class GameState
    {
        List<GameObject> _gameObjects = new List<GameObject>();
        string _name;
        Game _game;

        /// <summary>
        /// Get all game objects
        /// </summary>
        public GameObject[] Objects { get => _gameObjects.ToArray(); }

        /// <summary>
        /// Get/Set name
        /// </summary>
        public string Name { get => _name; set => _name = value; }

        /// <summary>
        /// Get/Set game
        /// </summary>
        internal Game Game { get => _game; set => _game = value; }

        /// <summary>
        /// Executed on game start
        /// </summary>
        public abstract void OnStart();

        /// <summary>
        /// Executed every game update
        /// </summary>
        public abstract void OnUpdate();

        /// <summary>
        /// Remove all destroyed object
        /// </summary>
        public void RemoveDestroyedObj()
        {
            for(int i = _gameObjects.Count() - 1; i >= 0; i--)
            {
                if (_gameObjects[i].IsDestroyed())
                {
                    _gameObjects.RemoveAt(i);
                }
            }
        }

        /// <summary>
        /// Add a game object
        /// </summary>
        /// <param name="obj"></param>
        public void AddGameObj(GameObject obj)
        {
            obj.SetGameState(this);
            if(obj.GetType() == typeof(ScriptObject) || obj.GetType().IsSubclassOf(typeof(ScriptObject)))
            {
                (obj as ScriptObject).OnStart();
            }
            _gameObjects.Add(obj);
        }
    }
}
