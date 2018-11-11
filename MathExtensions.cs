using UnityEngine;

namespace UModules
{
    /// <summary>
    /// Extensions to Vector types
    /// </summary>
    public static class VectorExtensions
    {
        /// <summary>
        /// Return the result of scaling a vector along the x and/or y axes
        /// </summary>
        /// <access>public static Vector2</access>
        /// <param name="v" type="this Vector2">Input vector to scale</param>
        /// <param name="sx" type="float">X scale factor</param>
        /// <param name="sy" type="float">Y scale factor</param>
        /// <returns>A new Vector2 (v.x * sx, v.y * sy)</returns>
        public static Vector2 Scale(this Vector2 v, float sx = 1, float sy = 1)
        {
            return new Vector2(v.x * sx, v.y * sy);
        }
        /// <summary>
        /// Return the result of scaling a vector along the x and y axes
        /// </summary>
        /// <access>public static Vector2</access>
        /// <param name="v" type="this Vector2">Input vector to scale</param>
        /// <param name="s" type="Vector2">Scale vector</param>
        /// <returns>A new Vector2 (v.x * s.x, v.y * s.y)</returns>
        public static Vector2 Scale(this Vector2 v, Vector2 s)
        {
            return new Vector2(v.x * s.x, v.y * s.y);
        }
    }
}