using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Comet : MonoBehaviour
{
    [SerializeField] private GameObject cometPrefab;
    [SerializeField] private Transform[] firePosition;
    public bool Dead = false;
    private Animator anim;
    private BoxCollider2D bc;
    private SpriteRenderer sp;

    public float health;
    public float startHP = 80;

    private float state = 0;
    private float ballCount = 0;
    private float waitCount = 0;
    [SerializeField] private float waitTime;
    private int randomPoint;
    [SerializeField] float attackLimit;

    private float attackTimer;
    [SerializeField] float attackTime;

    [SerializeField] private GameObject mainCharacterGO;
    private MainCharacter mainCharacter;


    void Start()
    {
        sp = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        bc = GetComponent<BoxCollider2D>();

        health = startHP;
        mainCharacter=mainCharacterGO.GetComponent<MainCharacter>();
    }

    public void Damage(float damage)
    {
        health -= (damage+mainCharacter.plusDamageByAnimalContract+mainCharacter.plusDamageByItem);
        
        
        if(health > 0)
        {
            anim.SetBool("Dead", false);
            StartCoroutine(damagedAnimation());
            //anim.SetBool("Attacked", false);
        }
        else if(health <= 0)
        {
            anim.SetBool("Dead", true);
            if(!Dead) GameObject.Find("GameManager").GetComponent<GameManager>().kills++;
            Dead = true;
            Destroy(this.gameObject,2.0f);
        }
    } 

    IEnumerator damagedAnimation()
    {
        anim.SetBool("Damaged", true);
        sp.color = new Color (1,1,1,0.5f);
        yield return new WaitForSeconds(0.3f);
        sp.color = new Color(1,1,1,1);
        anim.SetBool("Damaged", false);

    }

    void Update()
    {
        //Debug.Log(state);
        if(Dead == false)
        {     
            attackTimer += Time.deltaTime;
            if(attackTimer >= attackTime)
            {
                attackTimer = 0;
                if(state == 0)
                {
                    anim.SetBool("Attacking", true);
                    randomPoint = Random.Range(0,5);
                    Instantiate(cometPrefab, firePosition[randomPoint].position, firePosition[randomPoint].rotation);
                    ballCount++;
                    if(ballCount == attackLimit)
                    {
                        waitCount = 0;
                        state = 1;
                    }
                }
                if(state == 1)
                {
                    anim.SetBool("Attacking", false);
                    waitCount++;
                    if(waitCount == waitTime)
                    {
                        ballCount = 0;
                        state = 0;
                    }
                }
            }
        }
    }
}
