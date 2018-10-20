/*
    UModules::FloatAsset

    by: Rajin Shankar
    part of: UM_Basic

    available to use according to UM_Basic/LICENSE
 */

using UnityEngine;

namespace UModules
{
    /// <summary>
    /// Reusable asset to hold a single floating point number.
    /// Many behaviors can share a reference to this number, thus it can be changed in just one location to affect anything referencing it.
    /// Also supports being set during a play session while retaining its original value on subsequent sessions.
    /// </summary>
    [CreateAssetMenu(fileName = "Float_", menuName = "UModules/Float", order = 0)]
    public class FloatAsset : ScriptableObject
    {
        /// <summary>
        /// The original stored value
        /// </summary>
        [SerializeField] private float value;
        /// <summary>
        /// The value to use after calling Set
        /// </summary>
        private float? overrideValue = null;

        /// <summary>
        /// Override the value set in editor with a new value.
        /// (Setting value directly changes the serialized object, Set will only change the object's value for a single session.)
        /// </summary>
        /// <param name="newValue">Override value to store</param>
        /// <returns></returns>
        public float Set(float newValue)
        {
            overrideValue = newValue;
            return newValue;
        }

        /// <summary>
        /// Implicitly convert a FloatAsset to a float.
        /// Uses the override value created by Set if it exists, otherwise just the base stored value.
        /// </summary>
        /// <param name="floatAsset">The asset to convert</param>
        public static implicit operator float(FloatAsset floatAsset)
        {
            return floatAsset.overrideValue ?? floatAsset.value;
        }
    }
}
