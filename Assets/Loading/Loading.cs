using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Loading : MonoBehaviour
{
    [SerializeField] private Image loadBar;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadAsyncScene());
    }
    void update(){
        
    }

    IEnumerator LoadAsyncScene(){
        yield return null;
        AsyncOperation asyncScene;
        if(GameStart.newGame){
            asyncScene = SceneManager.LoadSceneAsync("Tutorial");
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
