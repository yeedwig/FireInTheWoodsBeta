using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{
    public static bool newGame;
    public GameObject settingWindow;
    public GameObject soundSetting;
    private bool pause = false;

    // Start is called before the first frame update
    void Start()
    {
        newGame=true;
    }

    // Update is called once per frame
    void Update()
    {
        checkPause();
    }

    public void onStartClick(){
        SceneManager.LoadScene("Loading");
    }

    
    public void onLoadClick(){
        newGame=false;
        SceneManager.LoadScene("Loading");
    }

    
    public void onExitClick(){
        //일단 에디터 종료
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); // 어플리케이션 종료
#endif
    }

    public void onClickContinue(){
        pause=false;
        Time.timeScale=1;
        settingWindow.SetActive(false);
    }

    public void checkPause(){
        if(Input.GetKeyDown(KeyCode.Escape)){
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
}
