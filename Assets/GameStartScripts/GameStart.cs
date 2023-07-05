using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

//최적화 최종으로 완료
public class GameStart : MonoBehaviour
{
    public static bool newGame; //새 게임 확인 변수
    private bool pause; //정지 상태 확인 변수

    //사용 객체
    public GameObject settingWindow;
    public GameObject soundSetting;
    
    void Start()
    {
        newGame=true;
        pause = false;
        TutorialManager.tutorialDone=false;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) checkPause();
    }

    //새로운 게임 시작
    public void onStartClick(){
        SceneManager.LoadScene("Loading");
    }

    //하던 게임 시작
    public void onLoadClick(){
        
        string path = Application.persistentDataPath+"GameManager"+".json";
        FileInfo fi = new FileInfo(path);
        if(!fi.Exists){
        }
        else{
            newGame=false;
            SceneManager.LoadScene("Loading");
        }
        
    }

    //게임 종료
    public void onExitClick(){
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    //이어하기 버튼 함수
    public void onClickContinue(){
        pause=false;
        Time.timeScale=1;
        settingWindow.SetActive(false);
    }

    //esc 창을 사용 중인지 확인하는 함수
    public void checkPause(){
        if(!pause)
        {
            pause=true;
            Time.timeScale=0;
            settingWindow.SetActive(true);
            
        }
        else
        {
            pause=false;
            Time.timeScale=1;
            soundSetting.SetActive(false);
            settingWindow.SetActive(false);
            
        }
    }
}
