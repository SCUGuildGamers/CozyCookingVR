using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stream : MonoBehaviour
{
    private LineRenderer lineRenderer = null;

    private ParticleSystem splashParticle = null;
    private Vector3 targetPosition = Vector3.zero;

    private Coroutine pourRoutine = null;
    public Material liquidFill;

    private struct info{
        public Vector3 endPoint;
        public bool v;
        public RaycastHit h;
        public info(Vector3 endPoint, bool v, RaycastHit r) : this()
        {
            this.endPoint = endPoint;
            this.v = v;
            this.h = r;
        }
    }
    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        //splashParticle = GetComponentInChildren<ParticleSystem>();
    }
    // Start is called before the first frame update
    void Start()
    {
        MoveToPosition(0, transform.position);
        MoveToPosition(1, transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Begin()
    {
        pourRoutine = StartCoroutine(BeginPour());
        
    }

    public void End()
    {
        StopCoroutine(pourRoutine);
        pourRoutine = StartCoroutine(EndPour());
    }

    private IEnumerator EndPour()
    {
        while(!hasReachedPos(0, targetPosition))
        {
            AnimateToPosition(0, targetPosition);
            AnimateToPosition(1, targetPosition);
            yield return null;
        }
        Destroy(gameObject);
        
    }

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

    private void MoveToPosition(int index, Vector3 target)
    {
        lineRenderer.SetPosition(index, target);
    }

    private void AnimateToPosition(int index, Vector3 targetPosition)
    {
        Vector3 currentPoint = lineRenderer.GetPosition(index);
        Vector3 newPos = Vector3.MoveTowards(currentPoint, targetPosition, Time.deltaTime*1.75f);
        lineRenderer.SetPosition(index, newPos);
    }

    private bool hasReachedPos(int index, Vector3 target)
    {
        Vector3 currPos = lineRenderer.GetPosition(index);
        return currPos == target;
    }

    private IEnumerator UpdateParticle()
    {
        yield return null;
    }

    public void Die()
    {
        Destroy(this);
    }

 /* couldn't figure out the collisions with the line ask abt it later 
    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.tag == "Fillable")
        {
            Debug.Log("should be filling");
            liquidFill = collision.gameObject.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material;
            liquidFill.SetFloat("_fill", liquidFill.GetFloat("_fill") + 0.0005f);
        }
    }
 */

}
