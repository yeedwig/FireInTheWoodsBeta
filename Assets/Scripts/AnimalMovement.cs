using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//내가 아는 선에서 최적화 완료
public class AnimalMovement : MonoBehaviour
{
    private int sequenceNum = 1;
    private Animator anim;
    [SerializeField] private AnimatorOverrideController[] animations;
    private SpriteRenderer sp;
    private string type = "null";

    //animal이 머무는 시간
    [SerializeField] public float dwellingTime = 5.0f;
    public bool isSitting = false;

    //animal이 불을 향해 움직이는 경로
    [SerializeField] GameObject[] towardsPathPoints;
    public int towardsNumberOfPoints;

    //animal이 불로부터 멀어지는 경로
    [SerializeField] GameObject[] awayPathPoints;
    public int awayNumberOfPoints;

    //현재 위치, 및 인덱스
    [SerializeField] float speed;
    private Vector3 actualPosition;
    private int pathIndex;
    private Vector3 startPosition;
    
    //animal이 drop하는 물건
    [SerializeField] GameObject[] droppingItems;
    GameObject droppedItem;
    private bool dropCheck=false;

    [SerializeField] GameObject animalManager;
    private bool checkStarted = false;

    void drop()
    {
        droppedItem = droppingItems[Random.Range(0,droppingItems.Length)];
        GameObject item;
        item=Instantiate(droppedItem,this.transform.position,Quaternion.identity);
        Destroy(item,60.0f);
    }

    //wait -> move -> sit ->drop -> move 식으로 만들고 싶음

    IEnumerator sitting()
    {
        //일정시간동안 sitting
        anim.SetBool("Sitting", true);
        isSitting = true;
        yield return new WaitForSeconds(dwellingTime);
        isSitting = false;
        anim.SetBool("Sitting", false);
        if(sp.flipX == true)
        {
            sp.flipX = false;
        }
        else if(sp.flipX == false)
        {
            sp.flipX = true;
        }
    }

    public void AnimalSequence()
    {
        //sequenceNum을 0부터 시작하게 하여 0에는 숲안쪽에서 기웃기웃하는 모습 넣어도 됨
        if(sequenceNum == 1)//move towards fire
        {
            actualPosition = this.transform.position;
            this.transform.position = Vector3.MoveTowards(actualPosition, towardsPathPoints[pathIndex].transform.position, speed * Time.deltaTime);
            if(actualPosition == towardsPathPoints[pathIndex].transform.position && pathIndex != towardsNumberOfPoints-1)
            {
                //actualPosition = this.transform.position;
                //this.transform.position = Vector3.MoveTowards(actualPosition, towardsPathPoints[pathIndex].transform.position, speed * Time.deltaTime);
                pathIndex++;
            }
            else if(actualPosition == towardsPathPoints[pathIndex].transform.position && pathIndex == towardsNumberOfPoints-1)// 2와 3사이의 거리가 너무 멀어지면 3에 도달하기 전에 앉아버림 문제 해결해야함
            {
                
                sequenceNum++;
            }
        }
        else if (sequenceNum == 2)//sit on fire
        {
            StartCoroutine(sitting());
            pathIndex = 0;
            sequenceNum++;
        }
        else if(sequenceNum == 3 && isSitting == false)//move away from fire
        {
            actualPosition = this.transform.position;
            this.transform.position = Vector3.MoveTowards(actualPosition, awayPathPoints[pathIndex].transform.position, speed * Time.deltaTime);
            if(actualPosition == awayPathPoints[pathIndex].transform.position && pathIndex != awayNumberOfPoints -1)
            {
                pathIndex++;
                //if(pathIndex == awayNumberOfPoints / 2)
                if(pathIndex == 1&&dropCheck)
                {
                    dropCheck=false;
                    drop();
                }
            }
            else if(actualPosition == awayPathPoints[pathIndex].transform.position && pathIndex == awayNumberOfPoints -1)
            {
                sequenceNum++;
            } 
        }
        if(sequenceNum == 4)
        {
            this.GetComponent<Animals>().currentState=false;
            gameObject.SetActive(false);
            if(sp.flipX == true)
            {
            sp.flipX = false;
            }
            else if(sp.flipX == false)
            {
            sp.flipX = true;
            }
            this.transform.position = startPosition;
            sequenceNum=1;
            animalManager.GetComponent<AnimalManager>().seats[this.GetComponent<Animals>().arrivingPosition]=false;
            checkStarted = false;
        }

    }

    void Start()
    {
        sp = GetComponent<SpriteRenderer>();
        startPosition = this.transform.position;
        anim = GetComponent<Animator>();
        //GetComponent<Animator>().runtimeAnimatorController;
        //= ver2 as RuntimeAnimatorController;
        dropCheck=false;
        
        pathIndex = 0;
        sequenceNum = 1;

        //아이템을 드랍할건지 안할건지 결정
        


    }

    void Update()
    {
        //state가 true일때 움직이기 시작
        if(this.GetComponent<Animals>().currentState)
        {
            if(checkStarted==false){
                checkStarted = true;
                int typeSelect = this.GetComponent<Animals>().currentType;
                GetComponent<Animator>().runtimeAnimatorController = animations[typeSelect] as RuntimeAnimatorController;
                int test = Random.Range(0,100);
                if(test<80) dropCheck=true;
                else dropCheck=false;
                
            }
            AnimalSequence();
        }
    }
}
