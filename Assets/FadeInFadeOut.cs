using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInFadeOut : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject BlackImage;
    void Start()
    {
        
    }

    private void Awake()
    {
        StartCoroutine(FadeInFromBlack());
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void StartFadeToBlack()
    {
        StartCoroutine(FadeToBlack());
    }


    public IEnumerator FadeToBlack()
    {   
        Color black = BlackImage.GetComponent<Image>().color;
        Color targetColor = new Color(black.r, black.g, black.b, 0f);
        float timer = 0f;
        float transitionTime = 2f;

        float tempAlpha = 0f;
        while (tempAlpha <= 1f)
        {
            tempAlpha += 0.01f;
            float t = Mathf.SmoothStep(0, 1, timer / transitionTime);
            BlackImage.GetComponent<Image>().color = new Color(black.r, black.g, black.b, tempAlpha);
            yield return null;
        }

    }

    public IEnumerator FadeInFromBlack()
    {
        yield return new WaitForSeconds(2.5f);
        Color black = BlackImage.GetComponent<Image>().color;
        Color targetColor = new Color(black.r, black.g, black.b, 0f);
        float timer = 0f;
        float transitionTime = 2f;

        float tempAlpha = 1f;
        while (tempAlpha >= 0)
        {
            tempAlpha -= 0.01f;
            float t = Mathf.SmoothStep(0, 1, timer / transitionTime);
            BlackImage.GetComponent<Image>().color = new Color(black.r, black.g, black.b, tempAlpha);
            yield return null;
        }
    }
}
