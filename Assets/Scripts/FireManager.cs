using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireManager : MonoBehaviour
{
    
    [SerializeField] private GameObject fires;
    [SerializeField] private GameObject itemBook;
    public string result;
    
    //드래그 드랍 받아오는 변수들
    public string inputItem;
    //문자열 확인용

    //시간 제한 넣기
    public float currTime = 30.0f;
    private bool checkingInput=false;

    //개수 제한 넣기
    public int count;

    [SerializeField] private GameObject barrier;
    public int barrierLife = 0;

    [SerializeField] private GameObject execute;

    // Start is called before the first frame update
    void Start()
    {
        currTime=30.0f;
        inputItem="";
        count=0;
    }

    // Update is called once per frame
    void Update()
    {
        
        if(inputItem!=""){
            currTime-=Time.deltaTime;
            if(currTime<0.0f)
            {
                currTime=30.0f;
                inputItem="";
                count=0;
            }
            else{
                if(count==3){
                    result = GetComponent<Kindlings>().FindFireType(inputItem);
                    Debug.Log(result);
                    bool[] itemCheck = itemBook.GetComponent<ItemBook>().foundCombinations;
                    //공격 종류
                    if(result=="normalAttack"){
                        GameObject.Find("MainCharacter").GetComponent<MainCharacter>().changeAttackType(0);
                    }
                    if(result=="longAttack"&&itemCheck[0]){
                        GameObject.Find("MainCharacter").GetComponent<MainCharacter>().changeAttackType(1);
                    }
                    if(result=="swordAttack"&&itemCheck[1]){
                        GameObject.Find("MainCharacter").GetComponent<MainCharacter>().changeAttackType(2);
                    }
                    if(result=="largeAttack"&&itemCheck[2]){
                        GameObject.Find("MainCharacter").GetComponent<MainCharacter>().changeAttackType(3);
                    }
                    if(result=="gunAttack"&&itemCheck[3]){
                        GameObject.Find("MainCharacter").GetComponent<MainCharacter>().changeAttackType(4);
                    }
                    if(result=="spinAttack"&&itemCheck[4]){
                        GameObject.Find("MainCharacter").GetComponent<MainCharacter>().changeAttackType(5);
                    }

                    //불 관련
                    if(result=="addHealth"&&itemCheck[5]){
                        GameObject.Find("GameManager").GetComponent<GameManager>().Heal(50.0f);
                    }
                    if(result=="addMaxHealth"&&itemCheck[6]){
                        GameObject.Find("GameManager").GetComponent<GameManager>().addMaxHealth(20.0f);
                    }
                    if(result=="executeDamage"&&itemCheck[7]){
                        if(!execute.activeInHierarchy){
                            execute.SetActive(true);
                        }
                        GameObject.Find("GameManager").GetComponent<GameManager>().executeDamage+=2.5f;
                    }
                    if(result=="barrier"&&itemCheck[8]){
                        barrierLife+=5;
                        if(!barrier.activeInHierarchy){
                            barrier.SetActive(true);
                            StartCoroutine(barrierControl());
                        }
                        
                    }
                    if(result=="invincible"&&itemCheck[9]){
                        StartCoroutine(GameObject.Find("GameManager").GetComponent<GameManager>().makeInvincible());
                    }
                    
                    //계약 종류
                    if(result=="TurtleMode"&&itemCheck[10]){
                        GameObject.Find("MainCharacter").GetComponent<MainCharacter>().changeAnimalMode(2);
                    }
                    if(result=="TigerMode"&&itemCheck[11]){
                        GameObject.Find("MainCharacter").GetComponent<MainCharacter>().changeAnimalMode(4);
                    }
                    if(result=="FalconMode"&&itemCheck[12]){
                        GameObject.Find("MainCharacter").GetComponent<MainCharacter>().changeAnimalMode(1);
                    }
                    if(result=="DeerMode"&&itemCheck[13]){
                        GameObject.Find("MainCharacter").GetComponent<MainCharacter>().changeAnimalMode(0);
                    }
                    if(result=="BearMode"&&itemCheck[14]){
                        GameObject.Find("MainCharacter").GetComponent<MainCharacter>().changeAnimalMode(3);
                    }
                    currTime=-0.1f;
                }
            }
        }
    }

    private IEnumerator barrierControl(){
        while(barrierLife>0){
            yield return new WaitForSeconds(1.0f);
        }
        barrier.SetActive(false);
    }


    
}
