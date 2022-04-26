using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;

namespace ZombiesGame.Mathematics
{
    static class LinearAlgebra
    {
        public static float GetVectorLength(Vector2f vec)
        {
            return (float)Math.Sqrt(vec.X * vec.X + vec.Y * vec.Y);
        }

        /// <summary>
        /// Scale a vector
        /// </summary>
        /// <param name="toScale"> Vector to scale </param>
        /// <param name="scalar"> Scalar </param>
        /// <returns> Scaled vector </returns>
        public static Vector2f ScaleVector(Vector2f toScale, Vector2f scalar)
        {
            return new Vector2f(toScale.X * scalar.X, toScale.Y * scalar.Y);
        }

        public static Vector2f VectorRotation(Vector2f vec, float angle)
        {
            return new Vector2f(vec.X * MathF.Cos(angle) - vec.Y * MathF.Sin(angle), vec.X * MathF.Sin(angle) + vec.Y * MathF.Cos(angle));
        }

        public static Vector2f NormalizeVector(Vector2f vec)
        {
            if(vec.X == 0 && vec.Y == 0)
            {
                return vec;
            }

            float d = MathF.Sqrt(vec.X * vec.X + vec.Y * vec.Y);
            return new Vector2f(vec.X / d, vec.Y / d);
        }
    }
}
