using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class NombreUsuario : MonoBehaviour
{

    public Text usuario;



    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void leerdatos() 
    {
        ScoreManager.instance.AddUserScore(usuario.text, ScoreManager.instance.EvaluateFinalScore());

            GameController.instance.LoadScoresLevel();
    }

}
