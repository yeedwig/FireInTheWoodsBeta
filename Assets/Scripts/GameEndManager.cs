using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameEndManager : MonoBehaviour
{
    public GameObject gameOverImage;
    public GameObject gameOverText;
    public GameObject retry;
    public GameObject exit;
    public Image image;
    public ColorBlock colorBlock;

    private float time;

    public void GameOverStart(){
        time=0;
        image=gameOverImage.GetComponent<Image>();
        StartCoroutine(GameOver());
    }

    IEnumerator GameOver(){
        while(time*0.05f<1.3f){
            image.color= new Color(image.color.r,image.color.g,image.color.b,time*0.05f);
            yield return new WaitForSeconds(0.1f);
            time++;
        }
        time=0;
        image=gameOverText.GetComponent<Image>();
        while(time*0.1f<1.3f){
            image.color= new Color(image.color.r,image.color.g,image.color.b,time*0.1f);
            yield return new WaitForSeconds(0.1f);
            time++;
        }
        retry.SetActive(true);
        exit.SetActive(true); 
    }

    public void onClickRetry(){
        SceneManager.LoadScene("Loading");
    }

    public void onClickExit(){
        SceneManager.LoadScene("GameStart");
    }



    /*IEnumerator GameClear(){

    }*/
}
