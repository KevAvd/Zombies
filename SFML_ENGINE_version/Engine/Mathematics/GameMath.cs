﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;

namespace SFML_Engine.Mathematics
{
    static class GameMath
    {
        /// <summary>
        /// Return vector length
        /// </summary>
        /// <param name="vec"> vector </param>
        /// <returns> Length of vector </returns>
        public static float GetVectorLength(Vector2f vec)
        {
            return (float)Math.Sqrt(vec.X * vec.X + vec.Y * vec.Y);
        }

        /// <summary>
        /// Keep an angle in degree between 0 and 360
        /// </summary>
        /// <returns> Angle in degree </returns>
        public static float Stay360(float angle)
        {
            if(angle > 360)
            {
                angle -= 360; 
            }
            else if(angle < 0)
            {
                angle += 360;
            }

            return angle;
        }

        /// <summary>
        /// Keep an angle in radian between 0 and 2 PI
        /// </summary>
        /// <returns> Angle in radian </returns>
        public static float Stay2PI(float angle)
        {
            if(angle > Math.PI * 2)
            {
                angle -= (float)Math.PI * 2;
            }
            else if(angle < 0)
            {
                angle += (float)Math.PI * 2;
            }

            return angle;
        }

        /// <summary>
        /// Convert degree to radian
        /// </summary>
        /// <param name="degree"> Degree to convert </param>
        /// <returns> Radian </returns>
        public static float ToRadian(float degree)
        {
            return degree * (float)Math.PI / 180.0f;
        }

        /// <summary>
        /// Convert radian to degree 
        /// </summary>
        /// <param name="radian"> Radian to convert </param>
        /// <returns> Degree </returns>
        public static float ToDegree(float radian)
        {
            return radian * 180.0f / (float)Math.PI;
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
