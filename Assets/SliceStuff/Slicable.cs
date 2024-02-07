using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sliceable : MonoBehaviour
{
    // Will not be slicable once count reaches 0
    public int sliceCount = 4;
    public Material InnerMaterial;

    // Copy function
    public void CopyValuesFrom(Sliceable input)
    {
        sliceCount = input.sliceCount;
        InnerMaterial = input.InnerMaterial;
    }
}
