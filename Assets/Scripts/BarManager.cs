using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//최적화 완료
public class BarManager : MonoBehaviour
{

    [SerializeField] private Image hpBar;
    [SerializeField] private Image timerBar;

    [SerializeField] private GameObject gameManager;
    [SerializeField] private GameObject fire;

    
    void Update()
    {
        Player_HP();
        Item_Timer();
    }

    public void Player_HP(){
        hpBar.fillAmount = gameManager.GetComponent<GameManager>().health/gameManager.GetComponent<GameManager>().maxHealth;
    }

    public void Item_Timer(){
        timerBar.fillAmount = fire.GetComponent<FireManager>().currTime/30.0f;
    }
}
