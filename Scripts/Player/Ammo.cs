using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{

    public int municion;

    void Start()
    {
    }

    void Update()
    {
    }

    public void CollectAmmo()
    {
    Destroy(gameObject);
    }
}
