using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpawnPoint : MonoBehaviour
{
    public GameController.colorPortal color ;
    void Start()
    {
        GameController intncia = GameController.instance;
        if ( intncia.getCurrentPortal() == color)
        {
            GameObject player;
            player = GameObject.Find("player");

            if (player)
            {
                player.transform.position = transform.position;
            }
            else
            {
                //Debug.Log("Error player no encontrado");
            }
        }
    }
}

