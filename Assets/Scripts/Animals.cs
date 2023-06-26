using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//최적화 완료
public class Animals :MonoBehaviour
{
    public string name; //객체 이름
    public bool[] typeAppeared = new bool[3]{false,false,false};//3가지 종류들 등장 했는지 확인
    public bool currentState = false; //현재 사용 중인지
    public int arrivingPosition; //도착 지점
    public int appearPercent = 100; //등장 확률
    public int currentType=0;
}
