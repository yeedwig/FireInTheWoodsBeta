using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PictureArea : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.GetComponent<Animals>() != null)
        {
            Debug.Log(other.name);
            other.GetComponent<Animals>().typeAppeared[other.GetComponent<Animals>().currentType]=true;
        }
        else
        {
            Debug.Log("not Found");
        }
    }
}
