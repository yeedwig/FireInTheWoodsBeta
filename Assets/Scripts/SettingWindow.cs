using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingWindow : MonoBehaviour
{
    [SerializeField] private GameObject inventory;
    [SerializeField] private GameObject settingWindow;
    [SerializeField] private GameObject soundSetting;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onClickContinue(){
        GameObject.Find("GameManager").GetComponent<GameManager>().pause=false;
        Time.timeScale=1;
        inventory.SetActive(true);
        settingWindow.SetActive(false);
    }
    public void onClickSetting(){
        soundSetting.SetActive(true);
    }
    public void soundSettingClose()
    {
        soundSetting.SetActive(false);
    }
    public void onClickExit(){
                //일단 에디터 종료
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); // 어플리케이션 종료
#endif
    }
}
