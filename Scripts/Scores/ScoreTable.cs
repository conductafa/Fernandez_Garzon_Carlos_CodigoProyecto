using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ScoreTable : MonoBehaviour
{
    private Transform entryContainer;
    private Transform entryTemplate;
    private Transform entryTable;
    private List<Transform> highscoreEntryTransformList;

    private void Awake()

    {
        entryTable = transform.Find("HighsocreTable");
        entryContainer = entryTable.Find("highscoreEntryContainer");
        entryTemplate = entryContainer.Find("highscoreEntryTemplate");

        entryTemplate.gameObject.SetActive(false);

        Highscores highscores = new Highscores();
        highscores.highscoreEntryList = new List<HighscoreEntry>();

        string[] scores = ScoreManager.instance.ReadHighScores();

        for(int i = 0; i < scores.Length; i++) {
            string[] data = scores[i].Split(',');

            HighscoreEntry tmp = new HighscoreEntry { score = int.Parse(data[1]), name = data[0] };
            highscores.highscoreEntryList.Add(tmp);
        }

        for(int i = 0; i < highscores.highscoreEntryList.Count; ++i )
        {
            for(int j = 0; j < highscores.highscoreEntryList.Count; ++j )
            {
                if(highscores.highscoreEntryList[i].score > highscores.highscoreEntryList[j].score)
                {
                    HighscoreEntry tmp = highscores.highscoreEntryList[i];
                    highscores.highscoreEntryList[i] = highscores.highscoreEntryList[j];
                    highscores.highscoreEntryList[j] = tmp;
                }
            }
        }

        highscoreEntryTransformList = new List<Transform>();
        foreach (HighscoreEntry highscoreEntry in highscores.highscoreEntryList) {
            CreateHighscoreEntryTransform(highscoreEntry, entryContainer, highscoreEntryTransformList);
        }

      
    }

        private void CreateHighscoreEntryTransform(HighscoreEntry highscoreEntry, Transform continer , List<Transform> transformList) {

            float templateHeigh = 30f;
            Transform entryTransform = Instantiate(entryTemplate, continer);
            RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
            entryRectTransform.anchoredPosition = new Vector2(0, -templateHeigh * transformList.Count);
            entryTransform.gameObject.SetActive(true);

            int rank = transformList.Count + 1;
            string rankString;

            switch (rank)
            {
                default:
                    rankString = rank + ""; break;

                case 1: rankString = "1"; break;
                case 2: rankString = "2"; break;
                case 3: rankString = "3"; break;

            }

            entryTransform.Find("posText").GetComponent<Text>().text = rankString;

            int score = highscoreEntry.score;

            entryTransform.Find("scoreText").GetComponent<Text>().text = score.ToString();

            string name = highscoreEntry.name;
            entryTransform.Find("nameText").GetComponent<Text>().text = name;

            entryTransform.Find("Background2").gameObject.SetActive(rank % 2 == 1 );
        


        if (rank == 1) 
        {
            entryTransform.Find("nameText").GetComponent<Text>().color = Color.green;
            entryTransform.Find("scoreText").GetComponent<Text>().color = Color.green;
            entryTransform.Find("posText").GetComponent<Text>().color = Color.green;


        }
        transformList.Add(entryTransform);

        }

    private void AddHighscoreEntry(int score, string name) {
      
    }

    private class Highscores 
    {
        public List<HighscoreEntry> highscoreEntryList;
    
    }


    
    [System.Serializable]
    private class HighscoreEntry {

        public int score;
        public string name;
        

    }

    void Start()
    {
        AddHighscoreEntry(10000, "asd");
    }

}
