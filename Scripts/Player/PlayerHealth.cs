using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class PlayerHealth : MonoBehaviour
{
   
    public float health;
    public float maxHealth;
    public float inmunityTime;
    bool isInmune;
    public Text healtText;
    Animator anim;

    public void StartLevel() 
    {
        GameObject textGameObject;
        textGameObject = GameObject.Find("Canvas");

        Text[] childrens;
        childrens = textGameObject.GetComponentsInChildren<Text>(); //en hijos
        foreach (Text text in childrens)
        {
            if (text.gameObject.name == "Health")
            {
                healtText = text;
            }
        }
    }

    void Start()
    {
        health = maxHealth;
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        bool gameStarted;
        gameStarted = GetComponent<PlayerControler>().GetPlayerStart();

        if (gameStarted)
        {
            if (health > maxHealth)     // creamos este if por si el player se cura con algun objeto de vida y estar siempre al maximo y no pasarnos 
            {
                health = maxHealth;
            }
            updateHealText();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Enemy"))
        {
            
                
            
           

            DoDamage(collision.GetComponent<Enemy>().damageToGive);


        }
        else if (collision.CompareTag("EnemyBullet"))
        {
            

            DoDamage(collision.GetComponent<EnemyBullet>().damageToGive);

        }
    }

    public void DoDamage(float damage) 
    {
       
        if (!GameController.instance.GodMode)
        {
            if ( !isInmune)
            {
                health -= damage;
                StartCoroutine(Inmunity());
               
               

                if (health <= 0)
                {
                    GameController.instance.GameOver();
                    print("player dead");
                }
            }

        }
        

    }


       
        
    

    IEnumerator Inmunity() 
    { 
        isInmune = true;
        yield return new WaitForSeconds(inmunityTime);
        isInmune = false;
    }

    public void updateHealText() 
    {
        healtText.text = $"{health}";
    }
}
