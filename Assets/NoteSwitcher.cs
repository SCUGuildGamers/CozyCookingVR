using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteSwitcher : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject Paper;

    private MeshRenderer currentPage;

    public Material step5;
    public Material step6;
    public Material step7;
    public Material step8;
    public Material step9;

    private List<Material> PageList = new List<Material>();
    private List<Material> tempPageList = new List<Material>();
    private int pageIndex = 0;

    GameObject currentLoop = null;
    void Start()
    {
        currentPage = Paper.GetComponent<MeshRenderer>();
        PageList.Add(step5);
        PageList.Add(step6);
        PageList.Add(step7);
        PageList.Add(step8);
        PageList.Add(step9);
        SetPage(pageIndex);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextPage()
    {
        Debug.Log("next button pressed");
        if(pageIndex <= 3)
        {
            pageIndex += 1;
            SetPage(pageIndex);
        }
    }

    public void LastPage()
    {
        Debug.Log("Last button pressed");
        if(pageIndex > 0)
        {
            pageIndex -= 1;
            SetPage(pageIndex);
        }
       
    }
    public void SetPage(int index)
    {
        pageIndex = index;
        Material[] tempList = currentPage.materials;
        tempList[1] = PageList[index];
        FlippedPage();
        for(int i = 0; i < tempList.Length; i++)
        {
            tempPageList.Add(tempList[i]);
        }
        currentPage.SetMaterials(tempPageList);
        tempPageList.Clear();
    }

    public void PickedUpBook()
    {
        AudioManager.instance.Play("sfx_pickingupnotebook", transform);
    }

    public void PutDownBook()
    {
        AudioManager.instance.Play("sfx_lettinggonotebook", transform);
    }

    public void FlippedPage()
    {
        AudioManager.instance.Play("sfx_turningpage", transform);
    }
}
