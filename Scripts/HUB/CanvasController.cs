using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class CanvasController : MonoBehaviour
{

    public static CanvasController instance = null;
    public Text timer;
    public Canvas myCanvas = null;

    public GameObject menuPausa; 



    void Awake()
    {
        if (instance == null)  
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            return; 
        }

        Destroy(this.gameObject);
    }
    
    void Start()
    {
        
    }

    void Update()
    {
        timer.text = TimeSpan.FromSeconds(GameController.instance.Timer).ToString();

        if (myCanvas != null)
        {
            if (myCanvas.worldCamera == null)
            {
                myCanvas.worldCamera = Camera.main;
            }
        }


    }

    public void MenuPausa() 
    {
        
        menuPausa.SetActive(true);
        GameController.instance.PauseGame();
        Debug.Log("PAUSA");

    }


    public void SalirMenuPausa()
    {
        menuPausa.SetActive(false);
        GameController.instance.ResumeGame();

    }


    public void MenuPrincipal()
    {
        SceneManager.LoadScene("MainMenu");

    }


    public void MenuPuntuaciones()
    {
        SceneManager.LoadScene("Puntuaciones");

    }

}
