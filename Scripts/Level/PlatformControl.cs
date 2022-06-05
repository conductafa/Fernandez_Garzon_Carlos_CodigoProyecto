using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformControl : MonoBehaviour
{

    PlatformEffector2D platformEffect;
    public bool leftPLatform;

    // Start is called before the first frame update
    void Start()
    {
        platformEffect = GetComponent<PlatformEffector2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S) && !leftPLatform)
        {
            platformEffect.rotationalOffset = 180;
            leftPLatform = true;
        
        }
        
    }

    private void OnCollisionExit2D(Collision2D collision)
    {


            platformEffect.rotationalOffset = 0;
            leftPLatform = false;
       

    }
}
