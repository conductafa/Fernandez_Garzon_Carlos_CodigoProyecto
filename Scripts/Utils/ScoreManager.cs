using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ScoreManager : MonoBehaviour
{

    public TextAsset jsonInfo;
    public TextAsset highScoresFile;
    private ScoreInfo scoresInfo;

    public struct ScoreTable
    {
        public float totalScore;

        public float scoreByKill;
        public float scoreByTime;
        public float penaltyByDeath;

        public void Reset()
        {
            totalScore = 0;
            scoreByTime = 0;
            scoreByKill = 0;
            penaltyByDeath = 0;
        }
    }

    public struct HighScoresTable
    {
        string user;
        float score;

        HighScoresTable(string _user, float _score)
        {
            user = _user;
            score = _score;
        }
    }

   public enum EnemyTypeScore{
        Boss,
        Basic,
        Shooter,
        Shield
    }

    ScoreTable puntuacion;
    public static ScoreManager instance = null; // se crea una variable estatica comun entre los ScoreManager a null

    void Awake()
    {
        if (instance == null)  //comprovamos si se ha creado un ScoreManager
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            return; // cortamos la ejecucion 
        }

        Destroy(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        scoresInfo = JsonUtility.FromJson<ScoreInfo>(jsonInfo.text);
        puntuacion.Reset();
    }

    // Update is called once per frame
    void Update()
    {
        //if(Input.GetButtonUp("Jump") )
        //{
        //    ReadHighScores();
        //}
    }

    void AddPenaltyDeath()
    {

    }

    public void AddKillScore(EnemyTypeScore enemy)
    {
        switch(enemy)
        {
            case EnemyTypeScore.Boss:
                puntuacion.scoreByKill += scoresInfo.scorerByBoss;
                break;
            case EnemyTypeScore.Basic:
                puntuacion.scoreByKill += scoresInfo.scorerByEnemyBasic;
                break;
            case EnemyTypeScore.Shooter:
                puntuacion.scoreByKill += scoresInfo.scorerByEnemyShooter;
                break;
            case EnemyTypeScore.Shield:
                puntuacion.scoreByKill += scoresInfo.scorerByEnemyShield;
                break;
        }
    }

   public float EvaluateFinalScore()
    {
        puntuacion.totalScore = puntuacion.scoreByKill + puntuacion.scoreByTime + puntuacion.penaltyByDeath;
        if (GameController.instance.Win)
        {
            puntuacion.totalScore *= 1000;
        }

        return puntuacion.totalScore;
    }

    public string[] ReadHighScores()
    {
        TextAsset dataset = Resources.Load<TextAsset>("HighScores");
        string[] dataLines = dataset.text.Split('\n');
     
        for(int i = 0; i < dataLines.Length; i++) {
            string[] data = dataLines[i].Split(',');
            for(int d = 0; d < data.Length; d++) {
                // TODO: hacer algo con la puntuacion?
                Debug.Log(data[d]); 
            }
        }

        return dataLines;
    }

    public void AddUserScore(string user, float score)
    {
        System.IO.File.AppendAllText("Assets/Resources/HighScores.csv", "\n" + user + "," + score);
    }

  

}



