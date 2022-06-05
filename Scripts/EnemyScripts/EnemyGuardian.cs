using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGuardian : Enemy
{
    public float DistanciaGuardia;
    public float speedGuardia;
    float speedDefault;
    private bool isPlayerInside = false;

    // Start is called before the first frame update
    void Start()
    {
        speedDefault = speed;
        
    }

    // Update is called once per frame
    void Update()
    {

        
    }

   public  void ActivarGuardia() 
    {
        countGuardiaActiva++;

        if (guardiaActiva == false)
        {
            GameObject[] EnemyScene;
            EnemyScene = GameObject.FindGameObjectsWithTag("Enemy");
            guardiaActiva = true;
            speed = speedGuardia;

            foreach (GameObject enemy in EnemyScene)
            {
                if (enemy != gameObject)
                {
                    Debug.Log(Vector3.Distance(transform.position, enemy.transform.position));

                    if (Vector3.Distance(transform.position, enemy.transform.position) < DistanciaGuardia)
                    {


                        enemy.GetComponent<Enemy>().ActivarGuardia();

                        
                    }

                }

            }

            
        }
        
    }

    public void DesactivarGuardia() 
    {
        countGuardiaActiva--;

        if (guardiaActiva == true && ((countGuardiaActiva <= 1 && isPlayerInside == false) || helthPoint <= 0))
        {
            GameObject[] EnemyScene;
            EnemyScene = GameObject.FindGameObjectsWithTag("Enemy");

            foreach (GameObject enemy in EnemyScene)
            {
                if (enemy != gameObject)
                {
                    if (Vector3.Distance(transform.position, enemy.transform.position) < DistanciaGuardia)
                    {

                        enemy.GetComponent<Enemy>().DesactivarGuardia();
                    }
                }
            }

            guardiaActiva = false;
            speed = speedDefault;

        }
     
    }

    public void PlayerEnter() 
    { 
        isPlayerInside = true;
        ActivarGuardia();
    
    }


    public void PlayerExit()
    {
        isPlayerInside = false;
        DesactivarGuardia();

    }



}
