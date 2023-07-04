using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] private GameObject[] ShadowGatesPrefab;
    [SerializeField] private Transform[] GatePosition;
    [SerializeField] private float BossMAXHP = 10000;
    private float BossHP;

    private bool gate1Instantiated = false;
    private bool gate2Instantiated = false;
    private bool gate3Instantiated = false;
    private bool gate4Instantiated = false;
    
    private bool Angry;
    private bool Damaged;
    private Animator anim;
    public GameObject GameManager;
    private float Phase;

    //Timer
    private float phaseTimer;
    [SerializeField] private float phaseTime;

    //Camera
    [SerializeField] private Camera theCamera;

    // Start is called before the first frame update
    void Start()
    {
        
        anim = GetComponent<Animator>();
        theCamera.orthographicSize = 50;
        BossHP = BossMAXHP;
        Phase = 1;
    }

    public void DamageBoss(float Damage)
    {
        BossHP -= Damage;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Phase);
        //Debug.Log(BossHP);
        if(Phase == 1 && gate1Instantiated == false)
        {
            Instantiate(ShadowGatesPrefab[0], GatePosition[0].position, GatePosition[0].rotation);
            gate1Instantiated = true;
        }
        else if(BossHP == 9000 && Phase == 1)
        {
            Debug.Log("inLoop");
            anim.SetBool("Damaged", true);
            phaseTimer += 1;
            if(phaseTimer > phaseTime)
            {
                phaseTimer = 0;
                Phase += 1;
            }
        }

        else if(Phase == 2 && gate2Instantiated == false)
        {
            anim.SetBool("Damaged", false);
            Instantiate(ShadowGatesPrefab[0], GatePosition[0].position, GatePosition[0].rotation);
            Instantiate(ShadowGatesPrefab[1], GatePosition[1].position, GatePosition[1].rotation);
            gate2Instantiated = true;
        }
        else if(BossHP == 7000 && Phase == 2)
        {
            anim.SetBool("Damaged", true);
            phaseTimer += 1;
            if(phaseTimer > phaseTime)
            {
                phaseTimer = 0;
                Phase += 1;
            }
        }

        else if(Phase == 3 && gate3Instantiated == false)
        {
            anim.SetBool("Damaged", false);
            Instantiate(ShadowGatesPrefab[0], GatePosition[0].position, GatePosition[0].rotation);
            Instantiate(ShadowGatesPrefab[1], GatePosition[1].position, GatePosition[1].rotation);
            Instantiate(ShadowGatesPrefab[2], GatePosition[2].position, GatePosition[2].rotation);
            gate3Instantiated = true;
        }
        else if(BossHP == 4000 && Phase == 3)
        {
            anim.SetBool("Damaged", true);
            phaseTimer += 1;
            if(phaseTimer > phaseTime)
            {
                phaseTimer = 0;
                Phase += 1;
            }
        }

        else if(Phase == 4 && gate4Instantiated == false)
        {
            anim.SetBool("Damaged", false);
            Instantiate(ShadowGatesPrefab[0], GatePosition[0].position, GatePosition[0].rotation);
            Instantiate(ShadowGatesPrefab[1], GatePosition[1].position, GatePosition[1].rotation);
            Instantiate(ShadowGatesPrefab[2], GatePosition[2].position, GatePosition[2].rotation);
            Instantiate(ShadowGatesPrefab[3], GatePosition[3].position, GatePosition[3].rotation);
            gate4Instantiated = true;
        }
        else if(BossHP == 0 && Phase == 4)
        {
            anim.SetBool("Damaged", true);
            phaseTimer += 1;
            if(phaseTimer > phaseTime)
            {
                phaseTimer = 0;
                Phase += 1;
            }
        }

        else if(Phase == 5)
        {
            //여기서 엔딩 구현
            GameManager.GetComponent<GameManager>().cleared = true;
        }
    }
}
