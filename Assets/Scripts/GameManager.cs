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

    //타이머 모양
    [SerializeField] private GameObject[] timerShape;

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
    [SerializeField] private Text timer;

    //레벨5
    public GameObject saveload;
    public GameObject saveAlarm;
    public Text saveText;

    public bool autoFireDamage=true;

    public Text healthUI;
    private int returnStage=0;
    public Text currentLevel; 

    private bool bgmChanged=false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Second());
        pause=false;
        kills=0;
        irochiCount=0;
        bgmChanged=false;
        
    }

    // Update is called once per frame
    void Update()
    {   
        if(level==5){
            currentLevel.text="Ready";
        }
        else if(level==11)
        {
            currentLevel.text="BOSS";
        }
        else{
            currentLevel.text="Level "+level.ToString();
        }
        
        overHealth();
        checkDead();
        checkPause();
        changeLevel();
        settingCheck();
        killCount.text=kills.ToString();
        healthUI.text = health.ToString()+"/"+maxHealth.ToString();
    }

    IEnumerator Second(){
        while(autoFireDamage){
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
                    defenseStage=true;
                }
            }
            else{
                if(stage==0){
                    if(level!=5){
                        gameEnd.GetComponent<GameEndManager>().dieBeforeStage=true;
                        level=5;
                        if(!bgmChanged){
                            bgmChanged=true;
                            SoundManager.instance.BackgroundSoundPlay(SoundManager.instance.bgList[1]);
                        }
                        StartCoroutine(level5());// 수정해야됨
                    }
                    
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
                else if(stage==5){
                    if(level!=10){
                        level=10;
                        stageTimer=300;
                        StartCoroutine(TimerForStage());
                    }
                }
                else if(stage==6){
                    if(level!=11){
                        SoundManager.instance.BackgroundSoundPlay(SoundManager.instance.bgList[2]);
                        timerShape[0].SetActive(false);
                        timerShape[1].SetActive(false);
                        timer.text="";
                        level=11;                        
                    }
                }
            }
        }
        else{
            level=-1;
            if(!dead){
                dead=true;
                gameEnd.GetComponent<GameEndManager>().GameClearedStart();
            } 
        }  
    }

    public IEnumerator level5(){
        if(GameStart.newGame){
            GameStart.newGame=false;
            
            yield return new WaitForSeconds(30.0f);
            saveAlarm.SetActive(true);
            saveText.text="도감을 다 모으셨습니다. 밑에 확인 버튼을 누른 뒤 30초 후 모든 아이템들과 현재 상태가 저장됩니다. 이후 스테이지에서 탈락하거나 시작화면서 load할때 해당 상태에서 시작합니다. 바닥에 남아있는 아이템들을 다 먹어주시고 아이템 조합은 유지되는 스텟에만 해주세요! 무기 조합등은 저장 후 하시는 것이 좋답니다.";
            while(saveAlarm.activeInHierarchy){
                yield return new WaitForSeconds(1.0f);
            }
            stageTimer=30;
            //대기타이머
            timerShape[1].SetActive(false);
            timerShape[0].SetActive(true);
            while(stageTimer>0){
                timer.text="<color=#0000ff>"+"0"+(stageTimer/60).ToString()+":"+(stageTimer%60/10).ToString()+(stageTimer%60%10).ToString()+"</color>";
                yield return new WaitForSeconds(1.0f);
                stageTimer--;
            }
            saveload.GetComponent<SaveAndLoad>().Save();
            saveAlarm.SetActive(true);
            saveText.text="취향에 맞는 무기를 사용하고 원하는 동물 정령과 계약하세요! 추가적으로 배리어와 ?? 등은 필요한 스테이지에 사용하시면 됩니다!";
            while(saveAlarm.activeInHierarchy){
                yield return new WaitForSeconds(1.0f);
            }
            
        }
        stageTimer=180;
        //대기 타이머
        timerShape[1].SetActive(false);
        timerShape[0].SetActive(true);
        while(stageTimer>0){
            timer.text="<color=#1f6802>"+"0"+(stageTimer/60).ToString()+":"+(stageTimer%60/10).ToString()+(stageTimer%60%10).ToString()+"</color>";
            yield return new WaitForSeconds(1.0f);
            stageTimer--;
        }
        stage=returnStage+1;
    }
    
    public IEnumerator TimerForStage(){
        returnStage=stage;
        //공격타이머
        timerShape[0].SetActive(false);
        timerShape[1].SetActive(true);
        while(stageTimer>0){
            timer.text="<color=#ffffff>"+"0"+(stageTimer/60).ToString()+":"+(stageTimer%60/10).ToString()+(stageTimer%60%10).ToString()+"</color>";
            yield return new WaitForSeconds(1.0f);
            stageTimer--;
        }
        stage=0;
    }

    public void onClickClose(){
        saveAlarm.SetActive(false);
    }

}
