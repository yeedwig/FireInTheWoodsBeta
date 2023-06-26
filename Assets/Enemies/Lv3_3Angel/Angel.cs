using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Angel : MonoBehaviour
{
    public float attackDamage = 50.0f;
    private Animator anim;
    public GameObject GameManager;
    private SpriteRenderer sp;

    [SerializeField] private GameObject Player;
    private MainCharacter main;

    private bool running = false;
    private bool stop = false;
    
    public float health;
    public float startHP = 100;
    
    public bool Dead = false;

    [SerializeField] GameObject[] towardsPathPoints;
    public int towardsNumberOfPoints;
    [SerializeField] float moveSpeed;
    [SerializeField] float runSpeed;
    private Vector3 actualPosition;
    private int pathIndex;
    private Vector3 startPosition;

    void Start()
    {
        anim = GetComponent<Animator>();
        sp = GetComponent<SpriteRenderer>();
        main = Player.GetComponent<MainCharacter>();
        //GetComponent<Animator>().runtimeAnimatorController = level1Override[Random.Range(0,5)] as RuntimeAnimatorController;
        health = startHP;
        startPosition = this.transform.position;
        pathIndex = 0;
    }

    public void Damage(float damage)
    {
        if(running == true)
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
    }


    IEnumerator damagedAnimation()
    {
        anim.SetBool("Damaged", true);
        sp.color = new Color (1,1,1,0.5f);
        yield return new WaitForSeconds(0.3f);
        sp.color = new Color(1,1,1,1);
        anim.SetBool("Damaged", false);

    } 

    void MoveTowardsFire()
    {
        actualPosition = this.transform.position;
        if(stop == false || running == true)
        {
            this.transform.position = Vector3.MoveTowards(actualPosition, towardsPathPoints[pathIndex].transform.position, moveSpeed * Time.deltaTime);
            if(actualPosition == towardsPathPoints[pathIndex].transform.position && pathIndex != towardsNumberOfPoints-1)
            {
                pathIndex++;
            }
        }
    }

    void checkStop()
    {
        if(this.transform.position.x - main.transform.position.x >= 0) //캐릭터가 자신의 왼쪽에 있을때
        {
            if(main.prevX == 1)
            {
                stop = true;
                anim.SetBool("Walking", false);
                anim.SetBool("Stop", true);
            }
            else
            {
                stop = false;
                anim.SetBool("Stop", false);
                anim.SetBool("Walking", true);
            }
        }
        else //캐릭터가 자신의 오른쪽에 있을때
        {
            if(main.prevX == -1)
            {
                stop = true;
                anim.SetBool("Walking", false);
                anim.SetBool("Stop", true);
            }
            else
            {
                stop = false;
                anim.SetBool("Stop", false);
                anim.SetBool("Walking", true);
            }
        }
    }

    void Update()
    {
        if(Player.GetComponent<MainCharacter>().canSee == false)
        {
            stop = false;
            anim.SetBool("Stop", false);
            anim.SetBool("Walking", true);
        }
        else
        {
            checkStop();
        }

        if(Dead == false)
        {
            if(running == true)
            {
                moveSpeed = runSpeed;
                anim.SetBool("Walking", false);
                anim.SetBool("Running", true);
            }
            MoveTowardsFire();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "AngelChangeBorder")
        {
            running = true;
        }
        if(other.gameObject.tag == "Fire")
        {
            //sp.sortingOrder = 5;
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
