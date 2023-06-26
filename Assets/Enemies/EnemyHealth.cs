using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private int status = 0;
    public float hp;
    public float startHP;

    void Start() 
    {
        hp = startHP;
    }
    public void Damage(float damage)
    {
        if(hp > 0)
        {
            hp -= damage;
        }
    } 

    void Update()
    {
        //Debug.Log(hp);
    }
}
