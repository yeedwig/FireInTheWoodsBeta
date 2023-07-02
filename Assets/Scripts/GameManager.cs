using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public float health=300.0f;
    public int kills;
    public int level;
    public float maxHealth = 300.0f;
    public float executeDamage = 1000.0f;

    public bool invincible = false;
    public bool dead = false;
    public bool cleared = false; //클리어 여부
    public int irochiCount;

    

    //일시정지 했는지 확인
    public bool pause = false;
    [SerializeField] public GameObject inventory;
    [SerializeField] public GameObject encyclopedia;
    [SerializeField] public GameObject itemBook;
    [SerializeField] private GameObject settingWindow;
    [SerializeField] private GameObject soundSetting;

    [SerializeField] private Text killCount;

    public GameObject gameEnd;

    public int stageTimer = 300;
    public bool defenseStage=false;
    public int stage=0;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Second());
        pause=false;
        kills=0;
        irochiCount=0;
        
    }

    // Update is called once per frame
    void Update()
    {
        overHealth();
        checkDead();
        checkPause();
        changeLevel();
        settingCheck();
        killCount.text=kills.ToString();
    }

    IEnumerator Second(){
        while(true){
            yield return new WaitForSeconds(1.0f);
            takeDamage(1.0f);
        }
    }

    void overHealth(){
        if(health>maxHealth) health=maxHealth;
    }

    void checkDead(){
        if(health<0.0f&&!dead&&!cleared&&!TutorialManager.tutorialDone){
            dead=true;
            gameEnd.GetComponent<GameEndManager>().GameOverStart();
        }
        if(health<0) health=-1.0f;
    }

    public void takeDamage(float damage)
    {
        if(!invincible)
        {
            health-=damage;
        }
        
    }

    public void addMaxHealth(float extraHealth)
    {
        maxHealth+=extraHealth;
        Heal(extraHealth);
    }

    public void Heal(float HealAmount)
    {
        health+=HealAmount;
    }

    public IEnumerator makeInvincible(){
        Debug.Log("invincible On");
        invincible = true;
        yield return new WaitForSeconds(5.0f);
        Debug.Log("invincible On");
        invincible = false;
    }


    //esc 일시정지
    public void settingCheck(){
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(settingWindow.activeInHierarchy){
                inventory.SetActive(true);
                soundSetting.SetActive(false);
                settingWindow.SetActive(false);
            }
            else{
                inventory.SetActive(false);
                settingWindow.SetActive(true);
            }
        }
    }

    void checkPause(){
        if(settingWindow.activeInHierarchy||encyclopedia.activeInHierarchy||itemBook.activeInHierarchy){
            pause=true;
            Time.timeScale=0;
        }
        else{
            pause=false;
            if(Input.GetKey(KeyCode.P)){
                Time.timeScale=100.0f;
            }
            else{
                Time.timeScale=1.0f;
            }
            
        }
    }

    //레벨 변경
    private void changeLevel()
    {
        if(!cleared){
            if(!defenseStage){
                if(irochiCount<2){
                    level=1;
                }
                else if(irochiCount<8){
                    if(level!=2){
                        level=2;
                    }
                }
                else if(irochiCount<15){
                    if(level!=3){
                        level=3;
                    }
                }
                else if(irochiCount<19){
                    if(level!=4){
                        level=4;
                    }
                }
                else if(irochiCount==19){
                    SoundManager.instance.BackgroundSoundPlay(SoundManager.instance.bgList[1]);
                    defenseStage=true;
                }
            }
            else{
                if(stage==0){
                    if(level!=5){
                        level=5;
                    }
                    if(Input.GetKeyDown(KeyCode.O)) stage++;
                }
                else if(stage==1){
                    if(level!=6){
                        level=6;
                        stageTimer=300;
                        StartCoroutine(TimerForStage());
                    }
                }
                else if(stage==2){
                    if(level!=7){
                        level=7;
                        stageTimer=300;
                        StartCoroutine(TimerForStage());
                    }
                }
                else if(stage==3){
                    if(level!=8){
                        level=8;
                        stageTimer=300;
                        StartCoroutine(TimerForStage());
                    }
                }
                else if(stage==4){
                    if(level!=9){
                        level=9;
                        stageTimer=300;
                        StartCoroutine(TimerForStage());
                    }
                }
            }
            
            
            /*if(kills<20)
            {
                level=1;
            }
            else if(kills<50)
            {
                if(level!=2){
                    level=2;
                }
            }
            else if(kills<80)
            {
                if(level!=3){
                    level=3;
                }
            }
            else if(kills<100)
            {
                if(level!=4){
                    level=4;
                }
            }
            else if(kills<90)
            {
                if(level!=5){
                    level=5;
                    
                }
            }

            else if(kills<110)
            {
                if(level!=6){
                    level=6;
                }
            }
            else if(kills<130)
            {
                if(level!=7){
                    level=7;
                }
            }
            else{
                if(level!=8){
                    level=8;
                    SoundManager.instance.BackgroundSoundPlay(SoundManager.instance.bgList[2]);
                }
            }*/
        }
        else{
            level=-1;
            if(!dead){
                dead=true;
                gameEnd.GetComponent<GameEndManager>().GameClearedStart();
            } 
        }  
    }

    public IEnumerator TimerForStage(){
        while(stageTimer>0){
            yield return new WaitForSeconds(1.0f);
            stageTimer--;
        }
        stage++;
        
    }


}
