using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Loading : MonoBehaviour
{
    //사용 객체
    [SerializeField] private Image loadBar;
    
    void Start()
    {
        StartCoroutine(LoadAsyncScene());
    }
    
    //로딩창 함수
    IEnumerator LoadAsyncScene(){
        yield return null;
        AsyncOperation asyncScene;
        //씬 이동 종류 확인 구문
        if(GameStart.newGame){
            if(TutorialManager.tutorialDone){
                TutorialManager.tutorialDone=false;
                asyncScene = SceneManager.LoadSceneAsync("FireScene");
            }
            else{
                asyncScene = SceneManager.LoadSceneAsync("Tutorial");
            }
        }
        else
        {
            asyncScene = SceneManager.LoadSceneAsync("FireScene");
        }
        asyncScene.allowSceneActivation = false;
        float timeC = 0;
        while(!asyncScene.isDone){
            yield return null;
            timeC += Time.deltaTime;
            if(asyncScene.progress<0.9f){
                loadBar.fillAmount = Mathf.Lerp(loadBar.fillAmount,asyncScene.progress,timeC);
                if(loadBar.fillAmount>=asyncScene.progress)
                {
                    timeC=0f;
                }
            }
            else
            {
                loadBar.fillAmount = Mathf.Lerp(loadBar.fillAmount,1f,timeC);
                if(loadBar.fillAmount==1.0f)
                {
                    asyncScene.allowSceneActivation = true;
                    yield break;
                }
            }
        }
        
    }
}
