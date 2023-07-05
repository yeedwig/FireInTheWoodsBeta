using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheFear : MonoBehaviour
{
    public float attackDamage = 20.0f;
    private Animator anim;
    public GameObject GameManager;
    private SpriteRenderer sp;
    public float health;
    public float startHP = 100;
    public bool Dead = false;

    //공격 관련
    [SerializeField] private float attackTime;
    private float attackTimer;

    //사운드 관련
    public AudioClip clip;
    public AudioClip clip1;

    [SerializeField] private GameObject mainCharacterGO;
    private MainCharacter mainCharacter;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        sp = GetComponent<SpriteRenderer>();
        health = startHP;
        mainCharacter=mainCharacterGO.GetComponent<MainCharacter>();
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
                GameManager.GetComponent<GameManager>().kills++;
            }
            Dead = true;
            attackDamage = 0;
            Destroy(this.gameObject,2.0f);
        }
    } 

    IEnumerator damagedAnimation()
    {
        anim.SetBool("Attacked", true);
        sp.color = new Color (1,1,1,0.5f);
        yield return new WaitForSeconds(0.3f);
        sp.color = new Color(1,1,1,1);
        anim.SetBool("Attacked", false);

    }

    void Update()
    {
        attackTimer += Time.deltaTime;
        if(attackTimer > attackTime)
        {
            attackTimer = 0;
            GameManager.GetComponent<GameManager>().takeDamage(attackDamage);
        }
    }
}
