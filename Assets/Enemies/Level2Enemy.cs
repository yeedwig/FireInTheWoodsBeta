using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2Enemy : MonoBehaviour
{
    public float attackDamage = 20.0f;
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


    [SerializeField] private int sequenceNum;

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
            //sp.sortingOrder = 5;
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
            Damage(100000.0f);
            GameObject.Find("fire").GetComponent<FireManager>().barrierLife--;
        }
        // if other is fire set damage to fire and, activate attack animation set path Index to 0, reset enemy
    }

    
}
