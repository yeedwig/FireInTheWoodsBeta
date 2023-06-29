using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndingManager : MonoBehaviour
{
    public GameObject[] cutScene;
    public GameObject cutSceneBackground;
    public GameObject cutSceneFront;
    [SerializeField] private GameObject panel;
    [SerializeField] private Text dialogue;

    private int endingLevel;

    void Start()
    {
        endingLevel=0;
    }

    
    void Update()
    {
        if(endingLevel==0){
            cutSceneBackground.SetActive(true);
            cutSceneFront.SetActive(true);
            cutScene[0].SetActive(true);
            panel.SetActive(true);
            dialogue.text ="그림자왕은 자신의 길을 가로막는자를 없애기 위해 나타났고";
            if(Input.GetKeyDown(KeyCode.Return)) endingLevel++;
        }
        else if(endingLevel==1){
            cutScene[0].SetActive(false);
            cutScene[1].SetActive(true);
            dialogue.text ="우리의 수호자는 모든 정령동물들 찾아내어 그힘을 이용해 그림자 왕을 물리쳤다.";
            if(Input.GetKeyDown(KeyCode.Return)) endingLevel++;
        }
        else if(endingLevel==2){
            cutScene[1].SetActive(false);
            cutScene[2].SetActive(true);
            dialogue.text ="그러나 끝까지 남은 힘을 쥐어짜낸 마왕은 자신이 가진 모든 힘을 사용하여 마지막 공격을 퍼붓는다.";
            if(Input.GetKeyDown(KeyCode.Return)) endingLevel++;
        }
        else if(endingLevel==3){
            cutScene[2].SetActive(false);
            cutScene[3].SetActive(true);
            dialogue.text ="하지만 주인공이 끝까지 지켜낸 불은 힘을 다시 회복하여";
            if(Input.GetKeyDown(KeyCode.Return)) endingLevel++;
        }
        else if(endingLevel==4){
            cutScene[3].SetActive(false);
            cutScene[4].SetActive(true);
            dialogue.text ="그림자왕을 다시 그림자로 돌려보내는데 성공한다.";
            if(Input.GetKeyDown(KeyCode.Return)) endingLevel++;
        }
        else if(endingLevel==5){
            cutScene[4].SetActive(false);
            cutScene[5].SetActive(true);
            dialogue.text ="불을 지켜준 우리의 수호자를 위해 불은 평생 어둡지 않고 사라지지 않을 따듯한 불꽃을 선물해준다.";
            if(Input.GetKeyDown(KeyCode.Return)) endingLevel++;
        }
        else if(endingLevel==6){
            cutScene[5].SetActive(false);
            cutScene[6].SetActive(true);
            dialogue.text ="주인공은 그동안 찍은 사진을 모두 모아서 도감을 완성해낸다.";
            if(Input.GetKeyDown(KeyCode.Return)) endingLevel++;
        }
        else if(endingLevel==7){
            dialogue.text ="도감은 많은 사람들에게 인기를 얻어 전세계적으로 팔리게 된다.";
            if(Input.GetKeyDown(KeyCode.Return)) endingLevel++;
        }
        else if(endingLevel==8){
            cutScene[6].SetActive(false);
            cutScene[7].SetActive(true);
            dialogue.text ="그리고 주인공 옆에는 언제나 꺼지지 않는 불꽃이 자리하고 있다.";
            if(Input.GetKeyDown(KeyCode.Return)) endingLevel++;
        }
        else if(endingLevel==9){
            cutScene[7].SetActive(false);
            cutScene[8].SetActive(true);
            cutSceneBackground.SetActive(false);
            cutSceneFront.SetActive(false);
            panel.SetActive(false);
            if(Input.GetKeyDown(KeyCode.Return)) endingLevel++;
        }
        else if(endingLevel==10)
        {
    #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
    #else
            Application.Quit();
    #endif
        }
    }
}
