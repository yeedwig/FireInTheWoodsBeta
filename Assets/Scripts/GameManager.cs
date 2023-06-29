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

    //일시정지 했는지 확인
    public bool pause = false;
    [SerializeField] private GameObject inventory;
    [SerializeField] private GameObject settingWindow;
    [SerializeField] private GameObject soundSetting;

    [SerializeField] private Text killCount;

    public GameObject gameEnd;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Second());
        pause=false;
        kills=0;
    }

    // Update is called once per frame
    void Update()
    {
        overHealth();
        checkDead();
        checkPause();
        changeLevel();
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
        if(health<0.0f&&!dead){
            dead=true;
            gameEnd.GetComponent<GameEndManager>().GameOverStart();
        }
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
    public void checkPause(){
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(!pause)
            {
                pause=true;
                Time.timeScale=0;
                inventory.SetActive(false);
                settingWindow.SetActive(true);
                
            }
            else
            {
                pause=false;
                Time.timeScale=1;
                inventory.SetActive(true);
                soundSetting.SetActive(false);
                settingWindow.SetActive(false);
                
            }
        }
    }

    //레벨 변경
    private void changeLevel()
    {
        if(!cleared){
            if(kills<10)
            {
                level=1;
            }
            else if(kills<30)
            {
                if(level!=2){
                    level=2;
                }
            }
            else if(kills<50)
            {
                if(level!=3){
                    level=3;
                }
            }
            else if(kills<70)
            {
                if(level!=4){
                    level=4;
                }
            }
            else if(kills<90)
            {
                if(level!=5){
                    level=5;
                    SoundManager.instance.BackgroundSoundPlay(SoundManager.instance.bgList[1]);
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
            }
        }
        else{
            level=100;
            if(health==0){
                SceneManager.LoadScene("Ending");
            }
        }
        
        
        
    }


}
