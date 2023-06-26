using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{

    public GameObject[] cutScene;
    public GameObject cutSceneBackground;
    public GameObject cutSceneFront;

    private int tutorialLevel;
    [SerializeField] private GameObject panel;
    [SerializeField] private Text dialogue;
    [SerializeField] private GameObject[] tutorialAnimals;
    [SerializeField] private GameObject inventory;
    [SerializeField] private GameObject encyclopediaArrow;
    [SerializeField] private GameObject mainCharacter;
    [SerializeField] private GameObject mainCamera;
    [SerializeField] private GameObject encyclopedia;
    [SerializeField] private GameObject itemDummy;
    [SerializeField] private GameObject itemBook;
    [SerializeField] private GameObject itemBookArrow;


    public GameObject item1;
    public GameObject fire;

    [SerializeField] GameObject[] humma1;
    [SerializeField] GameObject[] startPosition;
    private float lv1Timer=15.0f;
    private int enemyCount=0,enemyDied=0;

    // Start is called before the first frame update
    void Start()
    {
        tutorialLevel=-8;
    }

    // Update is called once per frame
    void Update()
    {
        
        if(tutorialLevel==-8){
            inventory.SetActive(false);
            cutScene[0].SetActive(true);
            panel.SetActive(true);
            if(Input.GetKeyDown(KeyCode.Return)) tutorialLevel++;
        }
        else if(tutorialLevel==-7){
            cutScene[0].SetActive(false);
            cutScene[1].SetActive(true);
            if(Input.GetKeyDown(KeyCode.Return)) tutorialLevel++;
        }
        else if(tutorialLevel==-6){
            cutScene[1].SetActive(false);
            cutScene[2].SetActive(true);
            if(Input.GetKeyDown(KeyCode.Return)) tutorialLevel++;
        }
        else if(tutorialLevel==-5){
            cutScene[2].SetActive(false);
            cutScene[3].SetActive(true);
            if(Input.GetKeyDown(KeyCode.Return)) tutorialLevel++;
        }
        else if(tutorialLevel==-4){
            cutScene[3].SetActive(false);
            cutScene[4].SetActive(true);
            if(Input.GetKeyDown(KeyCode.Return)) tutorialLevel++;
        }
        else if(tutorialLevel==-3){
            cutScene[4].SetActive(false);
            cutScene[5].SetActive(true);
            if(Input.GetKeyDown(KeyCode.Return)) tutorialLevel++;
        }
        else if(tutorialLevel==-2){
            cutScene[5].SetActive(false);
            cutScene[6].SetActive(true);
            if(Input.GetKeyDown(KeyCode.Return)) tutorialLevel++;
        }
        else if(tutorialLevel==-1){
            cutScene[6].SetActive(false);
            cutScene[7].SetActive(true);
            if(Input.GetKeyDown(KeyCode.Return)) tutorialLevel++;
        }
        else if(tutorialLevel==0)
        {
            cutScene[7].SetActive(false);
            cutSceneBackground.SetActive(false);
            cutSceneFront.SetActive(false);
            dialogue.text = "숲의 세계에 오신 것은 환영합니다";
            if(Input.GetKeyDown(KeyCode.Return)) tutorialLevel++;
        }
        else if(tutorialLevel==1){
            dialogue.text ="당신은 숲은 도와주실 것이라 믿고 저희 숲에 대해 알려드리겠습니다";
            if(Input.GetKeyDown(KeyCode.Return)) tutorialLevel++;
        }
        else if(tutorialLevel==2){
            dialogue.text = "저희 숲에서는 많은 동물들이 살고 있습니다. 이 친구들은 종종 불 앞에서 쉬고 가곤 하죠";
            if(Input.GetKeyDown(KeyCode.Return)) tutorialLevel++;
        }
        else if(tutorialLevel==3){
            inventory.SetActive(true);
            panel.SetActive(false);
            
            tutorialAnimals[0].SetActive(true);
            tutorialAnimals[0].GetComponent<Animals>().currentState=true;
            if(tutorialAnimals[0].GetComponent<AnimalMovement>().isSitting) tutorialLevel++;
        }
        else if(tutorialLevel==4){
            panel.SetActive(true);
            dialogue.text="(중간 과정 생략) 동물들을 모을 수 있는 도감을 드리겠습니다. 동물 가까이 가서 엔터(나중에 수정)를 누르시면 사진을 찍으실 수 있으십니다";
            if(Input.GetKeyDown(KeyCode.Return)) tutorialLevel++;
        }
        else if(tutorialLevel==5){
            panel.SetActive(false);
        
            mainCharacter.SetActive(true);
            mainCamera.GetComponent<CameraManager>().enabled =true;
            if(tutorialAnimals[0].GetComponent<Animals>().typeAppeared[0]) tutorialLevel++;
        }
        else if(tutorialLevel==6){
            panel.SetActive(true);
            dialogue.text="곰의 사진을 찍으셨군요. 우측 아래의 책 모양을 클릭해보시면 도감에 찍으신 동물의 사진이 들어 있을 껍니다.";
            if(Input.GetKeyDown(KeyCode.Return)) tutorialLevel++;
        }
        else if(tutorialLevel==7){
            dialogue.text="저희가 카메라에 영혼을 담아드려 동물의 사진을 찍으시면 동물이 가진 특성이나 비밀들을 도감에 담아드린답니다";
            if(Input.GetKeyDown(KeyCode.Return)) tutorialLevel++;
        }
        else if(tutorialLevel==8){
            panel.SetActive(false);
            
            encyclopediaArrow.SetActive(true);
            if(encyclopedia.activeInHierarchy) tutorialLevel++;
        }
        else if(tutorialLevel==9){
            encyclopediaArrow.SetActive(false);
            panel.SetActive(true);
            dialogue.text="위와 같이 저장되며 좌우 버튼을 통해 다양한 동물들을 확인할 수 있습니다. 우측 위의 x를 눌러 도감에서 나가시오";
            if(Input.GetKeyDown(KeyCode.Return)) tutorialLevel++;
        }
        else if(tutorialLevel==10){
            panel.SetActive(false);
            if(!encyclopedia.activeInHierarchy) tutorialLevel++;
        }
        else if(tutorialLevel==11){
            panel.SetActive(true);
            dialogue.text="숲의 동물들은 불을 왕래하며 많은 아이템들을 떨어뜨립니다. 표시된 아이템 더미를 먹어주세요";
            if(Input.GetKeyDown(KeyCode.Return)) tutorialLevel++;
        }
        else if(tutorialLevel==12){
            panel.SetActive(false);
            itemDummy.SetActive(true);
            if(!item1.activeInHierarchy) tutorialLevel++;
        }
        else if(tutorialLevel==13){
            panel.SetActive(true);
            itemDummy.SetActive(false);
            dialogue.text="위의 표시는 체력, 아이템 타이머, 죽인 적의 수를 나타냅니다";
            if(Input.GetKeyDown(KeyCode.Return)) tutorialLevel++;
        }
        else if(tutorialLevel==14){
            dialogue.text="체력이 다 떨어질 시 불의 생명력이 다되어 더 이상 숲을 지킬 수 없습니다";
            if(Input.GetKeyDown(KeyCode.Return)) tutorialLevel++;
        }
        else if(tutorialLevel==15){
            dialogue.text="불의 생명력을 살리기 위해서는 불에 조합을 해야되는데 어떻게 할까요? ";
            if(Input.GetKeyDown(KeyCode.Return)) tutorialLevel++;
        }
        else if(tutorialLevel==16){
            dialogue.text="아이템들의 조합은 아래 아이템 조합창에서 확인할 수 있어요. 아이템 조합창을 클릭해보세요";
            if(Input.GetKeyDown(KeyCode.Return)) tutorialLevel++;
        }
        else if(tutorialLevel==17){
            panel.SetActive(false);
            itemBookArrow.SetActive(true);
            if(itemBook.activeInHierarchy) tutorialLevel++;
        }
        else if(tutorialLevel==18){
            panel.SetActive(true);
            itemBookArrow.SetActive(false);
            dialogue.text="위의 버튼들은 아이템 조합들의 속성을 나타냅니다. 불은 어쩌고 이건 저쩌고";
            if(Input.GetKeyDown(KeyCode.Return)) tutorialLevel++;
        }
        else if(tutorialLevel==19){
            dialogue.text="현재 사용할 수 있는 아이템 조합들은 제한되어 있으며 불의 영혼과 결탁한 동물들을 발견하면 해금할 수 있게 됩니다";
            if(Input.GetKeyDown(KeyCode.Return)) tutorialLevel++;
        }
        else if(tutorialLevel==20){
            dialogue.text="중간의 불 관련 조합 버튼을 확인하여 불 체력을 증가시키는 조합을 확인한 후 아이템 조합창을 종료하세요";
            if(Input.GetKeyDown(KeyCode.Return)) tutorialLevel++;
        }
        else if(tutorialLevel==21){
            panel.SetActive(false);
            if(!itemBook.activeInHierarchy) tutorialLevel++;
        }
        else if(tutorialLevel==22){
            panel.SetActive(true);
            dialogue.text="아이템을 조합하는 법은 아래 인벤토리에서 해당 아이템들을 불에 드래그해서 넣어서 사용할 수 있습니다";
            if(Input.GetKeyDown(KeyCode.Return)) tutorialLevel++;
        }
        else if(tutorialLevel==23){
            dialogue.text="아이템은 3개씩 순서를 고려하여 조합하며 아이템 3개를 넣거나 위의 타이머가 초과될 시  자동 종료되게 됩니다.";
            if(Input.GetKeyDown(KeyCode.Return)) tutorialLevel++;
        }
        else if(tutorialLevel==24){
            dialogue.text="방금 확인한 불 체력 증가 조합을 확인하여 불 체력을 증가시켜주세요. 아이템 조합에 실패하면 해당 아이템은 소멸되니 주의해 주세요!";
            if(Input.GetKeyDown(KeyCode.Return)) tutorialLevel++;
        }
        else if(tutorialLevel==25){
            panel.SetActive(false);
            if(fire.GetComponent<FireManager>().result=="addHealth") tutorialLevel++;
        }
        else if(tutorialLevel==26){
            panel.SetActive(true);
            dialogue.text="불의 생명력이 증가하였습니다! 평상시에 불의 체력이 점차 떨어지니 잘 확인하고 장작을 넣어주세요!";
            if(Input.GetKeyDown(KeyCode.Return)) tutorialLevel++;
        }
        else if(tutorialLevel==27){
            dialogue.text="불의 생명력은 곧 그림자를 만들어내고 이 그림자는 불을 갉아먹으러 와요";
            if(Input.GetKeyDown(KeyCode.Return)) tutorialLevel++;
        }
        else if(tutorialLevel==28){
            dialogue.text="우리가 준 영혼의 카메라를 통해 공격을 할 수 있어요";
            if(Input.GetKeyDown(KeyCode.Return)) tutorialLevel++;
        }
        else if(tutorialLevel==29){
            dialogue.text="e 키를 누르면 우측에 표시된 카메라의 모습이 붉게 변할꺼에요 그러면 그림자들을 해치울 수 있어요";
            if(Input.GetKeyDown(KeyCode.Return)) tutorialLevel++;
        }
        else if(tutorialLevel==30){
            panel.SetActive(false);
            if(Input.GetKeyDown(KeyCode.E)) tutorialLevel++;
        }
        else if(tutorialLevel==31){
            panel.SetActive(true);
            dialogue.text="스페이스바로 공격하며 그림자가 불에 닿으면 체력이 떨어져요!";
            if(Input.GetKeyDown(KeyCode.Return)) tutorialLevel++;
        }
        else if(tutorialLevel==32){
            panel.SetActive(true);
            dialogue.text="숲을 지키다보면 다양한 동물들과의 계약, 특수 능력들 및 공격들을 얻을 수 있으니 이로치 동물들을 보면 사진 찍는걸 절대 까먹지 마세요!";
            if(Input.GetKeyDown(KeyCode.Return)) tutorialLevel++;
        }
        else if(tutorialLevel==33){
            dialogue.text="어 저기 적이 와요. 적들을 물리쳐주세요!";
            if(Input.GetKeyDown(KeyCode.Return)) tutorialLevel++;
        }
        else if(tutorialLevel==34){
            panel.SetActive(false);
            lv1Timer+=Time.deltaTime;
            if(lv1Timer>10.0f&&enemyCount<3){
                humma1[enemyCount].SetActive(true);
                enemyCount++;
                lv1Timer=0;
            }
            if(humma1[2]==null&&enemyCount>=3) tutorialLevel++;
            
        }
        else if(tutorialLevel==35){
            panel.SetActive(true);
            dialogue.text="숲을 지킬 자격이 되시는 것 같군요! 부탁드립니다";
            if(Input.GetKeyDown(KeyCode.Return)) tutorialLevel++;
        }
        else if(tutorialLevel==36){
            SceneManager.LoadScene("Loading");
        }

    
    }
}
