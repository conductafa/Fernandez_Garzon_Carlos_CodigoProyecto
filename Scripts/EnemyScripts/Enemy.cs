using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour //clase principal de todos los enemigos
{
    public string enemyName;
    public float helthPoint;
    public float damageToGive;
    public float speed;
    public bool guardiaActiva = false;
    public float multiplcadorSpeed = 2f;
    public float multiplicadorDamageToGive = 2f;
    protected int countGuardiaActiva = 0;


    public void Damage(float damage) 
    {
        helthPoint = helthPoint - damage;

        if (helthPoint <= 0)
        {
            ScoreManager.instance.AddKillScore(ScoreManager.EnemyTypeScore.Basic);
            Destroy(gameObject);
            
        }
    }


    public void ActivarGuardia()
    {
        if (guardiaActiva == false) 
        { 
            guardiaActiva = true;
            speed = speed * multiplcadorSpeed;
            damageToGive = damageToGive * multiplicadorDamageToGive;
        }

        countGuardiaActiva++;

    }

    public void DesactivarGuardia()
    {
        countGuardiaActiva--;

        if (guardiaActiva == true && countGuardiaActiva == 0)
        {
            guardiaActiva = false;
            speed = speed / multiplcadorSpeed;
            damageToGive = damageToGive / multiplicadorDamageToGive;
        }

        
    }


}

