using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMelee : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision) 
    {
        GameObject collisionGameObject = collision.gameObject;

        if (collisionGameObject.tag == "Player") 
        {
            PlayerControler.instance.DoDamage(9999);
        }

        Debug.Log(collisionGameObject.name);

    }

}
