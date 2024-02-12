using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sliceable : MonoBehaviour
{
    // Will not be slicable once count reaches 0
    public int sliceCount = 4;
    public Material InnerMaterial;
    public float margin = 0.1f;
    [HideInInspector]
    public float originalSize = -1f;

    private void Start()
    {
        Vector3 currentSize = GetComponent<MeshFilter>().mesh.bounds.size;
        float size = currentSize.x * currentSize.y * currentSize.z;
        if (originalSize < 0f)
        {
            originalSize = size;
        }
        Debug.Log(this.name + " Original Size: " + originalSize);
        Debug.Log(this.name + " Current Size: " + size);
    }

    // Copy function
    public void CopyValuesFrom(Sliceable input)
    {
        sliceCount = input.sliceCount;
        InnerMaterial = input.InnerMaterial;
        margin = input.margin;
        originalSize = input.originalSize;
    }
}
