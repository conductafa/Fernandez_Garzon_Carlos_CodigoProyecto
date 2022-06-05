using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class NextLevel : MonoBehaviour
{
    public GameController.colorPortal color;

    private GameController gameController;
    public string level;

    public string levelLite;
    public bool Final =  false;



    void Start()
    {

        GameObject go = GameObject.Find("GameManager");

        if (go)
        {
            gameController = go.GetComponent<GameController>();
        }
        else
        {
            Debug.Log("ERROR: GameManager no encontrado");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("salida de portal");


        if (other.gameObject.tag == "Player")
        {
            if (gameController.puedoUsarPortal == true)
            {
                if (!Final)
                {

                    if (SceneManager.GetActiveScene().name != level)
                    {
                        if (!GameController.instance.LiteVersion)
                        {

                            gameController.LoadLevel(level, color);

                        }
                        else
                        {
                            gameController.LoadLevel(levelLite, color);
                        }

                    }

                }
                else 
                {
                    GameController.instance.Win = true;
                    SceneManager.LoadScene("FinJuego");
                }
            }
            else
            {
           //     gameController.puedoUsarPortal = true;
            }
        }
        
        
    }

    void OnTriggerExit2D(Collider2D other)
    {

        if (other.gameObject.tag == "Player")
        {
            gameController.puedoUsarPortal = true;

        }
    }


}
