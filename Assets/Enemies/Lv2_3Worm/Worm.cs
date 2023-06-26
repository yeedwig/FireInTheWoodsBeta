using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worm : MonoBehaviour
{

    public float attackDamage = 10.0f;
    private Animator anim;
    public GameObject GameManager;
    [SerializeField] GameObject worm;
    private SpriteRenderer sp;
    private int status = 0;
    public float health;
    public float startHP = 100;
    public bool Dead = false;

    //사라지는 타이머 관련
    private float appearTimer;
    [SerializeField] private float appearTime;
    [SerializeField] private float currentState;

    // 위치 관련
    [SerializeField] GameObject[] towardsPathPoints;
    public int towardsNumberOfPoints;

    [SerializeField] float moveSpeed;
    private Vector3 actualPosition;
    private int pathIndex;
    private Vector3 startPosition;


    [SerializeField] private int sequenceNum;

    [SerializeField] private AnimatorOverrideController[] level1Override;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        sp = GetComponent<SpriteRenderer>();
        //GetComponent<Animator>().runtimeAnimatorController = level1Override[Random.Range(0,5)] as RuntimeAnimatorController;
        health = startHP;
        //startPosition = this.transform.position;
        pathIndex = 0;
    }

    public void Damage(float damage)
    {
        health -= damage;
        if(GameObject.Find("MainCharacter").GetComponent<MainCharacter>().currentAnimalMode==3)
        {
            health -= 20.0f;
        }
        else if(GameObject.Find("MainCharacter").GetComponent<MainCharacter>().currentAnimalMode==4)
        {
            health -= 10.0f;
        }
        
        
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
            appearTimer += Time.deltaTime;
            if(appearTimer >= appearTime)
            {
                currentState++;
                appearTimer = 0;
                if(currentState < 3)
                {
                    if(currentState == 1)
                    {
                        anim.SetBool("Walking", false);
                        anim.SetBool("Disappear", true);
                        gameObject.GetComponent<BoxCollider2D>().enabled = false;
                    }
                    if(currentState == 2)
                    {
                        gameObject.GetComponent<BoxCollider2D>().enabled = true;
                        anim.SetBool("Disappear", false);
                        anim.SetBool("Appear", true);
                    }
                }
                else
                {
                    currentState = 0;
                    anim.SetBool("Appear", false);
                    anim.SetBool("Walking", true);
                }
            }
        }
        //타이머를 설정하여 타이머 따라서 사라지고 다시 나타나고 하게 하기
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
        /// if other is fire set damage to fire and, activate attack animation set path Index to 0, reset enemy
    }
}
