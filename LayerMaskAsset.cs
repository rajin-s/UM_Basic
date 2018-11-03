/*
    UModules::LayerMaskAsset
    UModules::LayerMaskExtensionMethods

    by: Rajin Shankar
    part of: UM_Basic

    available to use according to UM_Basic/LICENSE
 */

using UnityEngine;

namespace UModules
{
    /// <summary>
    /// Reusable asset to hold a static layer mask.
    /// Many behaviors can share a reference to this mask, thus it can be changed in just one location to affect anything referencing it.
    /// </summary>
    [CreateAssetMenu(fileName = "Mask_", menuName = "UModules/Layer Mask", order = 1)]
    public class LayerMaskAsset : ScriptableObject
    {
        /// <summary>
        /// The stored layer mask
        /// </summary>
        [SerializeField] private LayerMask value;

        /// <summary>
        /// Test if a given layer is contained within the mask
        /// </summary>
        /// <param name="layer">The subject layer</param>
        /// <returns>True if the layer is contained in the mask, false otherwise</returns>
        public bool Test(int layer) { return value.Test(layer); }
        /// <summary>
        /// Implicitly convert a LayerMaskAsset to a LayerMask
        /// </summary>
        /// <param name="layerMaskAsset"></param>
        public static implicit operator LayerMask(LayerMaskAsset layerMaskAsset) { return layerMaskAsset.value; }
    }

    /// <summary>
    /// Extension method container for working with layer masks
    /// </summary>
    public static class LayerMaskExtensionMethods
    {
        /// <summary>
        /// Test if a given layer is contained with the given mask
        /// </summary>
        /// <param name="mask">The mask to test</param>
        /// <param name="layer">The subject layer</param>
        /// <returns>True if the layer is contained in the mask, false otherwise</returns>
        public static bool Test(this LayerMask mask, int layer)
        {
            return (mask & (1 << layer)) != 0;
        }
    }
}
