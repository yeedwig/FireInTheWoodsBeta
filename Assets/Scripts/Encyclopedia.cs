using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//최적화 완료
public class Encyclopedia : MonoBehaviour
{
    [SerializeField] private GameObject itemBook;
    [SerializeField] private GameObject encyclopedia;
    [SerializeField] private GameObject Animal;
    public Animals[] animals; //동물들의 상태를 불러오는 변수들
    
    private int page; //페이지 위치 저장

    //사진 3개 선언
    [SerializeField] private GameObject ver1;
    [SerializeField] private GameObject ver2;
    [SerializeField] private GameObject irochi;
    //배경 3개 선언
    [SerializeField] private GameObject ver1Background;
    [SerializeField] private GameObject ver2Background;
    [SerializeField] private GameObject irochiBackground;

    private Image ver1Img;
    private Image ver2Img;
    private Image irochiImg;

    public Sprite[] animalSprites;

    [SerializeField] private Text leftPage;
    [SerializeField] private Text rightPage;

    [SerializeField] private Text ver1Name;
    [SerializeField] private Text ver2Name;
    [SerializeField] private Text irochiName;
    [SerializeField] private Text ver1Text;
    [SerializeField] private Text ver2Text;
    [SerializeField] private Text irochiText;

    public string[] names;
    public string[] details;
    public bool encyclopediaChange = true;

    
    // Start is called before the first frame update
    void Start()
    {
        page=0;
        ver1Img=ver1.GetComponent<Image>();
        ver2Img=ver2.GetComponent<Image>();
        irochiImg=irochi.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if(encyclopediaChange)
        {
            
            if(animals[page].typeAppeared[0])
            {
                ver1.SetActive(true);
                ver1Img.sprite=animalSprites[page*3];
                ver1Name.text=names[page*3];
                ver1Text.text=details[page*3];
                ver1Background.SetActive(true);
            }
            else
            {
                ver1.SetActive(false);
                ver1Name.text="";
                ver1Text.text="";
                ver1Background.SetActive(false);
            } 
            if(animals[page].typeAppeared[1])
            {
                ver2.SetActive(true);
                ver2Img.sprite=animalSprites[page*3+1];
                ver2Name.text=names[page*3+1];
                ver2Text.text=details[page*3+1];
                ver2Background.SetActive(true);
            }
            else 
            {
                ver2.SetActive(false);
                ver2Name.text="";
                ver2Text.text="";
                ver2Background.SetActive(false);
            }
            if(animals[page].typeAppeared[2])
            {
                irochi.SetActive(true);
                irochiImg.sprite=animalSprites[page*3+2];
                irochiName.text=names[page*3+2];
                irochiText.text=details[page*3+2];
                irochiBackground.SetActive(true);
            }
            else 
            {
                irochi.SetActive(false);
                irochiName.text="";
                irochiText.text="";
                irochiBackground.SetActive(false);
            }
            leftPage.text=(page*2).ToString();
            rightPage.text=(page*2+1).ToString();
            encyclopediaChange=false;
        }
        
    }

    //넘기는 버튼 2개 전부 초기화 한번 시키기
    public void onLeftClick(){
        if(page!=0){
            page--;
            encyclopediaChange=true;
        } 
    }
    public void onRightClick(){
        if(page!=animals.Length-1){
            page++;
            encyclopediaChange=true;
        } 
    }

    public void openEncyclopedia(){
        encyclopedia.SetActive(true);
        encyclopediaChange=true;
        if(itemBook.activeInHierarchy){
            itemBook.SetActive(false);
        }
        
    }
    public void closeEncyclopedia(){
        encyclopedia.SetActive(false);
    }

    
    
}
