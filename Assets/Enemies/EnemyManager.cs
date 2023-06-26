using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private GameObject enemy;
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
    private float lv1Timer=0,lv2Timer=0,lv3Timer=0,lv4Timer=0,lv5Timer=0,lv6Timer=0,lv7Timer=0;
    // Start is called before the first frame update
    void Start()
    {
        theFearAlive=0;
        fearTimer=0;
        
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
            enemy=Instantiate(level2Enemies[Random.Range(0,2)],startPosition[randomNum].transform.position,Quaternion.identity);
            if(randomNum > 2)
            {
                enemy.GetComponent<SpriteRenderer>().flipX = true;
            }
            lv2Timer=0;
        }
    }
    void level3Gen(float time){
        if(lv3Timer>=time){
            int select = Random.Range(0,2);
            if(select==0)
            {
                enemy=Instantiate(level3Enemies[0],TheHandStartPosition[Random.Range(0,4)].transform.position,Quaternion.identity);
            }
            else{
                enemy=Instantiate(level3Enemies[1],cometStartPosition.transform.position,Quaternion.identity);
            }
            lv3Timer=0;
        }
    }
    void level4Gen(float time){
        if(lv4Timer>=time){
            int select = Random.Range(0,2);
            if(select==0)
            {
                int pos = Random.Range(0,4);
                enemy=Instantiate(level4Enemies[0],wormStartPosition[pos].transform.position,Quaternion.identity);
                if(pos>=2){
                    enemy.GetComponent<SpriteRenderer>().flipX = true;
                }
            }
            else{
                enemy=Instantiate(level4Enemies[1],goblinStartPosition.transform.position,Quaternion.identity);
            }
            lv4Timer=0;
        }
    }

    void level5Gen(float time){
        if(lv5Timer>=time){
            int pos = Random.Range(0,2);
            enemy=Instantiate(TheEye,theEyeStartPosition[pos].transform.position,Quaternion.identity);
            lv5Timer=0;
        }
    }
    void level6Gen(float time){
        if(lv6Timer>=time){
            int pos = Random.Range(0,2);
            enemy=Instantiate(TheAngel,theAngelStartPosition[pos].transform.position,Quaternion.identity);
            if(pos==0){
                enemy.GetComponent<SpriteRenderer>().flipX = true;
            }
            lv6Timer=0;
        }
    }
    void level7Gen(float time){
        if(lv7Timer>=time){
            if(theFearAlive==0){
                enemy=Instantiate(TheFear,theFearPosition[Random.Range(0,6)].transform.position,Quaternion.identity);
                theFearAlive=1; 
            }
            else if(theFearAlive==1){
                fearTimer+=Time.deltaTime;
                if(fearTimer>4.0f&&fearTimer<5.0f){
                    enemy.SetActive(false);
                    
                }
                else if(fearTimer>=5.0f){
                    fearTimer=0;
                    enemy.SetActive(true);
                    enemy.transform.position=theFearPosition[Random.Range(0,6)].transform.position;
                }
            }

            if(enemy.GetComponent<TheFear>()!=null&&enemy.GetComponent<TheFear>().Dead==true){
                  theFearAlive=0;
                  lv7Timer=0;
                  fearTimer=0;  
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        int level = GameObject.Find("GameManager").GetComponent<GameManager>().level;
        //Debug.Log(level);
        lv1Timer+=Time.deltaTime;
        lv2Timer+=Time.deltaTime;
        lv3Timer+=Time.deltaTime;
        lv4Timer+=Time.deltaTime;
        lv5Timer+=Time.deltaTime;
        lv6Timer+=Time.deltaTime;
        lv7Timer+=Time.deltaTime;
        if(level==1){
            level1Gen(15.0f);
        }

        else if(level==2){
            level1Gen(12.0f);
            level2Gen(20.0f);
        }

        else if(level==3){
            level1Gen(10.0f);
            level2Gen(20.0f);
            level3Gen(30.0f);
        }

        else if(level==4){
            level1Gen(10.0f);
            level2Gen(20.0f);
            level3Gen(30.0f);
            level4Gen(30.0f);
        }

        else if(level==5){
            level1Gen(10.0f);
            level2Gen(20.0f);
            level3Gen(30.0f);
            level4Gen(30.0f);
            level5Gen(30.0f);
        }
        else if(level==6){
            level1Gen(10.0f);
            level2Gen(20.0f);
            level3Gen(30.0f);
            level4Gen(30.0f);
            level5Gen(30.0f);
            level6Gen(30.0f);
        }
        else if(level==7){
            level1Gen(10.0f);
            level2Gen(20.0f);
            level3Gen(30.0f);
            level4Gen(30.0f);
            level5Gen(30.0f);
            level6Gen(30.0f);
            level7Gen(30.0f);
        }









        //ending
        else if(level==100){
            if(lv1Timer>1.0f){
                enemy=Instantiate(humma1,startPosition[Random.Range(0,6)].transform.position,Quaternion.identity);
                lv1Timer=0;
            }
        }


        
    }
}
