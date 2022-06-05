using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float dieTime;
    public GameObject diePEffect;
    private bool IsInitialized = false;
    private Vector2 direccion;
    private float shootspeed;
    public float damage;

    void Start()
    {
        StartCoroutine(Timer());
    }

    void FixedUpdate()
    {
        if (IsInitialized) 
        {
            transform.Translate(new Vector3(direccion.x, direccion.y , 0) * shootspeed * Time.fixedDeltaTime);
            //GetComponent<Rigidbody2D>().velocity = new Vector2(shootspeed * direccion * Time.fixedDeltaTime, 0f);
            //transform.localScale = new Vector2(transform.localScale.x * direccion, transform.localScale.y);
            //print("update bullet");
        }
        

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject collisionGameObject = collision.gameObject;

        if (collisionGameObject.tag != "Player")
        {
            Enemy enemy = collisionGameObject.GetComponent<Enemy>();
            enemy.Damage(damage);
            Die();
        }
    }
    IEnumerator Timer()
    {
        yield return new WaitForSeconds(dieTime);
        Die();
    }

    void Die()
    {
        if(diePEffect != null)
        {
            Instantiate(diePEffect, transform.position, Quaternion.identity); 
        }

        Destroy(gameObject);
    }

   public void Init(Vector2 _direction, float _shootspeed  )
   {
        direccion = _direction;
        shootspeed = _shootspeed;
        IsInitialized = true;
   }
}
