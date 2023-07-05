using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private GameObject enemy;
    private GameObject fearEnemy;
    [SerializeField] GameObject humma1;
    [SerializeField] GameObject[] level2Enemies;
    [SerializeField] GameObject[] level3Enemies;
    [SerializeField] GameObject[] level4Enemies;
    [SerializeField] GameObject[] TheHandStartPosition;
    [SerializeField] GameObject cometStartPosition;
    [SerializeField] GameObject[] wormStartPosition;
    [SerializeField] GameObject goblinStartPosition;
    [SerializeField] GameObject TheEye;
    [SerializeField] GameObject[] theEyeStartPosition;
    [SerializeField] GameObject TheAngel;
    [SerializeField] GameObject[] theAngelStartPosition;
    [SerializeField] GameObject TheFear;
    [SerializeField] GameObject[] theFearPosition;
    public int theFearAlive;
    private float fearTimer;
    
    [SerializeField] GameObject[] startPosition;
    private float lv1Timer=0,lv2Timer=0,lv3Timer=0,lv4Timer=0,lv5Timer=0,lv6Timer=0,lv7Timer=0,lv8Timer=0,lv9Timer=0,lv10Timer=0;


    [SerializeField] GameObject endingHumma;
    [SerializeField] GameObject[] endingPosition;

    private int prevLevel;

    public GameObject boss;
    public GameObject gameManager;

    // Start is called before the first frame update
    void Start()
    {
        theFearAlive=0;
        fearTimer=0;
        prevLevel=1;
    }

    void level1Gen(float time){
        if(lv1Timer>=time){
            enemy=Instantiate(humma1,startPosition[Random.Range(0,6)].transform.position,Quaternion.identity);
            lv1Timer=0;
        }
    }
    void level2Gen(float time){
        if(lv2Timer>=time){
            int randomNum = Random.Range(0,6);
            int type=Random.Range(0,2);
            enemy=Instantiate(level2Enemies[type],startPosition[randomNum].transform.position,Quaternion.identity);
            if(randomNum > 2)
            {
                enemy.GetComponent<SpriteRenderer>().flipX = true;
            }
            
            lv2Timer=0;
        }
    }
    void level3Gen(float time){
        if(lv3Timer>=time){
            enemy=Instantiate(level3Enemies[0],TheHandStartPosition[Random.Range(0,4)].transform.position,Quaternion.identity);
            lv3Timer=0;
        }
    }

    void level4Gen(float time){
        if(lv4Timer>=time){
            enemy=Instantiate(level3Enemies[1],cometStartPosition.transform.position,Quaternion.identity);
            lv4Timer=0;
        }
    }

    void level5Gen(float time){
        if(lv5Timer>=time){
            int pos = Random.Range(0,4);
            enemy=Instantiate(level4Enemies[0],wormStartPosition[pos].transform.position,Quaternion.identity);
            if(pos>=2){
                enemy.GetComponent<SpriteRenderer>().flipX = true;
            }
            lv5Timer=0;
        }
    }

    void level6Gen(float time){
        if(lv6Timer>=time){
            enemy=Instantiate(level4Enemies[1],goblinStartPosition.transform.position,Quaternion.identity);
            lv6Timer=0;
        }
    }
    void level7Gen(float time){
        if(lv7Timer>=time){
            int pos = Random.Range(0,2);
            enemy=Instantiate(TheEye,theEyeStartPosition[pos].transform.position,Quaternion.identity);
            lv7Timer=0;
        }
    }
    void level8Gen(float time){
        if(lv8Timer>=time){
            int pos = Random.Range(0,2);
            enemy=Instantiate(TheAngel,theAngelStartPosition[pos].transform.position,Quaternion.identity);
            if(pos==0){
                enemy.GetComponent<SpriteRenderer>().flipX = true;
            }
            lv8Timer=0;
        }
    }
    void level9Gen(float time){
        if(lv9Timer>=time){
            if(theFearAlive==0){
                fearEnemy=Instantiate(TheFear,theFearPosition[Random.Range(0,6)].transform.position,Quaternion.identity);
                theFearAlive=1; 
            }
            else if(theFearAlive==1){
                fearTimer+=Time.deltaTime;
                if(fearTimer>4.0f&&fearTimer<5.0f){
                    fearEnemy.SetActive(false);
                    
                }
                else if(fearTimer>=5.0f){
                    fearTimer=0;
                    fearEnemy.SetActive(true);
                    fearEnemy.transform.position=theFearPosition[Random.Range(0,5)].transform.position;
                }
            }
            if(fearEnemy.GetComponent<TheFear>()!=null&&fearEnemy.GetComponent<TheFear>().Dead==true){
                  theFearAlive=0;
                  lv9Timer=0;
                  fearTimer=0;  
            }
        }
    }

    
    // Update is called once per frame
    void Update()
    {
        int level = gameManager.GetComponent<GameManager>().level;
        //Debug.Log(level);
        lv1Timer+=Time.deltaTime;
        lv2Timer+=Time.deltaTime;
        lv3Timer+=Time.deltaTime;
        lv4Timer+=Time.deltaTime;
        lv5Timer+=Time.deltaTime;
        lv6Timer+=Time.deltaTime;
        lv7Timer+=Time.deltaTime;
        lv8Timer+=Time.deltaTime;
        lv9Timer+=Time.deltaTime;
        if(level!=prevLevel){
            prevLevel=level;
            breakTime();
            
        } 
        if(level==1){
            level1Gen(11.0f);
        }

        else if(level==2){
            level1Gen(12.0f);
            level2Gen(25.0f);
        }

        else if(level==3){
            level1Gen(12.0f);
            level2Gen(25.0f);
            level3Gen(20.0f);
        }

        else if(level==4){
            level1Gen(10.0f);
            level2Gen(25.0f);
            level3Gen(18.0f);
            level4Gen(40.0f);
        }
        //레벨 5는 저장 및 대기 시간용

        else if(level==6){
            level1Gen(7.0f);
            level2Gen(20.0f);
            level3Gen(15.0f);
            level4Gen(30.0f);
            level5Gen(20.0f);
        }
        else if(level==7){
            level1Gen(7.0f);
            level2Gen(20.0f);
            level3Gen(15.0f);
            level4Gen(30.0f);
            level5Gen(25.0f);
            level6Gen(20.0f);
        }
        else if(level==8){
            level1Gen(6.0f);
            level2Gen(20.0f);
            level3Gen(13.0f);
            level4Gen(30.0f);
            level5Gen(23.0f);
            level6Gen(17.0f);
            level7Gen(50.0f);
        }
        else if(level==9){
            level1Gen(6.0f);
            level2Gen(20.0f);
            level3Gen(13.0f);
            level4Gen(30.0f);
            level5Gen(23.0f);
            level6Gen(17.0f);
            level7Gen(40.0f);
            level8Gen(20.0f);
        }
        else if(level==10){
            level1Gen(6.0f);
            level2Gen(19.0f);
            level3Gen(15.0f);
            level4Gen(35.0f);
            level5Gen(25.0f);
            level6Gen(20.0f);
            level7Gen(45.0f);
            level8Gen(25.0f);
            level9Gen(40.0f);
        }
        else if(level==11){
            boss.SetActive(true);
            level1Gen(15.0f);
            level2Gen(25.0f);
            level3Gen(20.0f);
            level4Gen(40.0f);
            level5Gen(30.0f);
            level6Gen(25.0f);
            level7Gen(50.0f);
            level8Gen(30.0f);
            level9Gen(45.0f);

        }
        //ending
        else if(level==-1){
            if(lv1Timer>0.1f){
                enemy=Instantiate(endingHumma,endingPosition[Random.Range(0,16)].transform.position,Quaternion.identity);
                lv1Timer=0;
            }
        }
    }
    public void breakTime(){
        lv1Timer=0.0f;
        lv2Timer=0.0f;
        lv3Timer=0.0f;
        lv4Timer=0.0f;
        lv5Timer=0.0f;
        lv6Timer=0.0f;
        lv7Timer=0.0f;
        lv8Timer=0.0f;
        lv9Timer=0.0f;
    }
}
