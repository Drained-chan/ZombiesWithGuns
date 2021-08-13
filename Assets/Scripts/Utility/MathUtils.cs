using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MathUtils
{
    /// <summary>
    /// Math operations for modifying and creating vectors.
    /// </summary>
    public static class Vector
    {
        /// <summary>
        /// Given a rotation, returns a unit vector pointing in the same direction.
        /// </summary>
        /// <param name="rotation">Rotation to point along.</param>
        /// <returns>Unit vector pointing the same way as <paramref name="rotation"/>.</returns>
        public static Vector3 FromRotation(Quaternion rotation)
        {
            return rotation * Vector3.forward;
        }

        /// <summary>
        /// Given a rotation, returns a unit vector pointing in the same direction.
        /// </summary>
        /// <param name="rotation">Euler representation of the angle to point along.</param>
        /// <returns>Unit vector pointing the same way as <paramref name="rotation"/>.</returns>
        public static Vector3 FromRotation(Vector3 rotation)
        {
            return FromRotation(Quaternion.Euler(rotation));
        }
    }


    /// <summary>
    /// Math operations for modifying and creating rotations.
    /// </summary>
    public static class Rotation
    {
        /// <summary>
        /// Given a vector, returns a rotation oriented the same way.
        /// </summary>
        /// <param name="vector">Vector pointing in the direction the rotation should point.</param>
        /// <returns>A quaternion representation of the rotation pointing the same way as <paramref name="vector"/>.</returns>
        public static Quaternion FromVector(Vector2 vector)
        {
            return Quaternion.Euler(0, 0, Mathf.Atan2(vector.y, vector.x) * Mathf.Rad2Deg);
        }
    }
}