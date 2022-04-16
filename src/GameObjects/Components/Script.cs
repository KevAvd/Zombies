using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zombies.GameObjects.Entities;
using Zombies.Enum;

namespace Zombies.GameObjects.Components
{
    abstract class Script : Component
    {
        public abstract void Start();

        public abstract void OnUpdate(float dt);

        public abstract EntityType GetEntityType();
    }
}
