using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;


public class MainMenu : MonoBehaviour {

    public AudioMixer audio;

    public Dropdown resolucionesDrop;

    Resolution[] resolutions; 

    // Start is called before the first frame update
    void Start() // creacion de opciones de resoliucion 
    {
        resolutions = Screen.resolutions;

        if (resolucionesDrop != null)
        {

            

            resolucionesDrop.ClearOptions();
        }


        List<string> opciones = new List<string>();

        int actualIndex = 0;
        for (int i = 0; i < resolutions.Length; i++) 
        {
            string opcion = resolutions[i].width + "x" + resolutions[i].height;
            opciones.Add(opcion);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)

            {
                actualIndex = i;
            }
        
        }

        if (resolucionesDrop != null)
        {
            resolucionesDrop.AddOptions(opciones);
            resolucionesDrop.value = actualIndex;
            resolucionesDrop.RefreshShownValue();
        }



    }

    // Update is called once per frame
    void Update()
    {
    }

    public void StateGame()
    {
        GameObject gm = GameObject.Find("GameManager");
        GameController gameController = gm.GetComponent<GameController>();
        gameController.LoadStartLevel("Tutorial");   
    }

    public void SceneOptions()
    {
        SceneManager.LoadScene("Opciones");
    }

    public void SceneScores()
    {
        SceneManager.LoadScene("Puntaciones");
    }

    public void SceneHelp()
    {
        SceneManager.LoadScene("Ayuda");
    }

    public void SceneMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }



    public void ExitGame()
    {
        Application.Quit();
    }

    public void GodMode(bool check) 
    {
        GameController.instance.ToggleGodMode(check);
        
       

    }

    public void InfAmmo(bool check)
    {
        GameController.instance.ToggleInfAmmo(check);
        
  
    }


    public void LiteVersion(bool check)
    {
        GameController.instance.ToggleLiteVersion(check);
    }


    public void DoubleJump(bool check)
    {
        GameController.instance.ToggleDoubleJump(check);

    }

    public void SetResolucion(int resolucionIndex) 
    {
        Resolution resolucion = resolutions[resolucionIndex];
        Screen.SetResolution(resolucion.width, resolucion.height, Screen.fullScreen);
    }

    public void SetVolume(float volumen )
    {
        audio.SetFloat("volumen", volumen);
        
    }

    public void SetGraficos(int graficosIndex)
    {
        QualitySettings.SetQualityLevel(graficosIndex);
    }

    public void SetPantallaCompleta(bool isPantallaCompleta) 
    {
        Screen.fullScreen = isPantallaCompleta;
    }



}
