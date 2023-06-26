using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private GameObject enemy;
    [SerializeField] GameObject humma1;
    [SerializeField] GameObject marv;
    [SerializeField] GameObject harry;
    
    [SerializeField] GameObject[] startPosition;
    private float lv1Timer=0,lv2Timer=0,lv3Timer=0;
    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        int level = GameObject.Find("GameManager").GetComponent<GameManager>().level;
        //Debug.Log(level);
        lv1Timer+=Time.deltaTime;
        lv2Timer+=Time.deltaTime;
        if(level==1){
            if(lv1Timer>15.0f){
                enemy=Instantiate(humma1,startPosition[Random.Range(0,6)].transform.position,Quaternion.identity);
                lv1Timer=0;
            } 
        }
        else if(level==2){
            if(lv1Timer>12.0f){
                enemy=Instantiate(humma1,startPosition[Random.Range(0,6)].transform.position,Quaternion.identity);
                lv1Timer=0;
            } 
            if(lv2Timer>20.0f){
                int randomNum = Random.Range(0,6);
                enemy=Instantiate(marv,startPosition[randomNum].transform.position,Quaternion.identity);
                if(randomNum > 2)
                {
                    enemy.GetComponent<SpriteRenderer>().flipX = true;
                }
                lv2Timer=0;
            }

        }
        else if(level==3){
            if(lv1Timer>10.0f){
                enemy=Instantiate(humma1,startPosition[Random.Range(0,6)].transform.position,Quaternion.identity);
                lv1Timer=0;
            } 
        }
        //ending
        else if(level==100){
            if(lv1Timer>1.0f){
                enemy=Instantiate(humma1,startPosition[Random.Range(0,6)].transform.position,Quaternion.identity);
                lv1Timer=0;
            }
        }


        
    }
}
