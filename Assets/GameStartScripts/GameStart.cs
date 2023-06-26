using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{
    public static bool newGame;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onStartClick(){
        newGame=true;
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
}
