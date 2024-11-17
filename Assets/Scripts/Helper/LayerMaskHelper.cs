using UnityEngine;

public static class LayerMaskHelper
{
    public static LayerMask CreateLayerMask(Layer[] layers)
    {
        int maskValue = 0;
        foreach (Layer layer in layers)
        {
            int layerIndex = (int)layer;
            if (layerIndex >= 0 && layerIndex < 32)
            {
                maskValue |= 1 << layerIndex;
            }
            else
            {
                Debug.LogWarning("Layer index out of bounds: " + layerIndex);
            }
        }
        return maskValue;
    }
}
