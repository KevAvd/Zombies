using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SFML_Engine
{
    abstract class GameState
    {
        List<GameObject> _gameObjects = new List<GameObject>();
        List<Collision> _collisions = new List<Collision>();
        string _name;
        Game _game;

        /// <summary>
        /// Get all game objects
        /// </summary>
        public GameObject[] Objects { get => _gameObjects.ToArray(); }

        /// <summary>
        /// Get all collisions
        /// </summary>
        public Collision[] Collisions { get => _collisions.ToArray(); }

        /// <summary>
        /// Get/Set name
        /// </summary>
        public string Name { get => _name; set => _name = value; }

        /// <summary>
        /// Get/Set game
        /// </summary>
        public Game Game { get => _game; set => _game = value; }

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
            obj.GameState = this;
            obj.GraphicHandler.SetDefaultSpriteToCurrent();
            if(obj.GetType().IsSubclassOf(typeof(ScriptObject)))
            {
                (obj as ScriptObject).OnStart();
            }
            _gameObjects.Add(obj);
        }

        /// <summary>
        /// Clear all collisions
        /// </summary>
        public void ClearCollisions()
        {
            _collisions.Clear();
        }

        /// <summary>
        /// Detect all collision in a game state
        /// </summary>
        /// <param name="state"></param>
        public void CheckCollision()
        {
            foreach (GameObject obj1 in _gameObjects)
            {
                if(obj1.PhysicObject == null || obj1.PhysicObject.State == PhysicState.NOCLIP) { continue; }

                foreach (GameObject obj2 in _gameObjects)
                {
                    if (obj2.PhysicObject == null || obj1 == obj2 || obj2.PhysicObject.State == PhysicState.NOCLIP) { continue; }

                    if (CollisionDetection.GAMEOBJ_GAMEOBJ(obj1, obj2, out Collision collision))
                    {
                        _collisions.Add(collision);
                    }
                }
            }
        }
    }
}
