using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFML_Engine.GameObjects
{
    abstract class ScriptObject : GameObject
    {
        public abstract void OnStart();
        public abstract void OnUpdate();
    }
}
