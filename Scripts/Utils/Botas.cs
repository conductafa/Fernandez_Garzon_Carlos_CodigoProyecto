using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Botas : MonoBehaviour
{
    public static bool botas = false;

    void Awake() 
    { 
        if (botas) 
        {
            Destroy(gameObject);

        }

        botas = true;   
        
    }
}
