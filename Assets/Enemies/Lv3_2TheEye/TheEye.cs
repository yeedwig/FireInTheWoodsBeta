using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheEye : MonoBehaviour
{
    private Animator anim;
    public GameObject GameManager;
    private SpriteRenderer sp;
    private int status = 0;
    public float health;
    public float startHP = 100;
    public bool Dead = false;
    private float state = 0;



    //사운드 관련
    public AudioClip clip;
    public AudioClip clip1;


    //특성 관련
    private float closeTimer;
    [SerializeField] private float closeTime;
    private float closeCount = 0;
    private float waitCount = 0;
    [SerializeField] private float closeLimit;
    [SerializeField] private float waitTime;
    public bool canSee = true;
    //불 관련
    private float originalMainMaxIntensity;
    private float originalMainMinIntensity;

    private float originalRadMaxIntensity;
    private float originalRadMinIntensity;

    private float originalGlobalIntensity;

    [SerializeField] private float changeMainMax;
    [SerializeField] private float changeMainMin;
    [SerializeField] private float changeRadMax;
    [SerializeField] private float changeRadMin;
    [SerializeField] private float changeGlobalIntensity;

    [SerializeField] private GameObject mainFireLight;
    [SerializeField] private GameObject radiusFireLight;
    [SerializeField] private GameObject globalLight;

    [SerializeField] private GameObject mainCharacterGO;
    private MainCharacter mainCharacter;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        sp = GetComponent<SpriteRenderer>();
        health = startHP;

        originalMainMaxIntensity = mainFireLight.GetComponent<LightFlicker>().maxIntensity;
        originalMainMinIntensity = mainFireLight.GetComponent<LightFlicker>().minIntensity;

        originalRadMaxIntensity = radiusFireLight.GetComponent<LightFlicker>().maxIntensity;
        originalRadMinIntensity = radiusFireLight.GetComponent<LightFlicker>().minIntensity;

        originalGlobalIntensity = globalLight.GetComponent<UnityEngine.Rendering.Universal.Light2D>().intensity;
        mainCharacter=mainCharacterGO.GetComponent<MainCharacter>();
        
    }

    public void Damage(float damage)
    {
        health -= (damage+mainCharacter.plusDamageByAnimalContract+mainCharacter.plusDamageByItem);
        
        
        if(health > 0)
        {
            SoundManager.instance.SFXPlay("EnemyHitSound",clip);
            anim.SetBool("Dead", false);
            StartCoroutine(damagedAnimation());
        }
        else if(health <= 0)
        {
            SoundManager.instance.SFXPlay("EnemyDeadSound",clip1);
            anim.SetBool("Dead", true);
            if(!Dead) GameObject.Find("GameManager").GetComponent<GameManager>().kills++;
            Dead = true;
            canSee = true;
                    
            mainFireLight.GetComponent<LightFlicker>().maxIntensity = originalMainMaxIntensity;
            mainFireLight.GetComponent<LightFlicker>().minIntensity = originalMainMinIntensity;
                    
            radiusFireLight.GetComponent<LightFlicker>().maxIntensity = originalRadMaxIntensity;
            radiusFireLight.GetComponent<LightFlicker>().minIntensity = originalRadMinIntensity;

            globalLight.GetComponent<UnityEngine.Rendering.Universal.Light2D>().intensity = originalGlobalIntensity;
            Destroy(this.gameObject,2.0f);
        }
    } 

    IEnumerator damagedAnimation()
    {
        anim.SetBool("Attacked", true);
        sp.color = new Color (1,1,1,0.5f);
        yield return new WaitForSeconds(0.3f);
        sp.color = new Color(1,1,1,1);
        anim.SetBool("Attacked", false);

    }


    void Update()
    {
        //Debug.Log(state);
        if(Dead == false)
        {     
            closeTimer += Time.deltaTime;
            if(closeTimer >= closeTime)
            {
                closeTimer = 0;
                if(state == 0)
                {
                    anim.SetBool("Closed", true);
                    canSee = false;

                    mainFireLight.GetComponent<LightFlicker>().maxIntensity = changeMainMax;
                    mainFireLight.GetComponent<LightFlicker>().minIntensity = changeMainMin;

                    radiusFireLight.GetComponent<LightFlicker>().maxIntensity = changeRadMax;
                    radiusFireLight.GetComponent<LightFlicker>().minIntensity = changeRadMin;

                    globalLight.GetComponent<UnityEngine.Rendering.Universal.Light2D>().intensity = changeGlobalIntensity;

                    closeCount++;
                    if(closeCount == closeLimit)
                    {
                        closeCount = 0;
                        state = 1;
                    }
                }
                if(state == 1)
                {
                    anim.SetBool("Closed", false);

                    canSee = true;

                    mainFireLight.GetComponent<LightFlicker>().maxIntensity = originalMainMaxIntensity;
                    mainFireLight.GetComponent<LightFlicker>().minIntensity = originalMainMinIntensity;
                    
                    radiusFireLight.GetComponent<LightFlicker>().maxIntensity = originalRadMaxIntensity;
                    radiusFireLight.GetComponent<LightFlicker>().minIntensity = originalRadMinIntensity;

                    globalLight.GetComponent<UnityEngine.Rendering.Universal.Light2D>().intensity = originalGlobalIntensity;
                    waitCount++;
                    if(waitCount == waitTime)
                    {
                        waitCount = 0;
                        state = 0;
                    }
                }
            }
        }
    }
}
