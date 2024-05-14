using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteSwitcherKitchen : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject Paper;

    private MeshRenderer currentPage;

    public Material step1Tomato;
    public Material step2Onion;
    public Material step3Pork;
    public Material step4Choy;

    private List<Material> PageList = new List<Material>();
    private List<Material> tempPageList = new List<Material>();
    private int pageIndex = 0;

    GameObject currentLoop = null;
    void Start()
    {
        currentPage = Paper.GetComponent<MeshRenderer>();
        PageList.Add(step1Tomato);
        PageList.Add(step2Onion);
        PageList.Add(step1Tomato);
        PageList.Add(step3Pork);
        PageList.Add(step4Choy);
        //SetPage(pageIndex);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextPage()
    {
        Debug.Log("next button pressed");
        if(pageIndex <= 2)
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

    public void BeginOnionChop()
    {
        SetPage(0);
    }

    public void BeginTomatoChop()
    {
        SetPage(1);
    }

    public void BeginBowlStep()
    {
        SetPage(2);
    }

    public void BeginBrownPork()
    {
        SetPage(3);
    }

    public void BeginWashingBokChoy()
    {
        SetPage(4);
    }
}
