using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowGate : MonoBehaviour
{
    [SerializeField] private GameObject Boss;
    [SerializeField] private GameObject soldierPrefab;
    [SerializeField] private Transform firePosition;

    public bool Dead = false;
    private Animator anim;
    private BoxCollider2D bc;
    private SpriteRenderer sp;

    public float health;
    public float startHP = 600;

    private float appearState;
    private float state = 0;
    private float soldierCount = 0;
    private float waitCount = 0;
    [SerializeField] private float waitTime;
    private int randomPoint;
    [SerializeField] float soldierLimit;

    private float attackTimer;
    [SerializeField] float attackTime;

    [SerializeField] private GameObject mainCharacterGO;
    private MainCharacter mainCharacter;

    public AudioClip clip;
    public AudioClip clip1;

    void Start()
    {
        sp = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        bc = GetComponent<BoxCollider2D>();
        appearState = 0;
        health = startHP;
        mainCharacter=mainCharacterGO.GetComponent<MainCharacter>();
        StartCoroutine(startingAnimation());
        
    }
    IEnumerator startingAnimation()
    {
        anim.SetBool("Appear", true);
        yield return new WaitForSeconds(1.5f);
        anim.SetBool("Appear", false);
        appearState = 1;
    }

    public void Damage(float damage,bool check=true)
    {
        if(check){
            health -= (damage+mainCharacter.plusDamageByAnimalContract+mainCharacter.plusDamageByItem);
        }
        else{
            health -=damage;
        }
        if(health > 0)
        {
            SoundManager.instance.SFXPlay("EnemyHitSound",clip);
            anim.SetBool("Dead", false);
            StartCoroutine(damagedAnimation());
            //anim.SetBool("Attacked", false);
        }
        else if(health <= 0)
        {
            anim.SetBool("Dead", true);
            if(!Dead){
                SoundManager.instance.SFXPlay("EnemyDeadSound",clip1);
                GameObject.Find("GameManager").GetComponent<GameManager>().kills++;
                Boss.GetComponent<Boss>().DamageBoss(1000);
            } 
            Dead = true;
            //보스에 공격 가하기
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
        if(Dead == false && appearState == 1)
        {     
            attackTimer += Time.deltaTime;
            if(attackTimer >= attackTime)
            {
                attackTimer = 0;
                if(state == 0)
                {
                    //anim.SetBool("Attacking", false);
                    Instantiate(soldierPrefab, firePosition.position, firePosition.rotation);
                    soldierCount++;
                    if(soldierCount == soldierLimit)
                    {
                        waitCount = 0;
                        state = 1;
                    }
                }
                if(state == 1)
                {
                    //anim.SetBool("Attacking", false);
                    waitCount++;
                    if(waitCount == waitTime)
                    {
                        soldierCount = 0;
                        state = 0;
                    }
                }
            }
        }
    }
}
