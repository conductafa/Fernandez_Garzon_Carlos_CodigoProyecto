using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovements : MonoBehaviour
{
    Rigidbody2D rb;
    float speed;

    Animator anim;

    public GameObject zona;
    public bool isStatic;
    public Transform groundcheck;
    public LayerMask whatIsGround;
    public bool isWalker;
    public bool walksRight = false;
    public Transform wallCheck;
    public bool walldetected, isGround;
    public float detectionRadius;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {

        speed = GetComponent<Enemy>().speed;

        if (isStatic)
        {
            anim.SetBool("Idle", true);
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }

        if (isWalker)
        {
            if (walksRight)
            {
                rb.position = (rb.position + (new Vector2(1, 0) * speed * Time.fixedDeltaTime));
                // transform.Translate(new Vector3(-1, 0, 0) * speed * Time.fixedDeltaTime);
                //Debug.Log("posi");
            }
            else 
            {
                rb.position = (rb.position + (new Vector2(-1, 0) * speed * Time.fixedDeltaTime));
                //transform.Translate(new Vector3(1, 0, 0) * speed * Time.fixedDeltaTime);
                //Debug.Log("nega");
            }
        }
    }
    public void Flip()
    {
        walksRight = !walksRight;

        if (walksRight)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else           
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        GameObject collisionGameObject = collision.gameObject;

        if (collisionGameObject.name == zona.name)
        {
            Flip();
            Debug.Log("detectado");
            Debug.Log(collisionGameObject.name);
        }


    }
}
