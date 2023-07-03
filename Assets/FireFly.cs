using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireFly : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float moveTime;
    private float moveTimer;
    private int pathIndex;
    private Vector3 actualPosition;
    [SerializeField] GameObject[] RandomPoints;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        moveTimer += 1;
        if(moveTimer > moveTime)
        {
            pathIndex = Random.Range(0,6);
            moveTimer = 0;
        }
        actualPosition = this.transform.position;
        
        this.transform.position = Vector3.MoveTowards(actualPosition, RandomPoints[pathIndex].transform.position, moveSpeed * Time.deltaTime);
    }
}
