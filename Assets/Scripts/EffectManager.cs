using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//최적화 완료
public class EffectManager : MonoBehaviour
{
    //깜빡일 객체들
    public GameObject encyclopediaBlink;
    public GameObject itemBookBlink;

    //여러번 코루틴이 돌지 않도록 하는 변수
    private bool encyclopediaBlinkOn;
    private bool itemBookBlinkOn;
    
    void Start(){
        encyclopediaBlinkOn=false;
        itemBookBlinkOn=false;
    }

    //동물 도감 코루틴 시작
    public void NewEncyclopediaFound(){
        StartCoroutine(blinkEncyclopedia(encyclopediaBlink));
    }

    //아이템 북 코루틴 시작
    public void NewitemBookFound(){
        StartCoroutine(blinkItemBook(itemBookBlink));
    }

    //동물 도감 코루틴
    IEnumerator blinkEncyclopedia(GameObject obj){
        if(!encyclopediaBlinkOn){
            encyclopediaBlinkOn=true;
            obj.SetActive(true);
            yield return new WaitForSeconds(1.0f);
            obj.SetActive(false);
            encyclopediaBlinkOn=false;
        }
    }

    //아이템 도감 코루틴
    IEnumerator blinkItemBook(GameObject obj){
        if(!itemBookBlinkOn){
            itemBookBlinkOn=true;
            obj.SetActive(true);
            yield return new WaitForSeconds(3.0f);
            obj.SetActive(false);
            itemBookBlinkOn=false;
        }
    }
}
