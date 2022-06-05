using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBullet : Bullet
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject collisionGameObject = collision.gameObject;

        if (collisionGameObject.tag == "Escudo")
        {
            Destroy(collisionGameObject);
            Destroy(gameObject);
        }
    }
}
