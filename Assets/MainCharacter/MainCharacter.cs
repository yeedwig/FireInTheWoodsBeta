using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainCharacter : MonoBehaviour
{
    int testIndex = 0;

    Rigidbody2D rb;
    public Animator anim;
    private SpriteRenderer sprite;

    private bool canAction;
    //Camera State
    [SerializeField] private bool cameraMode = true;
    [SerializeField] Image CameraImage;
    [SerializeField] Sprite[] CameraSprites;

    //Movement
    float horizontal;
    float vertical;
    Vector2 vector;
    [SerializeField] private float defaultMoveSpeed = 10.0f;
    public float moveSpeed;
    public float prevX;
    public float prevY;
    private Transform currentPosition;
    private bool canMove = true;

    //Camera Area
    [SerializeField] GameObject RightBackCamera;
    [SerializeField] GameObject LeftBackCamera;
    [SerializeField] GameObject RightFrontCamera;
    [SerializeField] GameObject LeftFrontCamera;
    private bool takingPicture = false;
    private float timeToPicture = 0.25f;
    private float takePictureTimer = 0f;

    //Attack
    public int attackType = -1;
    [SerializeField] GameObject[] LeftBackAttackAreas;
    [SerializeField] GameObject[] RightBackAttackAreas;
    [SerializeField] GameObject[] LeftFrontAttackAreas;
    [SerializeField] GameObject[] RightFrontAttackAreas;
    private bool attacking = false;
    private bool canAttack = true;
    [SerializeField] private float attackAppearTime = 0.25f;
    private float attackAppearTimer =  0f;
    private float attackWaitTimer = 0f;
    [SerializeField] private float attackWaitTime = 2.0f;

    //Shooting Attack
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform[] firePosition;

    //Tornado Attack
    [SerializeField] private GameObject tornadoAttack;

    //Animal Mode
    public int animalMode = -1;
    [SerializeField] private GameObject[] animalModes;
    public int currentAnimalMode = -1;
    private bool autoHealOn = false;
    [SerializeField] private float deerMoveSpeed;
    [SerializeField] private float turtleMoveSpeed;

    //sound 관련
    private int moveCounter=0;
    public AudioClip clip;
    public AudioClip clip1;
    public AudioClip clip2;
    public AudioClip clip3;
    public AudioClip clip4;
    public AudioClip clip5;
    public AudioClip clip6;
    public AudioClip walkClip;
    
    //스탯 관련
    private float attackSpeedUp;
    private float attackUp;
    [SerializeField] private float moveSpeedUp;

    //시각 관련
    [SerializeField] private UnityEngine.Rendering.Universal.Light2D mainLight;
    public bool canSee;

    //공격력 증가 관련
    public float plusDamageByAnimalContract=0;
    public float plusDamageByItem=0;

    //이속 증가 관련
    public float plusSpeedByAnimalContract=0;
    public float plusSpeedByItem=0;

    //공속 증가 관련
    public float plusAttackSpeedByAnimalContract=0;
    public float plusAttackSpeedByItem=0;

    //스탯창 관련
    public Text damageUI;
    public Text attackSpeedUI;
    public Text speedUI;

    public GameObject present;

    void reset()
    {
        attackWaitTime = 2.0f - attackSpeedUp;
        if(currentAnimalMode!=2&&currentAnimalMode!=4){
            autoHealOn=false;
        }

        //공격력, 이속, 공속 초기화
        plusDamageByAnimalContract=0;
        plusSpeedByAnimalContract=0;
        plusAttackSpeedByAnimalContract=0;
    }
    public void SetMovement(bool state)
    {
        canMove = state;
    }
    public void SetAction(bool state)
    {
        canAction = state;
    }

    void Move()
    {
        //움직임
        if(Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {  
            moveCounter++;
            if(moveCounter>10){
                moveCounter=0;
                SoundManager.instance.SFXPlay("WalkSound",walkClip);
            }
            if(vector.x != 0)
            {
                prevX = vector.x;
            }

            if(vector.y != 0)
            {
                prevY = vector.y;
            }
                                            
            if(vector.y > 0) // 뒤로 가는경우
            {                                                                                          
                
                anim.SetFloat("DirX", vector.x);
                anim.SetFloat("DirY",1);
            }
            else
            {
                anim.SetFloat("DirX", vector.x);
                anim.SetFloat("DirY", -1);
            }

            anim.SetBool("Walking", true);
            moveSpeed=defaultMoveSpeed+plusSpeedByAnimalContract+plusSpeedByItem;
            rb.MovePosition(rb.position + vector * moveSpeed * Time.deltaTime);
        }
        else
        {
            rb.MovePosition(rb.position + vector * 0 * Time.deltaTime);
            if(prevY > 0)
            {
                anim.SetFloat("DirX", prevX);
                anim.SetFloat("DirY",1);
            }
            else
            {
                anim.SetFloat("DirX", prevX);
                anim.SetFloat("DirY", -1);
            }
           anim.SetBool("Walking", false);
        }
    }

    void DefaultAttack()//attackType 0
    {
        SoundManager.instance.SFXPlay("AttackCameraSound1",clip1);
        if(prevX > 0 && prevY > 0) //대각선 오른쪽 뒤
        {
            RightBackAttackAreas[attackType].SetActive(attacking);
                
        }
        else if(prevX < 0 && prevY > 0) //대각선 왼쪽 뒤
        {
            LeftBackAttackAreas[attackType].SetActive(attacking);
        }
        else if(prevX > 0 && prevY < 0)
        {
            RightFrontAttackAreas[attackType].SetActive(attacking);
        }
        else if(prevX < 0 && prevY < 0)
        {
            LeftFrontAttackAreas[attackType].SetActive(attacking);
        }
        //canAttack = false;
        
    }

    void RayAttack() //attackType 1
    {
        SoundManager.instance.SFXPlay("AttackCameraSound2",clip6);
        if(prevX > 0 && prevY > 0) //대각선 오른쪽 뒤
        {
            RightBackAttackAreas[attackType].SetActive(attacking);
        }
        else if(prevX < 0 && prevY > 0) //대각선 왼쪽 뒤
        {
            LeftBackAttackAreas[attackType].SetActive(attacking);
        }
        else if(prevX > 0 && prevY < 0)
        {
            RightFrontAttackAreas[attackType].SetActive(attacking);
        }
        else if(prevX < 0 && prevY < 0)
        {
            LeftFrontAttackAreas[attackType].SetActive(attacking);
        }
    }

    void SwordAttack() //attackType 2
    {
        SoundManager.instance.SFXPlay("AttackCameraSound3",clip3);
        if(prevX > 0 && prevY > 0) //대각선 오른쪽 뒤
        {
            RightBackAttackAreas[attackType].SetActive(attacking);
        }
        else if(prevX < 0 && prevY > 0) //대각선 왼쪽 뒤
        {
            LeftBackAttackAreas[attackType].SetActive(attacking);
        }
        else if(prevX > 0 && prevY < 0)
        {
            RightFrontAttackAreas[attackType].SetActive(attacking);
        }
        else if(prevX < 0 && prevY < 0)
        {
            LeftFrontAttackAreas[attackType].SetActive(attacking);
        }
    }
    
    void FarAwayAttack() //attackType 3
    {
        SoundManager.instance.SFXPlay("AttackCameraSound4",clip4);
        if(prevX > 0 && prevY > 0) //대각선 오른쪽 뒤
        {
            RightBackAttackAreas[attackType].SetActive(attacking);
        }
        else if(prevX < 0 && prevY > 0) //대각선 왼쪽 뒤
        {
            LeftBackAttackAreas[attackType].SetActive(attacking);
        }
        else if(prevX > 0 && prevY < 0)
        {
            RightFrontAttackAreas[attackType].SetActive(attacking);
        }
        else if(prevX < 0 && prevY < 0)
        {
            LeftFrontAttackAreas[attackType].SetActive(attacking);
        }
    }

    void ShootingAttack()
    {
        SoundManager.instance.SFXPlay("AttackCameraSound5",clip5);
        bulletPrefab.GetComponent<Projectile>().dirX = prevX;
        bulletPrefab.GetComponent<Projectile>().dirY = prevY;
        if(prevX > 0 && prevY > 0) //대각선 오른쪽 뒤
        {
            RightBackAttackAreas[attackType].SetActive(attacking);
            Instantiate(bulletPrefab, firePosition[0].position, firePosition[0].rotation);
        }
        else if(prevX < 0 && prevY > 0) //대각선 왼쪽 뒤
        {
            LeftBackAttackAreas[attackType].SetActive(attacking);
            Instantiate(bulletPrefab, firePosition[1].position, firePosition[1].rotation);
        }
        else if(prevX > 0 && prevY < 0)
        {
            RightFrontAttackAreas[attackType].SetActive(attacking);
            Instantiate(bulletPrefab, firePosition[2].position, firePosition[2].rotation);
        }
        else if(prevX < 0 && prevY < 0)
        {
            LeftFrontAttackAreas[attackType].SetActive(attacking);
            Instantiate(bulletPrefab, firePosition[3].position, firePosition[3].rotation);
        }

    }

    void TornadoAttack()
    {
        SoundManager.instance.SFXPlay("AttackCameraSound6",clip2);
        tornadoAttack.SetActive(true);
    }


    IEnumerator takePictureAnimation()
    {
        if(prevY > 0)
        {
            anim.SetFloat("DirX", prevX);
            anim.SetFloat("DirY",1);
        }
        else
        {
            anim.SetFloat("DirX", prevX);
            anim.SetFloat("DirY", -1);
        }
        anim.SetBool("Attacking",true);
        yield return new WaitForSeconds(0.3f);
        anim.SetBool("Attacking",false);

    }

    void takePicture()
    {
        if(Input.GetKeyDown(KeyCode.Space) && cameraMode == true)
        {
            SoundManager.instance.SFXPlay("AnimalCameraSound",clip);
            canMove = false;
            canAttack = false;
            takingPicture = true;
            StartCoroutine(takePictureAnimation());

            if(prevX > 0 && prevY > 0) //대각선 오른쪽 뒤
            {
                RightBackCamera.SetActive(true);
            }
            else if(prevX < 0 && prevY > 0) //대각선 왼쪽 뒤
            {
                LeftBackCamera.SetActive(true);
            }
            else if(prevX > 0 && prevY < 0)
            {
                RightFrontCamera.SetActive(true);
            }
            else if(prevX < 0 && prevY < 0)
            {
                LeftFrontCamera.SetActive(true);
            }
        }

        if(takingPicture)//공격 모션이 떠 있는 시간
        {
            takePictureTimer += Time.deltaTime;
            if(takePictureTimer >= timeToPicture)
            {
                takePictureTimer = 0;
                takingPicture = false;

                canMove = true;

                RightBackCamera.SetActive(false);
                LeftBackCamera.SetActive(false);
                RightFrontCamera.SetActive(false);
                LeftFrontCamera.SetActive(false);
            }
        }
    }


    IEnumerator attackAnimation()
    {
        if(prevY > 0)
        {
            anim.SetFloat("DirX", prevX);
            anim.SetFloat("DirY",1);
        }
        else
        {
            anim.SetFloat("DirX", prevX);
            anim.SetFloat("DirY", -1);
        }
        anim.SetBool("Attacking",true);
        yield return new WaitForSeconds(0.3f);
        anim.SetBool("Attacking",false);

    }


    void Attack()
    {
        if(Input.GetKeyDown(KeyCode.Space) && canAttack == true && cameraMode == false)
        {
            
            attacking = true;
            canMove = false;
            canAttack = false;
            StartCoroutine(attackAnimation());

            
            if(attackType == 0) //Default
            {
                //timer setting도 type별로 여기서 진행
                attackWaitTime = 0.5f +plusAttackSpeedByAnimalContract+plusAttackSpeedByItem;
                attackAppearTime = 0.25f;
                
                DefaultAttack();
            }
            if(attackType == 1) //Ray
            {
                //달리면서 공격을쓰면 달리는 모션이 그래도 됨
                attackAppearTime = 0.7f;
                attackWaitTime = 0.8f + plusAttackSpeedByAnimalContract+plusAttackSpeedByItem;
                RayAttack();
            }
            if(attackType == 2) //Sword
            {
                attackAppearTime = 0.7f;
                attackWaitTime = 1.0f + plusAttackSpeedByAnimalContract+plusAttackSpeedByItem;
                SwordAttack();
            }
            if(attackType == 3) //Far
            {
                attackAppearTime = 0.25f;
                attackWaitTime = 0.5f + plusAttackSpeedByAnimalContract+plusAttackSpeedByItem;
                FarAwayAttack();
            }
            if(attackType == 4) //Shooting
            {
                attackAppearTime = 0.25f;
                attackWaitTime = 0.8f + plusAttackSpeedByAnimalContract+plusAttackSpeedByItem;
                ShootingAttack();
            }
            
            if(attackType == 5)
            {
                canMove = true;
                attackAppearTime = 5.0f;
                attackWaitTime = 2.0f + plusAttackSpeedByAnimalContract+plusAttackSpeedByItem;
                TornadoAttack();
            }

        }

        if(attacking)//공격 모션이 떠 있는 시간
        {
            attackAppearTimer += Time.deltaTime;
            if(attackAppearTimer >= attackAppearTime)
            {
                attackAppearTimer = 0;
                attacking = false;
                if(attackType < 5)
                {
                    LeftBackAttackAreas[attackType].SetActive(false);
                    RightBackAttackAreas[attackType].SetActive(false);
                    LeftFrontAttackAreas[attackType].SetActive(false);
                    RightFrontAttackAreas[attackType].SetActive(false);
                }
                else if(attackType == 5)
                {
                    tornadoAttack.SetActive(false);
                }
                canMove = true;
            }
        }

        if(canAttack == false && attacking == false)// 공격 대기 시간 타이머
        {
            attackWaitTimer += Time.deltaTime;
            if(attackWaitTimer >= attackWaitTime)
            {
                attackWaitTimer = 0;
                canAttack = true;
            }
        }
    }

    void changeMode()
    {
        if(cameraMode == true)
        {
            cameraMode = false;
            CameraImage.sprite = CameraSprites[0]; 
        }
        else
        {
            cameraMode = true;
            CameraImage.sprite = CameraSprites[1]; 
        }
    }

    public void changeAttackType(int typeNum)
    {
        attackType = typeNum;
        if(attackType == 0) //Default
        {
            //timer setting도 type별로 여기서 진행
            attackWaitTime = 0.5f +plusAttackSpeedByAnimalContract+plusAttackSpeedByItem;
            attackAppearTime = 0.25f;
        }
        if(attackType == 1) //Ray
        {
            //달리면서 공격을쓰면 달리는 모션이 그래도 됨
            attackAppearTime = 0.7f;
            attackWaitTime = 0.8f + plusAttackSpeedByAnimalContract+plusAttackSpeedByItem;
        }
        if(attackType == 2) //Sword
        {
            attackAppearTime = 0.7f;
            attackWaitTime = 1.0f + plusAttackSpeedByAnimalContract+plusAttackSpeedByItem;
        }
        if(attackType == 3) //Far
        {
            attackAppearTime = 0.25f;
            attackWaitTime = 0.5f + plusAttackSpeedByAnimalContract+plusAttackSpeedByItem;
        }
        if(attackType == 4) //Shooting
        {
            attackAppearTime = 0.25f;
            attackWaitTime = 0.5f + plusAttackSpeedByAnimalContract+plusAttackSpeedByItem;
        }
        
        if(attackType == 5)
        {
            attackAppearTime = 5.0f;
            attackWaitTime = 2.0f + plusAttackSpeedByAnimalContract+plusAttackSpeedByItem;
        }
    }

    IEnumerator autoHeal(){
                while(currentAnimalMode==2||currentAnimalMode==4){
                    if(currentAnimalMode==2){
                        GameObject.Find("GameManager").GetComponent<GameManager>().Heal(10.0f);
                    }
                    else{
                        GameObject.Find("GameManager").GetComponent<GameManager>().Heal(5.0f);
                    }
                    yield return new WaitForSeconds(1.0f);
                }
            }

    public void changeAnimalMode(int modeNum)
    {
        currentAnimalMode=modeNum;
        animalModes[0].SetActive(false);
        animalModes[1].SetActive(false);
        animalModes[2].SetActive(false);
        animalModes[3].SetActive(false);
        animalModes[4].SetActive(false);
        if(modeNum == -1)
        {
            animalModes[0].SetActive(false);
            animalModes[1].SetActive(false);
            animalModes[2].SetActive(false);
            animalModes[3].SetActive(false);
            animalModes[4].SetActive(false);
        }
        else
        {
            animalModes[modeNum].SetActive(true);
        }

        if(modeNum == 0)//Deer
        {
            //movement Speed UP
            reset();
            plusSpeedByAnimalContract=5.0f;
        }

        else if(modeNum == 1)//Falcon
        {
            //set Attack Timer Delay down -> Attack Speed up
            reset();
            plusAttackSpeedByAnimalContract=-0.2f;
        }

        else if(modeNum == 2)//Turtle
        {
            reset();
            if(!autoHealOn){
                autoHealOn=true;
                StartCoroutine(autoHeal());
            }
            plusSpeedByAnimalContract=-1.0f;
        }

        else if(modeNum == 3)//Bear
        {
            reset();
            plusDamageByAnimalContract=30.0f;
        }

        else if(modeNum == 4)//Tiger
        {
            reset();
            plusDamageByAnimalContract=20.0f;
            plusSpeedByAnimalContract=3.0f;
            plusAttackSpeedByAnimalContract=-0.1f;

            if(!autoHealOn)
            {
                StartCoroutine(autoHeal());
            }
            
            //set all abilities higher but not as high as the other modes
        }
    }

    void checkCanSee()
    {
        if(mainLight.intensity < 0.4f)
        {
            canSee = false;
        }
        else
        {
            canSee = true;
        }
    }
    void changeStat(){
        if(attackType==0){
            damageUI.text = (50+plusDamageByAnimalContract+plusDamageByItem).ToString("F2");
            attackSpeedUI.text=(1/(attackWaitTime+attackAppearTime)).ToString("F2");
        }
        else if(attackType==1){
            damageUI.text = (75+plusDamageByAnimalContract+plusDamageByItem).ToString("F2");
            attackSpeedUI.text=(1/(attackWaitTime+attackAppearTime)).ToString("F2");
        }
        else if(attackType==2){
            damageUI.text = (75+plusDamageByAnimalContract+plusDamageByItem).ToString("F2");
            attackSpeedUI.text=(1/(attackWaitTime+attackAppearTime)).ToString("F2");
        }
        else if(attackType==3){
            damageUI.text = (50+plusDamageByAnimalContract+plusDamageByItem).ToString("F2");
            attackSpeedUI.text=(1/(attackWaitTime+attackAppearTime)).ToString("F2");
        }
        else if(attackType==4){
            damageUI.text = (50+plusDamageByAnimalContract+plusDamageByItem).ToString("F2");
            attackSpeedUI.text=(1/(attackWaitTime+attackAppearTime)).ToString("F2");
        }
        else if(attackType==5){
            damageUI.text = (1+(plusDamageByAnimalContract+plusDamageByItem)*0.05).ToString("F2");
            attackSpeedUI.text=(250/(attackWaitTime+attackAppearTime)).ToString("F2");
        }
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        canAction = true;
        attackSpeedUp = 0.0f; //저장할때 업그레이드도 저장될거면 이거를 빼야할거 같긴함, 아래 업그레이드 관련된거들도
        moveSpeed = defaultMoveSpeed;
        prevX=1.0f;
        prevY=-1.0f;
        if(attackType == 0) //Default
        {
            //timer setting도 type별로 여기서 진행
            attackWaitTime = 0.5f +plusAttackSpeedByAnimalContract+plusAttackSpeedByItem;
            attackAppearTime = 0.25f;
        }
        if(attackType == 1) //Ray
        {
            //달리면서 공격을쓰면 달리는 모션이 그래도 됨
            attackAppearTime = 0.7f;
            attackWaitTime = 0.8f + plusAttackSpeedByAnimalContract+plusAttackSpeedByItem;
        }
        if(attackType == 2) //Sword
        {
            attackAppearTime = 0.7f;
            attackWaitTime = 1.0f + plusAttackSpeedByAnimalContract+plusAttackSpeedByItem;
        }
        if(attackType == 3) //Far
        {
            attackAppearTime = 0.25f;
            attackWaitTime = 0.5f + plusAttackSpeedByAnimalContract+plusAttackSpeedByItem;
        }
        if(attackType == 4) //Shooting
        {
            attackAppearTime = 0.25f;
            attackWaitTime = 0.5f + plusAttackSpeedByAnimalContract+plusAttackSpeedByItem;
        }
        
        if(attackType == 5)
        {
            attackAppearTime = 5.0f;
            attackWaitTime = 2.0f + plusAttackSpeedByAnimalContract+plusAttackSpeedByItem;
        }
    }

    void Update()
    {
        changeStat();
        //damageUI.text = health.ToString();
        //attackSpeedUI.text = (1/attackWaitTime).ToString("F2");
        speedUI.text = moveSpeed.ToString();
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        vector.Set(horizontal, vertical);
        if(canAction == true)
        {
            takePicture();// 카메라 모드일 경우
            Attack();// 공격 모드일 경우
        }
        if(Input.GetKeyDown(KeyCode.E))
        {
            changeMode();
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            if(testIndex == 5)
            {
                testIndex = 0;
            }
            changeAnimalMode(testIndex);
            testIndex++;
        }
        checkCanSee();
    }

    void FixedUpdate()
    {
        if(canMove == true)//|| (attackType == 5 && cameraMode == false))
        {
            Move();
        }
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.name=="present"){
            present.SetActive(false);
        }
    }


}
