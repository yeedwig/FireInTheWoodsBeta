using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingWindow : MonoBehaviour
{
    [SerializeField] private GameObject inventory;
    [SerializeField] private GameObject settingWindow;
    [SerializeField] private GameObject soundSetting;
    
    public Slider BG;
    public Slider SFX;

    [SerializeField] private Text BGp;
    [SerializeField] private Text SFXp;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        BGp.text=(BG.value*0.5*100).ToString("F1")+"%";
        SFXp.text=(SFX.value*0.5*100).ToString("F1")+"%";
    }

    public void onClickContinue(){
        GameObject.Find("GameManager").GetComponent<GameManager>().pause=false;
        Time.timeScale=1;
        if(inventory!=null){
            inventory.SetActive(true);
        }
        
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
