using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowSoldier : MonoBehaviour
{
    public float attackDamage = 50.0f;
    private Animator anim;
    public GameObject GameManager;
    private SpriteRenderer sp;
    private int status = 0;
    public float health;
    public float startHP = 200;
    public bool Dead = false;


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
        pathIndex = 0;
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
        
        if(health <= 0)
        {
            anim.SetBool("Dead", true);
            attackDamage = 0;
            Destroy(gameObject, 2.0f);
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

    void MoveTowardsFire()
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
        if(Dead == false)
        {
            MoveTowardsFire();
        }
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
            Damage(GameObject.Find("GameManager").GetComponent<GameManager>().executeDamage,false);
        }
        else if(other.gameObject.tag == "Barrier")
        {
            Damage(100000.0f);
            GameObject.Find("fire").GetComponent<FireManager>().barrierLife--;
        }
    }

}
