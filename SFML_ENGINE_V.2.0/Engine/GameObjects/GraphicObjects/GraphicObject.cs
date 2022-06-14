using SFML.Graphics;

namespace SFML_Engine
{
    abstract class GraphicObject
    {
        /// <summary>
        /// Get vertices of graphic object
        /// </summary>
        /// <returns></returns>
        public abstract Vertex[] GetVertices();

        /// <summary>
        /// Add new frame
        /// </summary>
        /// <param name="x"> X position </param>
        /// <param name="y"> Y position </param>
        /// <param name="w"> Width </param>
        /// <param name="h"> Height </param>
        public abstract void AddSprite(float x, float y, float w, float h);
    }
}
