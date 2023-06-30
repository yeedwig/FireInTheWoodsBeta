using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PictureArea : MonoBehaviour
{
    public GameObject animalManager;
    public GameObject effectManager;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.GetComponent<Animals>() != null)
        {
            if(!other.GetComponent<Animals>().typeAppeared[other.GetComponent<Animals>().currentType]){
                other.GetComponent<Animals>().typeAppeared[other.GetComponent<Animals>().currentType]=true;
                effectManager.GetComponent<EffectManager>().NewEncyclopediaFound();
                if(other.GetComponent<Animals>().currentType==2){
                    animalManager.GetComponent<AnimalManager>().irochiUpgrade(other.name);
                }
                
            }
        }
    }
    
}
