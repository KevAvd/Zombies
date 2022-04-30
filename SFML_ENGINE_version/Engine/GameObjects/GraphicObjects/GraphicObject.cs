using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;

namespace SFML_Engine.GameObjects.GraphicObjects
{
    abstract class GraphicObject
    {
        bool _background;

        /// <summary>
        /// Get/Set background
        /// </summary>
        public bool Background { get { return _background; } set { _background = value; } }

        public abstract Vertex[] GetVertices();
        public abstract void AddFrame(float x, float y, float w, float h);
    }
}
