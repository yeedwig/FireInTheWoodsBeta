using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlinkAnimation : MonoBehaviour
{
    private float time;
    public SpriteRenderer renderer;
    public Image image;
    public ColorBlock colorBlock;
    
    void Start() {
        renderer=GetComponent<SpriteRenderer>();
        image=GetComponent<Image>();
        
    }

    void Update()
    {
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
