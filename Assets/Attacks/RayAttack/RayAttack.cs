using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayAttack : MonoBehaviour
{
    private float damage = 75;

    public GameObject mainCharacter;
    

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
        if(other.GetComponent<ShadowSoldier>() != null)
        {
            ShadowSoldier health = other.GetComponent<ShadowSoldier>();
            health.Damage(damage);
            Debug.Log("Damaged!!");
        }
        if(other.GetComponent<ShadowGate>() != null)
        {
            ShadowGate health = other.GetComponent<ShadowGate>();
            health.Damage(damage);
            Debug.Log("Damaged!!");
        }
    }
}
