using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SauceStream : MonoBehaviour 
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

    GameObject currentLoop = null;

    private struct info
    {
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
        if (Keyboard.current.quoteKey.wasPressedThisFrame)
        {
            Debug.Log("should be gluggin");
            glugSource.Play();
        }
    }

    public void Begin()
    {

        StartCoroutine(UpdateParticle());
        pourRoutine = StartCoroutine(BeginPour());

    }

    public void End()
    {
        StartCoroutine(currentLoop.GetComponent<Loopable>().LerpDestroySelf(0.0f, 0.0f));
        StopCoroutine(pourRoutine);
        pourRoutine = StartCoroutine(EndPour());
    }

    private IEnumerator EndPour()
    {

        while (!hasReachedPos(0, targetPosition))
        {
            /*
            if (glugSource.isPlaying)
            {
                glugSource.Stop();
            }
            */
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
            if(currentLoop == null)
            {
                currentLoop = AudioManager.instance.LerpLoopable("sfx_bottleglug", transform, 0.0f);
            }
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
        Vector3 newPos = Vector3.MoveTowards(currentPoint, targetPosition, Time.deltaTime * 1.75f); // maybe make the stream faster?
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
                isFilling.h.collider.gameObject.GetComponent<PotDetector>().AddSauce();

                if (!potSource.isPlaying)
                {
                    //potSource.Play();
                }
            }
            else if (!isFilling.v && isHitting)
            {
                if (potSource.isPlaying)
                {
                    //potSource.Stop();
                }
            }
            yield return null;
        }
    }
}
