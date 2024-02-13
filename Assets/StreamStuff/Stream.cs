using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stream : MonoBehaviour
{
    private LineRenderer lineRenderer = null;

    private ParticleSystem splashParticle = null;
    private Vector3 targetPosition = Vector3.zero;

    private Coroutine pourRoutine = null;

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

        Physics.Raycast(ray, out hit, 2.0f);
        Vector3 endPoint = hit.collider ? hit.point : ray.GetPoint(2.0f);
        return endPoint;
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
}
