using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//최적화 최종으로 완료
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
        if(Input.GetKeyDown(KeyCode.Escape)) checkPause();
    }

    public void onStartClick(){
        SceneManager.LoadScene("Loading");
    }

    
    public void onLoadClick(){
        newGame=false;
        SceneManager.LoadScene("Loading");
    }

    public void onExitClick(){
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void onClickContinue(){
        pause=false;
        Time.timeScale=1;
        settingWindow.SetActive(false);
    }

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
