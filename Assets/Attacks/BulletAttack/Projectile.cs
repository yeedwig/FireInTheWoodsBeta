using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float lifeTime;
    private float damage = 50;

    public float dirX;
    public float dirY;
    // Start is called before the first frame update
    void Start()
    {
        //dirX = main.GetComponent<MainCharacter>().prevX;
        //dirY = main.GetComponent<MainCharacter>().prevY;
        
        rb.velocity =new Vector2(dirX*speed, dirY*speed);
        //transform.Translate(0,0,speed * Time.deltaTime);
        Invoke("DestroyProjectile", lifeTime);

    }

    //oncollision animation

    // Update is called once per frame
    void DestroyProjectile()
    {
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.GetComponent<Level1Enemy>() != null)
        {
            Level1Enemy health = other.GetComponent<Level1Enemy>();
            health.Damage(damage);
            Destroy(gameObject);
            Debug.Log("Damaged!!");
        }

        if(other.GetComponent<Level2Enemy>() != null)
        {
            Level2Enemy health = other.GetComponent<Level2Enemy>();
            health.Damage(damage);
            Destroy(gameObject);
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
