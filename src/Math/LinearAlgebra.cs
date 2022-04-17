using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;

namespace Zombies.Math
{
    static class LinearAlgebra
    {
        public static Vector2f VectorRotation(Vector2f vec, float angle)
        {
            return new Vector2f(vec.X * MathF.Cos(angle) - vec.Y * MathF.Sin(angle), vec.X * MathF.Sin(angle) + vec.Y * MathF.Cos(angle));
        }

        public static Vector2f NormalizeVector(Vector2f vec)
        {
            float d = MathF.Sqrt(vec.X * vec.X + vec.Y * vec.Y);
            return new Vector2f(vec.X / d, vec.Y / d);
        }
    }
}
