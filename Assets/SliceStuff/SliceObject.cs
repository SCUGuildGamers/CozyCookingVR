using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
public class SliceObject : MonoBehaviour
{
    public Transform planeDebug;

    public Transform startPoint;
    public Transform endPoint;
    public VelocityEstimator controllerVelocity;
    public LayerMask slicableLayer;
    public Material cubeCrossSectionMat;
    public int explosionForce = 2000;

    private Material replaceMaterial;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Checks if slice line has hit an object in Slicable layer, then calls Slice on object
        bool hasHit = Physics.Linecast(startPoint.position, endPoint.position, out RaycastHit hit, slicableLayer);
        if (hasHit)
        {
            GameObject target = hit.transform.gameObject;
            Slice(target);
        }
        
    }
    public void Slice(GameObject target)
    {
        // Grabs Slicable component and if slice count is 0, then stop slice from happening
        Sliceable sliceableComponent = target.GetComponent<Sliceable>();
        if (sliceableComponent.sliceCount == 0)
            return;

        // Calculates the plane of the slice based on controller velocity and slice object position
        Vector3 vel = controllerVelocity.GetVelocityEstimate();
        Vector3 planeNormal = Vector3.Cross(endPoint.position - startPoint.position, vel);
        planeNormal.Normalize();
        SlicedHull hull = target.Slice(endPoint.position, planeNormal);
        if(hull != null)
        {
            // Decrements the slice count for children
            sliceableComponent.sliceCount--;
            // Set replace material to sliced object's defined material. If not set, use default on slicer
            replaceMaterial = sliceableComponent.InnerMaterial ? sliceableComponent.InnerMaterial : cubeCrossSectionMat;

            GameObject upperHull = hull.CreateUpperHull(target, replaceMaterial); // CreateUpperHull(target, material that shows) 
            SetupSliceComponent(upperHull, target);
            GameObject lowerHull = hull.CreateLowerHull(target, replaceMaterial);
            SetupSliceComponent(lowerHull, target);
            Destroy(target);

        }
    }

    // Create components, set mesh values, add explosion force, and set layer to slicable
    public void SetupSliceComponent(GameObject slicedObject, GameObject parent)
    {
        Rigidbody rb = slicedObject.AddComponent<Rigidbody>();
        MeshCollider mc = slicedObject.AddComponent<MeshCollider>();
        XRGrabInteractable grabable = slicedObject.AddComponent<XRGrabInteractable>();
        Sliceable sc = slicedObject.AddComponent<Sliceable>();
        sc.CopyValuesFrom(parent.GetComponent<Sliceable>());
        mc.convex = true;
        rb.AddExplosionForce(explosionForce, slicedObject.transform.position, 1);
        slicedObject.layer = LayerMask.NameToLayer("Sliceable");
    }
}
