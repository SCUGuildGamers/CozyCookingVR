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

    private List<Material> PageList = new List<Material>();
    private List<Material> tempPageList = new List<Material>();
    private int pageIndex = 0;

    void Start()
    {
        currentPage = Paper.GetComponent<MeshRenderer>();
        PageList.Add(step5);
        PageList.Add(step6);
        PageList.Add(step7);
        PageList.Add(step8);
        SetPage();
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
            SetPage();
        }
    }

    public void LastPage()
    {
        Debug.Log("Last button pressed");
        if(pageIndex > 0)
        {
            pageIndex -= 1;
            SetPage();
        }
       
    }
    public void SetPage()
    {
        Material[] tempList = currentPage.materials;
        tempList[1] = PageList[pageIndex];
        for(int i = 0; i < tempList.Length; i++)
        {
            tempPageList.Add(tempList[i]);
        }
        currentPage.SetMaterials(tempPageList);
        tempPageList.Clear();
    }
}