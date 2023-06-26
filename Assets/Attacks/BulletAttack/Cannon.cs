using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{

    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform firePoint;

    public void Fire()
    {
        Instantiate(projectile, firePoint.position, firePoint.rotation);
    }
}
