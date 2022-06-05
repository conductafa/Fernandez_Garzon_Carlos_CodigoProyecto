using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public float muertebala = 1; 
    public GameObject projectile;
    private bool IsInitialized = false;

    public float timeToShoot;
    float shootCooldown;

    void Start()
    {
        shootCooldown = timeToShoot; 
    }

    void Update()

    {
        shootCooldown -= Time.deltaTime;

        if (shootCooldown < 0)
        {
            GameObject bullete = Instantiate(projectile, transform.position, Quaternion.identity);
            bullete.GetComponent<timded>();

            timded tiempobala = bullete.GetComponent<timded>();
            tiempobala.dieTime = muertebala; 

            if (transform.localScale.x < 0)
            {
                bullete.GetComponent<Rigidbody2D>().AddForce(new Vector2(200f, 0f), ForceMode2D.Force);
            }
            else 
            {
                bullete.GetComponent<Rigidbody2D>().AddForce(new Vector2(-200f, 0f), ForceMode2D.Force);
            }

            shootCooldown = timeToShoot;
           
        }
    }
}
