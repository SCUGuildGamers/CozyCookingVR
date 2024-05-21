using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class NoteBookIntro : MonoBehaviour
{
    // Start is called before the first frame update
    private Outline bookOutline;
    public GameObject fadeOut;
    public GameObject buttonCanvas;
    public GameObject instructions;
    void Start()
    {
        fadeOut.SetActive(true);
        bookOutline = GetComponent<Outline>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PickedUpBook()
    {
        bookOutline.enabled = false;
        buttonCanvas.SetActive(true);
        instructions.GetComponent<TextMeshProUGUI>().text = "Poke the button";
        AudioManager.instance.Play("sfx_pickingupnotebook", transform);
    }

    public void PutDownBook()
    {
        AudioManager.instance.Play("sfx_lettinggonotebook", transform);
    }

    public void ButtonPressed()
    {
        Debug.Log("button pressed");

        StartCoroutine(SceneTransition());
    }

    private IEnumerator SceneTransition()
    {
        fadeOut.GetComponent<FadeInFadeOut>().StartFadeToBlack();
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene(sceneName: "Home Kitchen Original");
    }

}
