using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BowlScript1 : MonoBehaviour
{

    private Outline OutlineScript;

    private int soupLevel = 0;

    public GameObject soup1;
    public GameObject soup2;
    public GameObject soup3;

    public GameObject fadeOut;

    // Start is called before the first frame update
    void Start()
    {
        OutlineScript = gameObject.GetComponent<Outline>();
        fadeOut.SetActive(true);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(sceneName: "Home Kitchen 2");
        }
    }

    public void StartingState()
    {
        OutlineScript.enabled = true;
    }

    public void EndingState()
    {
        //gameObject.enabled = false;
    }

    public void FillBowl()
    {
        if(soupLevel == 0)
        {
            soup1.SetActive(true);
            soupLevel += 1;
        }
        else if(soupLevel == 1)
        {
            soup1.SetActive(false);
            soup2.SetActive(true);
            soupLevel += 1;
        }
        else
        {
            soup2.SetActive(false);
            soup3.SetActive(true);
            // this is where you change the gamestate to change scenes 
            fadeOut.GetComponent<FadeInFadeOut>().StartFadeToBlack();
            StartCoroutine(SwitchScenes());
        }

    }
    private IEnumerator SwitchScenes()
    {
        yield return new WaitForSeconds(6);
        // insert fade to black code here
        SceneManager.LoadScene(sceneName: "Dorm P3");
    }
}
