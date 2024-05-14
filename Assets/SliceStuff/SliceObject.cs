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

    public GameObject SlicedManager;

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
            // Set replace material to sliced object's defined material. If not set, use default on slicer
            replaceMaterial = sliceableComponent.InnerMaterial ? sliceableComponent.InnerMaterial : cubeCrossSectionMat;

            GameObject upperHull = hull.CreateUpperHull(target, replaceMaterial); // CreateUpperHull(target, material that shows) 
            GameObject lowerHull = hull.CreateLowerHull(target, replaceMaterial);

            // Calculates upper and lower hull sizes, if either is too small, don't slice
            float upperHullSize = CalculateSize(upperHull);
            float lowerHullSize = CalculateSize(lowerHull);
            if (upperHullSize > sliceableComponent.originalSize * sliceableComponent.margin &&
                lowerHullSize > sliceableComponent.originalSize * sliceableComponent.margin)
            {
                // Decrements the slice count for children
                sliceableComponent.sliceCount--;
                // Sets up upper and lower hulls, destroys parent object
                SetupSliceComponent(upperHull, target);
                SetupSliceComponent(lowerHull, target);
                if (sliceableComponent.isRandomSound)
                {
                    AudioManager.instance.RandomPlay(sliceableComponent.sounds, transform);
                }
                else
                {
                    StartCoroutine(AudioManager.instance.PlayInOrder(sliceableComponent.sounds, transform));
                }
                Destroy(target);
            }
            else
            {
                Debug.Log("Slice is too small.");
                Destroy(upperHull);
                Destroy(lowerHull);
            }
        }
    }

    // Create components, set mesh values, add explosion force, and set layer to slicable
    public void SetupSliceComponent(GameObject slicedObject, GameObject parent)
    {
        slicedObject.tag = parent.tag;
        slicedObject.transform.parent = SlicedManager.transform;

        MeshCollider mc = slicedObject.AddComponent<MeshCollider>();
        Rigidbody rb = slicedObject.AddComponent<Rigidbody>();
        XRGrabInteractable grabable = slicedObject.AddComponent<XRGrabInteractable>();
        Sliceable sc = slicedObject.AddComponent<Sliceable>();
        sc.CopyValuesFrom(parent.GetComponent<Sliceable>());
        sc.margin *= 0.5f;
        mc.convex = true;
        rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        rb.AddExplosionForce(explosionForce, slicedObject.transform.position, 1);
        grabable.movementType = XRBaseInteractable.MovementType.VelocityTracking;
        grabable.useDynamicAttach = true;
        slicedObject.layer = LayerMask.NameToLayer("Sliceable");


        // Hugo: Do not mind me, this is necessary to keep track of the number of cuts...

    }

    // Calculates the size of an object using MeshFilter
    public float CalculateSize(GameObject target)
    {
        Vector3 sizeVector = target.GetComponent<MeshFilter>().mesh.bounds.size;
        return sizeVector.x * sizeVector.y * sizeVector.z;
    }
}
