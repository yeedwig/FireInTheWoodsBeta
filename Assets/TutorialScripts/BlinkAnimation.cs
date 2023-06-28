using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlinkAnimation : MonoBehaviour
{
    private float time; //깜빡이는 시간 조절을 위한 변수

    //투명도 변환을 위한 변수들
    public SpriteRenderer renderer;
    public Image image;
    public ColorBlock colorBlock;
    
    void Start() {
        renderer=GetComponent<SpriteRenderer>();
        image=GetComponent<Image>();
    }

    void Update()
    {
        //UI일 경우 아래와 같이 변환
        if(renderer==null){
            if(time<0.35f)
            {
                image.color= new Color(image.color.r,image.color.g,image.color.b,1-2*time);
            }
            else
            {
                image.color= new Color(image.color.r,image.color.g,image.color.b,2*time-0.4f);
                if(time>0.7f){
                    time=0;
                }
            }
        }
        //객체일 경우 아래와 같이 변환
        else{
            if(time<0.35f)
            {
                renderer.color = new Color(1,1,1,1-2*time);
            }
            else
            {
                renderer.color = new Color(1,1,1,2*time-0.4f);
                if(time>0.7f){
                    time=0;
                }
            }
        }
        time+=Time.deltaTime;
    }
    
}
