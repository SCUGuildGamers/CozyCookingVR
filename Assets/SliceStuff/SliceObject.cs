using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;
using UnityEngine.InputSystem;
public class SliceObject : MonoBehaviour
{
    public Transform planeDebug;

    public Transform startPoint;
    public Transform endPoint;
    public VelocityEstimator controllerVelocity;
    public LayerMask slicableLayer;
    public Material cubeCrossSectionMat;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        bool hasHit = Physics.Linecast(startPoint.position, endPoint.position, out RaycastHit hit, slicableLayer);
        if (hasHit)
        {
            GameObject target = hit.transform.gameObject;
            Slice(target);
        }
        
    }
    public void Slice(GameObject target)
    {
        Vector3 vel = controllerVelocity.GetVelocityEstimate();
        Vector3 planeNormal = Vector3.Cross(endPoint.position - startPoint.position, vel);
        planeNormal.Normalize();
        SlicedHull hull = target.Slice(endPoint.position, planeNormal);
        if(hull != null)
        {
            GameObject upperHull = hull.CreateUpperHull(target, cubeCrossSectionMat); // CreateUpperHull(target, material that shows) 
            SetupSliceComponent(upperHull);
            upperHull.layer = LayerMask.NameToLayer("Sliceable");
            GameObject lowerHull = hull.CreateLowerHull(target, cubeCrossSectionMat);
            SetupSliceComponent(lowerHull);
            lowerHull.layer = LayerMask.NameToLayer("Sliceable");
            Destroy(target);

        }
    }

    public void SetupSliceComponent(GameObject slicedObject)
    {
        Rigidbody rb = slicedObject.AddComponent<Rigidbody>();
        MeshCollider mc = slicedObject.AddComponent<MeshCollider>();
        mc.convex = true;
        rb.AddExplosionForce(2000, slicedObject.transform.position, 1);

    }
}
