using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

//동물들 나올때마다 저장되는 부분은 animalManager에 존재


public class SaveAndLoad : MonoBehaviour
{

    //동물 저장
    [SerializeField] private GameObject Animal;
    public Animals[] animals;
    
    //게임 매니저 저장
    [SerializeField] private GameObject gameManager;
    public GameManager gm;
    public GameManager gmTemp;



    [SerializeField] private GameObject mainCharacter;


    // Start is called before the first frame update
    void Start()
    {
        animals = Animal.GetComponentsInChildren<Animals>(true);
        gm = gameManager.GetComponent<GameManager>();
        if(!GameStart.newGame){
            Load();
        }
        
    }

    
    public void Save()
    {
        StartCoroutine(SaveAnimals());
        StartCoroutine(SaveGameManager());
        //StartCoroutine(SaveMainCharacter());
        
    }

    IEnumerator SaveAnimals(){
        for(int i=0;i<animals.Length;i++){
            string json = JsonUtility.ToJson(animals[i]);
            string fileName=animals[i].name;
            string path = Application.dataPath + "/GameData/" + fileName + ".Json";
            File.WriteAllText(path,json);
            
        }
        yield return null;
    }

    IEnumerator SaveGameManager(){
        string json = JsonUtility.ToJson(gm);
        string fileName="GameManager";
        string path = Application.dataPath + "/GameData/" + fileName + ".Json";
        File.WriteAllText(path,json);
        yield return null;
    }

    /*IEnumerator SaveMainCharacter(){
        string json = JsonUtility.ToJson(mc);
        string fileName="MainCharacter";
        string path = Application.dataPath + "/GameData/" + fileName + ".Json";
        File.WriteAllText(path,json);
        yield return null;
    }*/

    void Load()
    {
        StartCoroutine(LoadAnimals());
        StartCoroutine(LoadGameManager());
        //StartCoroutine(LoadMainCharacter());
    }

    IEnumerator LoadAnimals(){
        for(int i=0;i<animals.Length;i++){
            string path = Application.dataPath+"/GameData/"+animals[i].name+".json";
            string json = File.ReadAllText(path);
            JsonUtility.FromJsonOverwrite(json,animals[i]);
            animals[i].currentState=false;
        }
        yield return null;
    }

    IEnumerator LoadGameManager(){
        string path = Application.dataPath+"/GameData/"+"GameManager"+".json";
        string json = File.ReadAllText(path);
        gmTemp=new GameManager();
        JsonUtility.FromJsonOverwrite(json,gmTemp);
        gm.health=gmTemp.health;
        gm.kills=gmTemp.kills;
        gm.level=gmTemp.level;
        gm.maxHealth=gmTemp.maxHealth;
        gm.executeDamage=gmTemp.executeDamage;
        gm.invincible=false;
        gm.pause=false;

        yield return null;
    }

    /*IEnumerator LoadMainCharacter(){
        
        yield return null;
    }*/

   
}
