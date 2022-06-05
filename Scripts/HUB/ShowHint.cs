using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShowHint : MonoBehaviour
{
    bool MensajeActivo = false;
    bool PanelActivo = false;
    
    public bool mensajeMostrado = false;
    
    public string mensaje;
    int indexLetra;
    public float frecuenciaLetras = 0.5f;
    float lastTime = 0; 
   

    // Start is called before the first frame update
    void Start()
    {
       
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ActivateHint() 
    {
        if (!mensajeMostrado)
        {
            HintController.instance.MostrarMensaje(mensaje);
            mensajeMostrado = true;

        }

    }


   

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collisionGameObject = collision.gameObject;

        if (collisionGameObject.tag == "Player")
        {

            ActivateHint();

        }
    }






}
