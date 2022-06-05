using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class LevelFinal : MonoBehaviour
{
    
    public Text titulo;
    public Text score;

    

    // Start is called before the first frame update
    void Start()
    {

        score.text = "" + ScoreManager.instance.EvaluateFinalScore();

        if (GameController.instance.Win)
        {
            titulo.text = "HAS GANADO";
        }
        else 
        {
            titulo.text = "HAS PERDIDO";
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
