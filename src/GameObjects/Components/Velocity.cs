using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
using Zombies.GameObjects.Entities;
namespace Zombies.GameObjects.Components
{
    class Velocity : Component
    {
        //Property
        float _vx;                                              //X axis velocity
        float _vy;                                              //Y axis velocity

        /// <summary>
        /// Get/Set vx
        /// </summary>
        public float X { get => _vx; set => _vx = value; }
        /// <summary>
        /// Get/Set vy
        /// </summary>
        public float Y { get => _vy; set => _vy = value; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="entity"> Component's entity </param>
        public Velocity(Entity entity) : base(entity) 
        {
            _vx = 0;
            _vy = 0;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="entity"> Component's velocity </param>
        /// <param name="vx"> X axis velocity </param>
        /// <param name="vy"> Y axis velocity </param>
        public Velocity(Entity entity, float vx, float vy) : base(entity)
        {
            _vx = vx;
            _vy = vy;
        }
    }
}
