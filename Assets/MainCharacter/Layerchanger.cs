using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Layerchanger : MonoBehaviour
{
    public SpriteRenderer playerSR;
    private BoxCollider2D bc;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Border0")
        {
            playerSR.sortingOrder = 7;
        }
        else if(collision.gameObject.name == "Border1")
        {
            playerSR.sortingOrder = 5;
        }
        else if(collision.gameObject.name == "Border2")
        {
            playerSR.sortingOrder = 3;
        }
        else if(collision.gameObject.name == "Border3")
        {
            playerSR.sortingOrder = 1;
        }
    }
    // Update is called once per frame
    void Update()
    {
        //Debug.Log(playerSR.sortingOrder);
    }
}
