using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPageManager : MonoBehaviour
{
    public GameObject nextPage;
    private GameObject thisPage;
    public GameObject lastPage;
    // Start is called before the first frame update
    void Start()
    {
        thisPage = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void goToNextPage()
    {
        nextPage.SetActive(true);
        thisPage.SetActive(false);
    }

    public void goToLastPage()
    {
        lastPage.SetActive(true);
        thisPage.SetActive(false);
    }
}
