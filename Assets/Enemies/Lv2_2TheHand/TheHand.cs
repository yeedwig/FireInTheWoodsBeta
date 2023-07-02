using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheHand : MonoBehaviour
{
    private Animator anim;
    public GameObject GameManager;
    private SpriteRenderer sp;
    [SerializeField] GameObject main;
    public float health;
    public float startHP = 100;
    public bool Dead = false;

    private bool Grab;
    [SerializeField] private float grabTime;
    private float grabTimer;
    private float grabState;


    // 위치 관련
    [SerializeField] GameObject[] towardsPathPoints;
    public int towardsNumberOfPoints;

    [SerializeField] float moveSpeed;
    private Vector3 actualPosition;
    private int pathIndex;
    private Vector3 startPosition;

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
        startPosition = this.transform.position;
        this.GetComponent<BoxCollider2D>().enabled = true;
        grabState = 0;
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
        }
        else if(health <= 0)
        {
            anim.SetBool("Dead", true);
            if(!Dead){
                SoundManager.instance.SFXPlay("EnemyDeadSound",clip1);
                GameObject.Find("GameManager").GetComponent<GameManager>().kills++;
            } 
            Dead = true;
            this.GetComponent<BoxCollider2D>().enabled = false;
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

    void MoveTowardsCharacter()
    {
        actualPosition = this.transform.position;


        this.transform.position = Vector3.MoveTowards(actualPosition, towardsPathPoints[pathIndex].transform.position, moveSpeed * Time.deltaTime);
        if(actualPosition == towardsPathPoints[pathIndex].transform.position && pathIndex != towardsNumberOfPoints-1)
        {
            pathIndex++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(this.transform.position.x - main.transform.position.y >= 0)
        {
            this.GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            this.GetComponent<SpriteRenderer>().flipX = false;
        }
        if(Dead == false && Grab == false)
        {
            MoveTowardsCharacter();
        }
        if(Grab == true)
        {
            grabTimer += Time.deltaTime;
            if(grabState == 0)
            {
                main.GetComponent<MainCharacter>().SetMovement(false);
                main.GetComponent<MainCharacter>().SetAction(false);
                anim.SetBool("Grab", true);
                grabState++;
            }   
            if(grabTimer >= grabTime)
            {
                grabTimer = 0;
                main.GetComponent<MainCharacter>().SetMovement(true);
                main.GetComponent<MainCharacter>().SetAction(true);
                anim.SetBool("Grab", false);
                Grab = false;
                anim.SetBool("Dead", true);
                Destroy(this.gameObject,2.0f);
                grabState = 0;
            }

        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player" && Dead == false)
        {
            Grab = true;
            this.GetComponent<BoxCollider2D>().enabled = false;
        }
        // if other is fire set damage to fire and, activate attack animation set path Index to 0, reset enemy
    }
}
