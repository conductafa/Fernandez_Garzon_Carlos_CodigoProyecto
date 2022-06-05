using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class GameController : MonoBehaviour

{
    public bool puedoUsarPortal;


    private bool IsGamePaused = false;
    
    public float Timer = 0;
    public bool GodMode = false;
    public bool InfAmmo = false;
    public bool DoubleJump = false;
    public bool LiteVersion = false;
    public bool Win = false;


    public enum colorPortal
    {
       none, red, blue, green, count

    };


    private colorPortal curentPortal = colorPortal.none;

    GameObject canvasObject;
    GameObject playerObject;


    public  colorPortal getCurrentPortal() { return curentPortal; }
    public bool getGamePaused() { return IsGamePaused; } 

    public static GameController instance = null; // se crea una variable estatica comun entre los gamecontrller a null

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
        DontDestroyOnLoad(gameObject);

        Debug.Log(SceneManager.GetActiveScene().name);
        

        canvasObject = CanvasController.instance.gameObject;
        canvasObject.SetActive(false);

        if (SceneManager.GetActiveScene().name == "room")
        {
            WeaponController weaponController;
            PlayerControler playerControler;

            GameObject player = GameObject.Find("player");
            player.SetActive(true);
            // GameObject canvas = GameObject.Find("Canvas");
            canvasObject.SetActive(true);

            playerControler = player.GetComponent<PlayerControler>();
            playerControler.SetPlayerStart(true);

            weaponController = player.GetComponent<WeaponController>();
            weaponController.ResetAmmo();

        }
    }

    void Update()
    {
        Timer += Time.deltaTime;
    }

    public void LoadLevel(string level , colorPortal color ) 
    
    {
        puedoUsarPortal = false;
        curentPortal = color;

        PlayerControler playerControler;
        GameObject player = GameObject.Find("player");
        Debug.Log("level: " + level);

        Scene sceneToLoad = SceneManager.GetSceneByName(level);
        Debug.Log("scena: " + sceneToLoad.name);
        SceneManager.LoadScene( level , LoadSceneMode.Single);

        //SceneManager.MoveGameObjectToScene(FPSControllerr.gameObject, sceneToLoad);

        playerControler = player.GetComponent<PlayerControler>();

        playerControler.StartLevel();

    }

    public void LoadUILevel(string level)

    {
        PlayerControler playerControler;
        GameObject player = GameObject.Find("player");
        Debug.Log("level: " + level);
        Scene sceneToLoad = SceneManager.GetSceneByName(level);

        Debug.Log("scena: " + sceneToLoad.name);
        SceneManager.LoadScene(level, LoadSceneMode.Single);

        //SceneManager.MoveGameObjectToScene(FPSControllerr.gameObject, sceneToLoad);


    }

    public void LoadStartLevel(string level) 
    {
        WeaponController weaponController;
        PlayerControler playerControler;

        GameObject player = GameObject.Find("player");
        player.SetActive(true);
        // GameObject canvas = GameObject.Find("Canvas");
        canvasObject.SetActive(true);

        LoadLevel(level , colorPortal.blue);

        playerControler = player.GetComponent<PlayerControler>();
        playerControler.SetPlayerStart(true);

        weaponController = player.GetComponent<WeaponController>();
        weaponController.ResetAmmo();



    }

    public void GameOver() 
    {
        GameObject player = GameObject.Find("player");
        PlayerControler pc = player.GetComponent<PlayerControler>();
        
        pc.Reset();
        LoadLevel("MainMenu" , colorPortal.red);
        Destroy(gameObject);

    }

    public void ToggleGodMode(bool check )
    {
        GodMode = check;
        
    
    }

    public void ToggleLiteVersion(bool check)
    {
        LiteVersion = check;


    }
    

    public void ToggleDoubleJump(bool check)
    {
        DoubleJump = check;


    }

    public void ToggleInfAmmo(bool check)
    {
        InfAmmo = check;


    }

    public bool PauseGame() 
    {

     IsGamePaused = true;
        Time.timeScale = 0;
        return IsGamePaused;
        
    }

    public bool ResumeGame() 
    {

        IsGamePaused = false;
        Time.timeScale = 1;
        return IsGamePaused;


    }

    public void LoadScoresLevel() 
    {
        SceneManager.LoadScene("Puntaciones");
    }

    public void BotonGuardado(string nombre) 
    {
        float score = ScoreManager.instance.EvaluateFinalScore();
        ScoreManager.instance.AddUserScore(nombre , score);


        LoadScoresLevel();

    }

    public void SceneMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }


  

}
