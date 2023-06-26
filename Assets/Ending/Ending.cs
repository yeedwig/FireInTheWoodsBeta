using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ending : MonoBehaviour
{
    public GameObject[] scenes;
    private int page=0;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            scenes[page].SetActive(false);
            scenes[++page].SetActive(true);
        }
    }
}
