using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Stream : MonoBehaviour
{
    private LineRenderer lineRenderer = null;

    private ParticleSystem splashParticle = null;
    private Vector3 targetPosition = Vector3.zero;

    private Coroutine pourRoutine = null;
    public Material liquidFill;
    public LayerMask myLayerMask;

    public AudioSource potSource;
    public AudioSource floorSource;
    public AudioSource glugSource;
    //public AudioClip Pouring;
    public AudioClip hittingPot;
    public AudioClip bottleGlug;
    public AudioClip hittingFloor;



    private struct info{
        public bool v;
        public RaycastHit h;
        public info(bool v, RaycastHit r) : this()
        {
            this.v = v;
            this.h = r;
        }
    }
    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        splashParticle = GetComponentInChildren<ParticleSystem>();
    }
    // Start is called before the first frame update
    void Start()
    {
        potSource.clip = hittingPot;
        floorSource.clip = hittingFloor;
        glugSource.clip = bottleGlug;

        MoveToPosition(0, transform.position);
        MoveToPosition(1, transform.position);
        myLayerMask = LayerMask.GetMask("Pot");
        myLayerMask = ~myLayerMask;    
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(targetPosition); 
     
    }

    public void Begin()
    {
        
        StartCoroutine(UpdateParticle());
        pourRoutine = StartCoroutine(BeginPour());
        
    }

    public void End()
    {
  
        StopCoroutine(pourRoutine);
        pourRoutine = StartCoroutine(EndPour());
    }

    private IEnumerator EndPour()
    {

        while (!hasReachedPos(0, targetPosition))
        {

            AnimateToPosition(0, targetPosition);
            AnimateToPosition(1, targetPosition);
            yield return null;
        }
        Destroy(gameObject);
        
    }

    /*
    private IEnumerator BeginPour()
    {
        // play the start pour sound
        while (gameObject.activeSelf)
        {
            info a = FindEndPoint();
            targetPosition = a.endPoint;
            MoveToPosition(0, transform.position);
            AnimateToPosition(1, targetPosition);
            
            if(a.v && hasReachedPos(1, targetPosition)){
                liquidFill = a.h.collider.gameObject.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material;
                liquidFill.SetFloat("_fill", liquidFill.GetFloat("_fill") + 0.0005f);
                // play filling pot sound here 
                // play the hitting the pot sound
            }
            yield return null;

        }
    }
    */
    /*
    private info FindEndPoint()
    {
        RaycastHit hit;
        bool willFill = false;
        Ray ray = new Ray(transform.position, Vector3.down);
        Physics.Raycast(ray, out hit, 2.0f);
        Vector3 endPoint = hit.collider ? hit.point : ray.GetPoint(2.0f);
        
        if(hit.collider.tag == "Fillable")
        {
            // old implementation more but fills before the water hits the pot 
            //liquidFill = hit.collider.gameObject.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material;
            //liquidFill.SetFloat("_fill", liquidFill.GetFloat("_fill") + 0.0005f);
            willFill = true;
        }

        info a = new info(endPoint, willFill, hit);
        return a;
    }
    */

    private IEnumerator BeginPour()
    {
        // play the start pour sound
  
        while (gameObject.activeSelf)
        {

            targetPosition = FindEndPoint();
            MoveToPosition(0, transform.position);
            AnimateToPosition(1, targetPosition);
            yield return null;

        }
    }
    private Vector3 FindEndPoint()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, Vector3.down);
        Physics.Raycast(ray, out hit, 2.0f, myLayerMask); // 2.0 is the max distance the water can go
        Vector3 endPoint = hit.collider ? hit.point : ray.GetPoint(2.0f);
        return endPoint;
    }
    private info ShouldFill()
    {
        info a;
        RaycastHit hit;
        Ray ray = new Ray(transform.position, Vector3.down);
        Physics.Raycast(ray, out hit, 2.0f, myLayerMask); // 2.0 is the max distance the water can go
        if (hit.collider.tag == "Fillable")
        {
            a = new info(true, hit);
            return a;
        }
        else
        {
            a = new info(false, hit);
            return a;
        }
    }
    private void MoveToPosition(int index, Vector3 target)
    {
        lineRenderer.SetPosition(index, target);
    }

    private void AnimateToPosition(int index, Vector3 targetPosition)
    {
        Vector3 currentPoint = lineRenderer.GetPosition(index);
        Vector3 newPos = Vector3.MoveTowards(currentPoint, targetPosition, Time.deltaTime*1.75f); // maybe make the stream faster?
        lineRenderer.SetPosition(index, newPos);
    }

    private bool hasReachedPos(int index, Vector3 target)
    {
        Vector3 currPos = lineRenderer.GetPosition(index);
        return currPos == target;
    }

    private IEnumerator UpdateParticle()
    {
        bool isHitting;
        info isFilling;
        while (gameObject.activeSelf)
        {
            isFilling = ShouldFill();
            splashParticle.gameObject.transform.position = targetPosition;
            isHitting = hasReachedPos(1, targetPosition);
            splashParticle.gameObject.SetActive(isHitting);
            if (isHitting && isFilling.v)
            {
                liquidFill = isFilling.h.collider.gameObject.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material;
                liquidFill.SetFloat("_fill", liquidFill.GetFloat("_fill") + 0.0003f);
              
            }
            else if (!isFilling.v && isHitting)
            {
                
            }
            
            yield return null;
        }   
    }

    public void Die()
    {
        Destroy(this);
    }


}
