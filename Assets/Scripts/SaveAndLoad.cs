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

    //캐릭터 저장
    [SerializeField] private GameObject mainCharacter;
    public MainCharacter mc;
    public MainCharacter mcTemp;

    //아이템북 저장
    [SerializeField] private GameObject itemBook;
    public ItemBook ib;

    //아이템 저장
    [SerializeField] private GameObject[] items;
    public Item[] itemArray;
    public Dictionary<string, Item> itemDic = new Dictionary<string, Item>();


    // Start is called before the first frame update
    void Start()
    {
        animals = Animal.GetComponentsInChildren<Animals>(true);
        gm = gameManager.GetComponent<GameManager>();
        mc = mainCharacter.GetComponent<MainCharacter>();
        ib = itemBook.GetComponent<ItemBook>();
        for(int i=0;i<itemArray.Length;i++){
            itemDic.Add(itemArray[i].itemName,itemArray[i]);
        }
        if(!GameStart.newGame){
            GameStart.newGame=true;
            Load();
        }
    }

    public void Save()
    {
        StartCoroutine(SaveGameManager());
        //StartCoroutine(SaveMainCharacter()); 
        StartCoroutine(SaveItems());
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
    public class itemData
    {
        public string name;
        public int itemNum;
    }
    IEnumerator SaveItems(){
        for(int i=0;i<items.Length;i++){
            itemData forSave = new itemData();
            forSave.name=items[i].GetComponent<Slot>().item.itemName;
            forSave.itemNum=items[i].GetComponent<Slot>().itemCount;
            string json = JsonUtility.ToJson(forSave);
            string fileName="Slot"+i.ToString();
            string path = Application.dataPath + "/GameData/" + fileName + ".Json";
            File.WriteAllText(path,json);  
        }
        yield return null;
    }

    void Load()
    {
        StartCoroutine(LoadAnimals());
        StartCoroutine(LoadGameManager());
        //StartCoroutine(LoadMainCharacter());
        StartCoroutine(LoadItemBook());
        StartCoroutine(LoadItems());
    }

    IEnumerator LoadAnimals(){
        for(int i=0;i<animals.Length;i++){
            animals[i].typeAppeared[0]=true;
            animals[i].typeAppeared[1]=true;
            animals[i].typeAppeared[2]=true;
        }
        yield return null;
    }

    IEnumerator LoadGameManager(){
        string path = Application.dataPath+"/GameData/"+"GameManager"+".json";
        string json = File.ReadAllText(path);
        gmTemp=new GameManager();
        JsonUtility.FromJsonOverwrite(json,gmTemp);
        gm.health=gmTemp.maxHealth;
        gm.kills=gmTemp.kills;
        gm.level=5;
        gm.maxHealth=gmTemp.maxHealth;
        gm.executeDamage=gmTemp.executeDamage;
        gm.invincible=false;
        gm.pause=false;
        gm.irochiCount=19;
        gm.defenseStage=true;
        yield return null;
    }

    /*IEnumerator LoadMainCharacter(){
        string path = Application.dataPath+"/GameData/"+"MainCharacter"+".json";
        string json = File.ReadAllText(path);
        mcTemp=new MainCharacter();
        JsonUtility.FromJsonOverwrite(json,mcTemp);
        mc.cameraMode=mcTemp.cameraMode;
        yield return null;
    }*/

    IEnumerator LoadItemBook(){
        for(int i=0;i<15;i++){
            ib.foundCombinations[i]=true;
        }
        yield return null;
    }

    IEnumerator LoadItems(){
        for(int i=0;i<items.Length;i++){
            string path = Application.dataPath+"/GameData/"+"Slot"+i.ToString()+".json";
            string json = File.ReadAllText(path);
            itemData forLoad = new itemData();
            JsonUtility.FromJsonOverwrite(json,forLoad);
            Debug.Log(forLoad.name);
            Debug.Log(forLoad.itemNum);
            items[i].GetComponent<Slot>().AddItem(itemDic[forLoad.name],forLoad.itemNum);
        }
        yield return null;
    }

   
}
