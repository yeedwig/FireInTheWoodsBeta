using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ItemBook : MonoBehaviour
{
    [SerializeField] private GameObject itemBook;
    [SerializeField] private GameObject encyclopedia;
    public bool[] foundCombinations;
    
    [SerializeField] private GameObject[] itemObjects;
    [SerializeField] private Sprite[] itemSprites;
    public Image[] img;
    
    public Text[] textObjects;
    public string[] texts;
    
    private bool itemBookChange=true;
    [SerializeField] private int itemType=0;
    
    // Start is called before the first frame update
    void Start()
    {
        for(int i=0;i<15;i++){
            img[i]=itemObjects[i].GetComponent<Image>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(itemBookChange){
            if(foundCombinations[itemType*5])
            {
                img[0].sprite=itemSprites[itemType*15];
                img[1].sprite=itemSprites[itemType*15+1];
                img[2].sprite=itemSprites[itemType*15+2];
                textObjects[0].text=texts[itemType*5];
                itemObjects[0].SetActive(true);
                itemObjects[1].SetActive(true);
                itemObjects[2].SetActive(true);
            }
            else
            {
                itemObjects[0].SetActive(false);
                itemObjects[1].SetActive(false);
                itemObjects[2].SetActive(false);
                textObjects[0].text="";
            }
            if(foundCombinations[itemType*5+1])
            {
                img[3].sprite=itemSprites[itemType*15+3];
                img[4].sprite=itemSprites[itemType*15+4];
                img[5].sprite=itemSprites[itemType*15+5];
                textObjects[1].text=texts[itemType*5+1];
                itemObjects[3].SetActive(true);
                itemObjects[4].SetActive(true);
                itemObjects[5].SetActive(true);
            }
            else
            {
                itemObjects[3].SetActive(false);
                itemObjects[4].SetActive(false);
                itemObjects[5].SetActive(false);
                textObjects[1].text="";
            }
            if(foundCombinations[itemType*5+2])
            {
                img[6].sprite=itemSprites[itemType*15+6];
                img[7].sprite=itemSprites[itemType*15+7];
                img[8].sprite=itemSprites[itemType*15+8];
                textObjects[2].text=texts[itemType*5+2];
                itemObjects[6].SetActive(true);
                itemObjects[7].SetActive(true);
                itemObjects[8].SetActive(true);
            }
            else
            {
                itemObjects[6].SetActive(false);
                itemObjects[7].SetActive(false);
                itemObjects[8].SetActive(false);
                textObjects[2].text="";
            }
            if(foundCombinations[itemType*5+3])
            {
                img[9].sprite=itemSprites[itemType*15+9];
                img[10].sprite=itemSprites[itemType*15+10];
                img[11].sprite=itemSprites[itemType*15+11];
                textObjects[3].text=texts[itemType*5+3];
                itemObjects[9].SetActive(true);
                itemObjects[10].SetActive(true);
                itemObjects[11].SetActive(true);
            }
            else
            {
                itemObjects[9].SetActive(false);
                itemObjects[10].SetActive(false);
                itemObjects[11].SetActive(false);
                textObjects[3].text="";
            }
            if(foundCombinations[itemType*5+4])
            {
                img[12].sprite=itemSprites[itemType*15+12];
                img[13].sprite=itemSprites[itemType*15+13];
                img[14].sprite=itemSprites[itemType*15+14];
                textObjects[4].text=texts[itemType*5+4];
                itemObjects[12].SetActive(true);
                itemObjects[13].SetActive(true);
                itemObjects[14].SetActive(true);
            }
            else
            {
                itemObjects[12].SetActive(false);
                itemObjects[13].SetActive(false);
                itemObjects[14].SetActive(false);
                textObjects[4].text="";
            }
            itemBookChange=false;
        }
    }

    public void openItemBook(){
        itemBook.SetActive(true);
        itemBookChange=true;
        if(encyclopedia.activeInHierarchy){
            encyclopedia.SetActive(false);
        }
    }
    public void closeItemBook(){
        itemBook.SetActive(false);
    }

    public void tagOneOpen()
    {
        itemBookChange=true;
        itemType=0;
    }
    public void tagTwoOpen()
    {
        itemBookChange=true;
        itemType=1;
    }
    public void tagThreeOpen()
    {
        itemBookChange=true;
        itemType=2;
    }
}
