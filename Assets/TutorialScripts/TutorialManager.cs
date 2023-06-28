using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//최적화 완료
public class TutorialManager : MonoBehaviour
{

    
    public static bool tutorialDone; //튜토리얼 완료됬는지 확인 변수
    private int tutorialLevel; // 현재 튜토리얼 단계 변수

    //마지막 단계에서 적 소멸 확인하는데 사용하는 변수
    private float lv1Timer;
    private int enemyCount,enemyDied;

    public GameObject[] cutScene;
    public GameObject cutSceneBackground;
    public GameObject cutSceneFront;
    [SerializeField] private GameObject settingWindow;
    [SerializeField] private GameObject panel;
    [SerializeField] private Text dialogue;
    [SerializeField] private GameObject tutorialAnimals;
    [SerializeField] private GameObject inventory;
    [SerializeField] private GameObject encyclopediaArrow;
    [SerializeField] private GameObject mainCharacter;
    [SerializeField] private GameObject mainCamera;
    [SerializeField] private GameObject encyclopedia;
    [SerializeField] private GameObject itemDummy;
    [SerializeField] private GameObject itemBook;
    [SerializeField] private GameObject itemBookArrow;
    [SerializeField] private GameObject cameraModeArrow;
    [SerializeField] private GameObject healthArrow;
    [SerializeField] private GameObject itemDummyArrow;
    [SerializeField] private GameObject timerArrow;
    [SerializeField] private GameObject yellowArrow;
    [SerializeField] private GameObject killsArrow;
    public GameObject item1;
    public GameObject fire;
    [SerializeField] GameObject[] humma1;
    [SerializeField] GameObject[] startPosition;
    
    void Start()
    {
        tutorialDone=false;
        tutorialLevel=-10;
        lv1Timer=15.0f;
        enemyCount=0;
        enemyDied=0;
    }

