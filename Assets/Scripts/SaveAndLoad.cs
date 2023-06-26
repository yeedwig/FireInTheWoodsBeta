using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

//동물들 나올때마다 저장되는 부분은 animalManager에 존재


public class SaveAndLoad : MonoBehaviour
{
    [SerializeField] private GameObject Animal;
    public Animals[] animals;
    
    // Start is called before the first frame update
    void Start()
    {
        animals = Animal.GetComponentsInChildren<Animals>(true);
        /*if(GameStart.newGame){
            
            for(int i=0;i<animals.Length;i++)
            {
                string json = JsonUtility.ToJson(animals[i]);
                string fileName=animals[i].name;
                string path = Application.dataPath + "/GameData/" + fileName + ".Json";
                File.WriteAllText(path,json);
            }

            
            
        }
        else
        {
            
            for(int i=0;i<animals.Length;i++)
            {
                string path = Application.dataPath+"/GameData/"+animals[i].name+".json";
                string json = File.ReadAllText(path);
                JsonUtility.FromJsonOverwrite(json,animals[i]);
            }
        }*/
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Load()
    {

    }

   
}
