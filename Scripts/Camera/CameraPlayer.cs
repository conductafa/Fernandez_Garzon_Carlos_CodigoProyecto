using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPlayer : MonoBehaviour 
{

    private GameObject player;
    private Vector3 distancia;

    public static CameraPlayer instance = null; // se crea una variable estatica comun entre los gamecontrller a null

    void Awake()
    {
        if (instance == null)  //comprovamos si se ha creado un gamecontroler
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            return; // cortamos la ejecucion 
        }

        Destroy(this.gameObject);
    }


    void Start()
    {
        player = PlayerControler.instance.gameObject;
        distancia = new Vector3(0, 0 ,-10);      
    }

    void Update()
    {
        if (player)
        {
            transform.position = player.transform.position + distancia;
        } else  
        
        {
            player = PlayerControler.instance.gameObject;
        }
    }
}
