using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{

    [SerializeField] private float damage = 20.0f;
    
    public GameObject mainCharacter;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.GetComponent<Level1Enemy>() != null)
        {
            Level1Enemy health = other.GetComponent<Level1Enemy>();
            health.Damage(damage);
            Debug.Log("Damaged!!");
        }

        if(other.GetComponent<Level2Enemy>() != null)
        {
            Level2Enemy health = other.GetComponent<Level2Enemy>();
            health.Damage(damage);
            Debug.Log("Damaged!!");
        }

        if(other.GetComponent<Worm>() != null)
        {
            Worm health = other.GetComponent<Worm>();
            health.Damage(damage);
            Debug.Log("Damaged!!");
        }
        
        if(other.GetComponent<Comet>() != null)
        {
            Comet health = other.GetComponent<Comet>();
            health.Damage(damage);
            Debug.Log("Damaged!!");
        }

        if(other.GetComponent<ShadowBall>() != null)
        {
            ShadowBall health = other.GetComponent<ShadowBall>();
            health.Damage(damage);
            Debug.Log("Damaged!!");
        }

        if(other.GetComponent<Angel>() != null)
        {
            Angel health = other.GetComponent<Angel>();
            health.Damage(damage);
            Debug.Log("Damaged!!");
        }
        if(other.GetComponent<TheHand>() != null)
        {
            TheHand health = other.GetComponent<TheHand>();
            health.Damage(damage);
            Debug.Log("Damaged!!");
        }
        if(other.GetComponent<TheEye>() != null)
        {
            TheEye health = other.GetComponent<TheEye>();
            health.Damage(damage);
            Debug.Log("Damaged!!");
        }
        if(other.GetComponent<TheFear>() != null)
        {
            TheFear health = other.GetComponent<TheFear>();
            health.Damage(damage);
            Debug.Log("Damaged!!");
        }
        if(other.GetComponent<Goblin>() != null)
        {
            Goblin health = other.GetComponent<Goblin>();
            health.Damage(damage);
            Debug.Log("Damaged!!");
        }
    }

}