using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowBall : MonoBehaviour
{
    
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float lifeTime;
    [SerializeField] private float attackDamage = 10;
    private float health = 30.0f;
    private Animator anim;

    public float dirX;
    public float dirY;

    public AudioClip clip;
    public AudioClip clip1;

    
    // Start is called before the first frame update
    void Start()
    {
        
        rb.velocity =new Vector2(dirX*speed, dirY*speed);
        anim = GetComponent<Animator>();

        Invoke("DestroyProjectile", lifeTime);
        

    }

    public void Damage(float damage)
    {
        health-=damage;
        
        if(health <= 0)
        {
            anim.SetBool("Dead", true);
            attackDamage = 0;
            Destroy(gameObject, 2.0f);
        }
    } 

    //oncollision animation

    // Update is called once per frame
    public void DestroyProjectile()
    {
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Fire")
        {
            GameObject.Find("GameManager").GetComponent<GameManager>().takeDamage(attackDamage);
            anim.SetBool("Reached", true);
            Destroy(this.gameObject,3.0f);
        }
        
        else if(other.gameObject.tag == "Execute")
        {
            Damage(GameObject.Find("GameManager").GetComponent<GameManager>().executeDamage);
        }
        else if(other.gameObject.tag == "Barrier")
        {
            Destroy(gameObject);
            GameObject.Find("fire").GetComponent<FireManager>().barrierLife--;
        }
        /// if other is fire set damage to fire and, activate attack animation set path Index to 0, reset enemy
    }
}
