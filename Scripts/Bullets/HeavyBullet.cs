using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyBullet : Bullet
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject collisionGameObject = collision.gameObject;

        if (collisionGameObject.tag == "Pared")
        {
            Destroy(collisionGameObject);
            Destroy(gameObject);
        }
    }
}
