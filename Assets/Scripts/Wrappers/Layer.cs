using System;
using System.Collections.Generic;
using UnityEngine;

namespace OmniDi.Library.Wrappers
{
    [Serializable]
    public struct Layer
    {
        [SerializeField] private int layer;

        public int Index => layer;
        public string Name => LayerMask.LayerToName(layer);

        private Layer(int layer)
        {
            this.layer = layer;
        }

        /// <summary>
        /// Returns a Result containing a Layer based on the input layer index.
        /// </summary>
        /// <param name="layerIndex">The index of the layer.</param>
        public static Result<Layer> New(int layerIndex)
        {
            var isValidLayer = layerIndex is >= 0 and <= 31;
            return isValidLayer ? Result<Layer>.Ok(new Layer(layerIndex)) : Result<Layer>.Err("Index is outside layer index");
        }
    }
}