    void Update()
    {
        //튜토리얼 단계들
        if(!settingWindow.activeInHierarchy){
            if(tutorialLevel==-10){
                cutSceneBackground.SetActive(true);
                cutSceneFront.SetActive(true);
                inventory.SetActive(false);
                cutScene[0].SetActive(true);
                panel.SetActive(true);
                dialogue.text ="세상의 모든 숲에는 그 숲을 지키는 불이 있으며, 그 숲이 살아있다는 것은 그 불이 살아있다는것을 의미한다.";
                if(Input.GetKeyDown(KeyCode.Return)) tutorialLevel++;
            }
            else if(tutorialLevel==-9){
                cutScene[0].SetActive(false);
                cutScene[1].SetActive(true);
                dialogue.text ="그 불에서 동물들은 온기를 느끼며 보호와 보살핌을 받을 수 있다.";
                if(Input.GetKeyDown(KeyCode.Return)) tutorialLevel++;
            }
            else if(tutorialLevel==-8){
                cutScene[1].SetActive(false);
                cutScene[2].SetActive(true);
                dialogue.text ="하지만 불의 힘이 강해질수록 그 반대되는 그림자 세력들도 힘을 얻게 된다.";
                if(Input.GetKeyDown(KeyCode.Return)) tutorialLevel++;
            }
            else if(tutorialLevel==-7){
                cutScene[2].SetActive(false);
                cutScene[3].SetActive(true);
                dialogue.text ="그 상태에서 숲의 불을 지키는 불의 정령이 약해지거나, 사라지게 되면, 숲은 결국 그림자들에 의해 잠식당하고 만다.";
                if(Input.GetKeyDown(KeyCode.Return)) tutorialLevel++;
            }
            else if(tutorialLevel==-6){
                dialogue.text ="이 숲이 그러했다.";
                if(Input.GetKeyDown(KeyCode.Return)) tutorialLevel++;
            }
            else if(tutorialLevel==-5){
                cutScene[3].SetActive(false);
                cutScene[4].SetActive(true);
                dialogue.text ="동물 도감의 완성을 위해 숲을 탐험하다가 길을 잃은 우리의 주인공";
                if(Input.GetKeyDown(KeyCode.Return)) tutorialLevel++;
            }
            else if(tutorialLevel==-4){
                cutScene[4].SetActive(false);
                cutScene[5].SetActive(true);
                dialogue.text ="오랜 방황 끝에 불이 있는곳에 도달한다.";
                if(Input.GetKeyDown(KeyCode.Return)) tutorialLevel++;
            }
            else if(tutorialLevel==-3){
                cutScene[5].SetActive(false);
                cutScene[6].SetActive(true);
                dialogue.text ="불의 정령을 찾아오기 전까지 불을 지켜줄 사람이 필요햇던 숲은 우리의 주인공에게 불을 지켜줄 임무를 하사한다.";
                if(Input.GetKeyDown(KeyCode.Return)) tutorialLevel++;
            }
            else if(tutorialLevel==-2){
                dialogue.text ="그대신 도감을 완성할 수 있게 도와준다고, 그리고 한번도 보지 못한 동물들도 만나게 해주겠다고..";
                if(Input.GetKeyDown(KeyCode.Return)) tutorialLevel++;
            }
            else if(tutorialLevel==-1){
                cutScene[6].SetActive(false);
                cutScene[7].SetActive(true);
                dialogue.text ="그렇게 카메라에 불의 힘을 부여받은 우리의 주인공은 숲의 불이 다 회복될때까지 불을 지켜내야 하는 임무를 맡게 되는데...";
                if(Input.GetKeyDown(KeyCode.Return)) tutorialLevel++;
            }
            else if(tutorialLevel==0)
            {
                cutScene[7].SetActive(false);
                cutSceneBackground.SetActive(false);
                cutSceneFront.SetActive(false);
                dialogue.text = "숲에 오신 것을 환영해요, 모험가님.";
                if(Input.GetKeyDown(KeyCode.Return)) tutorialLevel++;
            }
            else if(tutorialLevel==1){
                dialogue.text ="숲을 지키기 위해 알아야 할 것들에 대해 알려드릴께요.";
                if(Input.GetKeyDown(KeyCode.Return)) tutorialLevel++;
            }
            else if(tutorialLevel==2){
                dialogue.text = "숲에는 많은 동물들이 살고 있어요. 이 친구들은 종종 불 근처로 와서 휴식을 즐기다 돌아가요.";
                if(Input.GetKeyDown(KeyCode.Return)) tutorialLevel++;
            }
            else if(tutorialLevel==3){
                inventory.SetActive(true);
                panel.SetActive(false);
                tutorialAnimals.SetActive(true);
                tutorialAnimals.GetComponent<Animals>().currentState=true;
                if(tutorialAnimals.GetComponent<AnimalMovement>().isSitting) tutorialLevel++;
            }
            else if(tutorialLevel==4){
                panel.SetActive(true);
                dialogue.text="동물 근처에 가서 스페이스바를 누르면 동물 사진을 찍을 수 있어요. 사진이 찍혔을 경우 우측 아래 빨간색 동물 도감에 표시돼요.";
                if(Input.GetKeyDown(KeyCode.Return)) tutorialLevel++;
            }
            else if(tutorialLevel==5){
                dialogue.text="저희가 영혼을 담아준 카메라 덕분에 촬영한 동물의 사진 뿐만 아니라 동물에 대한 다양한 정보들도 담기게 돼요.";
                if(Input.GetKeyDown(KeyCode.Return)) tutorialLevel++;
            }
            else if(tutorialLevel==6){
                dialogue.text="지금 불 앞에서 쉬고 있는 곰의 사진을 찍어보세요!";
                if(Input.GetKeyDown(KeyCode.Return)) tutorialLevel++;
            }
            else if(tutorialLevel==7){
                panel.SetActive(false);
                mainCharacter.SetActive(true);
                mainCamera.GetComponent<CameraManager>().enabled =true;
                if(tutorialAnimals.GetComponent<Animals>().typeAppeared[0]) tutorialLevel++;
            }
            else if(tutorialLevel==8){
                panel.SetActive(true);
                dialogue.text="곰의 사진을 찍으셨네요! 오른쪽 아래의 빨간 책 모양을 클릭해보시면 찍으신 동물의 사진과 정보가 담겨 있을꺼에요.";
                if(Input.GetKeyDown(KeyCode.Return)) tutorialLevel++;
            }
            else if(tutorialLevel==9){
                panel.SetActive(false);
                encyclopediaArrow.SetActive(true);
                if(encyclopedia.activeInHierarchy) tutorialLevel++;
            }
            else if(tutorialLevel==10){
                encyclopediaArrow.SetActive(false);
                panel.SetActive(true);
                dialogue.text="곰의 사진과 정보가 저장된게 보이시나요? 좌우 버튼을 통해 찍으신 다양한 동물들을 확인할 수 있어요. 우측 위의 x 버튼을 눌러서 도감에서 나가주세요.";
                if(Input.GetKeyDown(KeyCode.Return)) tutorialLevel++;
            }
            else if(tutorialLevel==11){
                panel.SetActive(false);
                if(!encyclopedia.activeInHierarchy) tutorialLevel++;
            }
            else if(tutorialLevel==12){
                panel.SetActive(true);
                dialogue.text="많은 동물들이 불 근처에서 휴식을 취하지만 불의 생명력이 무한한 것은 아니에요.";
                if(Input.GetKeyDown(KeyCode.Return)) tutorialLevel++;
            }
            else if(tutorialLevel==13){
                healthArrow.SetActive(true);
                dialogue.text="좌측 위를 보시면 불 모양이 점차 줄어드는 것이 보일 꺼에요. 저 표시가 불의 생명력을 의미해요.";
                if(Input.GetKeyDown(KeyCode.Return)) tutorialLevel++;
            }
            else if(tutorialLevel==14){
                healthArrow.SetActive(false);
                dialogue.text="지금부터 불의 생명력을 유지할 수 있는 방법을 알려드릴께요.";
                if(Input.GetKeyDown(KeyCode.Return)) tutorialLevel++;
            }
            else if(tutorialLevel==15){
                dialogue.text="우선 동물들은 불에서 돌아가는 길에 다양한 아이템들을 떨어뜨려요. 이번엔 처음 오신거니까 제가 선물을 드릴께요.";
                if(Input.GetKeyDown(KeyCode.Return)) tutorialLevel++;
            }
            else if(tutorialLevel==16){
                dialogue.text="표시된 아이템 꾸러미 위로 가면 아이템을 획득할 수 있어요.";
                if(Input.GetKeyDown(KeyCode.Return)) tutorialLevel++;
            }
            else if(tutorialLevel==17){
                itemDummyArrow.SetActive(true);
                panel.SetActive(false);
                itemDummy.SetActive(true);
                if(!item1.activeInHierarchy) tutorialLevel++;
            }
            else if(tutorialLevel==18){
                panel.SetActive(true);
                itemDummy.SetActive(false);
                itemDummyArrow.SetActive(false);
                dialogue.text="숲의 불은 다양한 아이템들을 조합하여 상호작용을 할 수 있어요. 그럼 어떤 상호작용들이 가능할까요?";
                if(Input.GetKeyDown(KeyCode.Return)) tutorialLevel++;
            }
            else if(tutorialLevel==19){
                dialogue.text="오른쪽 아래 두루마리에는 불에서 조합할 수 있는 조합들이 나와있어요. 아이템 조합 두루마리를 클릭해보세요.";
                if(Input.GetKeyDown(KeyCode.Return)) tutorialLevel++;
            }
            else if(tutorialLevel==20){
                panel.SetActive(false);
                itemBookArrow.SetActive(true);
                if(itemBook.activeInHierarchy) tutorialLevel++;
            }
            else if(tutorialLevel==21){
                panel.SetActive(true);
                itemBookArrow.SetActive(false);
                dialogue.text="왼쪽 슬롯들에는 필요한 아이템들과 오른쪽에는 아이템들을 넣었을 때 발생하는 효과가 나와있어요.";
                if(Input.GetKeyDown(KeyCode.Return)) tutorialLevel++;
            }
            else if(tutorialLevel==22){
                dialogue.text="현재 사용할 수 있는 아이템 조합들은 제한되어 있지만 불의 영혼과 결탁한 동물들을 발견하면 사용할 수가 있어요! 그렇니까 영혼의 동물을 발견하면 꼭 사진을 찍으셔야해요!";
                if(Input.GetKeyDown(KeyCode.Return)) tutorialLevel++;
            }
            else if(tutorialLevel==23){
                dialogue.text="두루마리의 오른쪽 위를 보시면 3개의 표시가 나오는데 각각 공격 방식, 불 관련 상호작용, 동물 영혼과의 계약에 대한 조합을 확인할 수 있어요.";
                if(Input.GetKeyDown(KeyCode.Return)) tutorialLevel++;
            }
            else if(tutorialLevel==24){
                dialogue.text="중간 노락색을 눌러 불과의 상호작용에 관련된 조합들을 확인해보세요.";
                if(Input.GetKeyDown(KeyCode.Return)) tutorialLevel++;
            }
            else if(tutorialLevel==25){
                panel.SetActive(false);
                yellowArrow.SetActive(true);
                if(itemBook.GetComponent<ItemBook>().itemType==1) tutorialLevel++;
            }
            else if(tutorialLevel==26){
                panel.SetActive(true);
                yellowArrow.SetActive(false);
                dialogue.text="제일 위를 보시면 불의 체력을 회복하기 위한 조합이 보이실 꺼에요. 해당 조합을 기억하시고 우측 위의 x 버튼을 눌러 아이템 조합 두루마리를 닫아주세요.";
                if(Input.GetKeyDown(KeyCode.Return)) tutorialLevel++;
            }
            else if(tutorialLevel==27){
                panel.SetActive(false);
                if(!itemBook.activeInHierarchy) tutorialLevel++;
            }
            else if(tutorialLevel==28){
                panel.SetActive(true);
                dialogue.text="이제 불의 체력을 회복시켜볼께요.아래 인벤토리의 아이템들을 드래그하여 불에 조합에 따라 넣어주면 생명력이 회복된답니다!";
                if(Input.GetKeyDown(KeyCode.Return)) tutorialLevel++;
            }
            else if(tutorialLevel==29){
                timerArrow.SetActive(true);
                dialogue.text="아이템을 넣기 시작하면 왼쪽 위의 타이머가 시작되는데 아이템을 3개를 넣거나 시간이 다되면 넣고 있던 조합이 초기화되요!";
                if(Input.GetKeyDown(KeyCode.Return)) tutorialLevel++;
            }
            else if(tutorialLevel==30){
                timerArrow.SetActive(false);
                dialogue.text="아이템을 넣는 순서도 조합에 영향을 미친다는 것과 아이템을 넣으면 조합에 실패해도 소모된다는 점에 주의해 주세요!";
                if(Input.GetKeyDown(KeyCode.Return)) tutorialLevel++;
            }
            else if(tutorialLevel==31){
                dialogue.text="이제 아이템을 넣어 불의 생명력을 회복시켜주세요.";
                if(Input.GetKeyDown(KeyCode.Return)) tutorialLevel++;
            }
            else if(tutorialLevel==32){
                panel.SetActive(false);
                if(fire.GetComponent<FireManager>().result=="addHealth") tutorialLevel++;
            }
            else if(tutorialLevel==33){
                panel.SetActive(true);
                dialogue.text="불의 생명력이 증가하였습니다! 불의 체력은 시간에 따라 점점 떨어지니 잘 확인하고 장작을 넣어주세요!";
                if(Input.GetKeyDown(KeyCode.Return)) tutorialLevel++;
            }
            else if(tutorialLevel==34){
                dialogue.text="불의 생명력을 위협하는 다른 요소는 그림자의 공격이에요.";
                if(Input.GetKeyDown(KeyCode.Return)) tutorialLevel++;
            }
            else if(tutorialLevel==35){
                dialogue.text="저희가 영혼을 부여해드린 카메라를 통해 그림자들을 제거할 수 있어요.";
                if(Input.GetKeyDown(KeyCode.Return)) tutorialLevel++;
            }
            else if(tutorialLevel==36){
                dialogue.text="e 키를 누르면 우른쪽에 표시된 카메라의 모습이 붉게 변할꺼에요. 노란색은 동물들을 찍는 모드, 붉은색은 그림자를 공격하는 모드에요. 한번 바꿔보세요.";
                if(Input.GetKeyDown(KeyCode.Return)) tutorialLevel++;
            }
            else if(tutorialLevel==37){
                cameraModeArrow.SetActive(true);
                panel.SetActive(false);
                if(Input.GetKeyDown(KeyCode.E)) tutorialLevel++;
            }
            else if(tutorialLevel==38){
                cameraModeArrow.SetActive(false);
                panel.SetActive(true);
                dialogue.text="스페이스바로 그림자를 공격할 수 있어요. 그림자가 불에 닿으면 불의 생명력이 떨어지니 조심하세요!";
                if(Input.GetKeyDown(KeyCode.Return)) tutorialLevel++;
            }
            else if(tutorialLevel==39){
                dialogue.text="어 저기 그림자들이 오네요! 그림자들을 물리쳐주세요!";
                if(Input.GetKeyDown(KeyCode.Return)) tutorialLevel++;
            }
            else if(tutorialLevel==40){
                panel.SetActive(false);
                lv1Timer+=Time.deltaTime;
                if(lv1Timer>10.0f&&enemyCount<3){
                    humma1[enemyCount].SetActive(true);
                    enemyCount++;
                    lv1Timer=0;
                }
                if(humma1[2]==null&&enemyCount>=3) tutorialLevel++;
            }
            else if(tutorialLevel==41){
                panel.SetActive(true);
                killsArrow.SetActive(true);
                dialogue.text="좌측 위의 작은 그림자 표시는 지금까지 제거한 그림자 수를 나타내요. 제거한 그림자가 많아질수록 강한 그림자가 나오기 시작하니까 조심하세요!";
                if(Input.GetKeyDown(KeyCode.Return)) tutorialLevel++;
            }
            else if(tutorialLevel==42){
                killsArrow.SetActive(false);
                dialogue.text="이제 숲을 지킬 자격이 되시는 것 같군요! 불의 영혼이 되살아날때까지 숲을 잘 부탁드릴꼐요!";
                if(Input.GetKeyDown(KeyCode.Return)) tutorialLevel++;
            }
            else if(tutorialLevel==43){
                tutorialDone=true;
                SceneManager.LoadScene("Loading");
            }
        }
        

    
    }
}
