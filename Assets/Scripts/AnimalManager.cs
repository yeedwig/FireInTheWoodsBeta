using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

//저장 부분 제외하면 최적화 완료
public class AnimalManager : MonoBehaviour
{

    public GameObject[] animalsInNormal;//노말 불일때 나오는 동물들
    public bool[] seats = new bool[5]{false,false,false,false,false};  //총 5개의 자리를 표현 false일 경우 비어있고 true일 경우 차있음

    //시간 간격을 확인하기 위한 변수들
    private float currTime;
    //10으로변경할것
    [SerializeField] int timeDelay;
    public int RandomAnimalID;

    private Animals animalInstance; //객체를 가져오기 위한 변수
    private AnimalMovement animalMovement; //움직임 스크립트 받아옴
    public int typeSelect; //색 타입 선정

    //이로치 발견시 값들 넣기 위한 객체들 선언
    public GameObject itemBook;

    public GameObject effectManager;

    public GameObject mainCharacterGO;
    public MainCharacter mainCharacter;
    public GameObject camera;

    public GameObject gameManager;

    void Start(){
        timeDelay=10;
        mainCharacter=mainCharacterGO.GetComponent<MainCharacter>();
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P)){
            irochiUpgrade("Warthog");
        }


        currTime +=Time.deltaTime; //시간 계산
        if(currTime>timeDelay) //변경
        {
            //배열 안에서 ID를 구하고 해당 객체를 받아옴
            RandomAnimalID = Random.Range(0,animalsInNormal.Length);
            animalInstance = animalsInNormal[RandomAnimalID].GetComponent<Animals>();
            
            //현재 실행 중인 놈이 아니고 자리가 차 있지 않으면
            if(!animalInstance.currentState&&!seats[animalInstance.arrivingPosition])
            {
                
                //객체당 확률 확인
                int probability = Random.Range(0,100);
                if(probability<animalInstance.appearPercent)
                {
                    currTime=0;
                    //객체 출현시 기본, 색 변화, 이로치 중에서 선택
                    //1번 생성되면 2번 생성가능, 2번 생성되면 3번 생성 가능
                    int selectingNum = Random.Range(0,100);
                    if(!animalInstance.typeAppeared[0])
                    {
                        typeSelect=0;
                    }
                    else if(!animalInstance.typeAppeared[1])
                    {
                        typeSelect=1;
                    }
                    else if(!animalInstance.typeAppeared[2])
                    {
                        
                        typeSelect=2;
                    }
                    else{
                        typeSelect=Random.Range(0,3);
                    }

                    //시간 테스트용 나중에 삭제 ㄱ
                    if(!animalInstance.typeAppeared[typeSelect]){
                        animalInstance.typeAppeared[typeSelect]=true;
                        if(typeSelect==2){
                            GameObject.Find("GameManager").GetComponent<GameManager>().irochiCount++;
                            irochiUpgrade(animalInstance.name);
                        } 
                    }
                    

                    
                    //확률을 30프로까지 내림
                    if(animalInstance.appearPercent>=40){
                        animalInstance.appearPercent-=20;
                    }
                    //실행
                    animalInstance.currentType=typeSelect;
                    //이로치 처음 발견시 확인
                
                    animalInstance.currentState=true;
                    seats[animalInstance.arrivingPosition]=true;
                    animalInstance.gameObject.SetActive(true);
                }               
            }
        }
    }

    public void irochiUpgrade(string name)
    {
        switch (name)
        {
            case("Bear"):
                itemBook.GetComponent<ItemBook>().foundCombinations[14]=true;
                effectManager.GetComponent<EffectManager>().NewitemBookFound();
                break;
            
            case("Raccoon"):
                //아이템 보따리
                break;

            case("Deer"):
                mainCharacter.plusSpeedByItem+=3.0f;
                break;
            
            case("Rabbit"):
                itemBook.GetComponent<ItemBook>().foundCombinations[1]=true;
                effectManager.GetComponent<EffectManager>().NewitemBookFound();
                break;

            case("HedgeHog"):
                itemBook.GetComponent<ItemBook>().foundCombinations[7]=true;
                effectManager.GetComponent<EffectManager>().NewitemBookFound();
                break;
            
            case("WartHog"):
                mainCharacter.plusDamageByItem+=50;
                break;

            case("Owl"):
                //시야 범위 넓힘 또는 불 범위 넓어짐
                break;
            
            case("Beaver"):
                itemBook.GetComponent<ItemBook>().foundCombinations[8]=true;
                effectManager.GetComponent<EffectManager>().NewitemBookFound();
                break;

            case("Mole"):
                itemBook.GetComponent<ItemBook>().foundCombinations[9]=true;
                effectManager.GetComponent<EffectManager>().NewitemBookFound();
                break;
            
            case("Buck"):
                itemBook.GetComponent<ItemBook>().foundCombinations[13]=true;
                effectManager.GetComponent<EffectManager>().NewitemBookFound();
                break;

            case("Weasel"):
                Debug.Log("ok");
                mainCharacter.plusAttackSpeedByItem-=0.1f;
                break;
            
            case("Wolf"):
                gameManager.GetComponent<GameManager>().autoFireDamage=false;
                break;

            case("Fox"):
                itemBook.GetComponent<ItemBook>().foundCombinations[3]=true;
                effectManager.GetComponent<EffectManager>().NewitemBookFound();
                break;
            
            case("Falcon"):
                itemBook.GetComponent<ItemBook>().foundCombinations[12]=true;
                effectManager.GetComponent<EffectManager>().NewitemBookFound();
                break;

            case("Magpie"):
                itemBook.GetComponent<ItemBook>().foundCombinations[0]=true;
                effectManager.GetComponent<EffectManager>().NewitemBookFound();
                break;
            
            case("Duck"):
                itemBook.GetComponent<ItemBook>().foundCombinations[2]=true;
                effectManager.GetComponent<EffectManager>().NewitemBookFound();
                break;

            case("Skunk"):
                itemBook.GetComponent<ItemBook>().foundCombinations[4]=true;
                effectManager.GetComponent<EffectManager>().NewitemBookFound();
                break;
            
            case("Turtle"):
                itemBook.GetComponent<ItemBook>().foundCombinations[10]=true;
                effectManager.GetComponent<EffectManager>().NewitemBookFound();
                break;

            case("Tiger"):
                itemBook.GetComponent<ItemBook>().foundCombinations[11]=true;
                effectManager.GetComponent<EffectManager>().NewitemBookFound();
                break;
            
            default:
                break;
        }
    }
}
