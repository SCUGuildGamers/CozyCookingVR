using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartButton : MonoBehaviour
{
    public void RestartGame()
    {
        StartCoroutine(SwitchScenes());
    }

    private IEnumerator SwitchScenes()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(sceneName: "Home Kitchen Intro");
    }
}
