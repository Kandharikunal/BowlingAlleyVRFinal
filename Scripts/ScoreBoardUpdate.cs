using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.UI;
using System;
using UnityEngine;
using TMPro;

public class ScoreBoardUpdate : MonoBehaviour
{
    public ScoreDB scoredb;

    string fpath ;

    public TextMeshProUGUI scoreboardUIText;
    string scores_text = "";

    // void WriteData() {
    //    string fileName = @"temp.txt"; 

    //    if (!File.Exists(fileName)) 
    //    {
    // 	 using (StreamWriter writer = File.CreateText(fileName))
    // 	       {
    //     	  writer.WriteLine("Hello World");
    // 	       }
    // }
    // }

    // string[] ReadData()
    // {
    //     string[] lines = System.IO.File.ReadAllLines(@"Assets/database/HighScores.txt");
    //     return lines;
    // }
    void perform()
    {
                // Test write to file.
        // WriteData();

        // Test Reading from file.
        // string[] data = ReadData();
        // string highScoreData = "";
        scoredb.LoadDataFromFile();
        int len=0;
        List<string> scores = new List<string>();
        // List<string> dateTime = new List<string>();
        // int length=scoredb.hightscores.Length;
        // String k="";
        // for(int p=0;p<length;p++)
        // {
        //     k+=scoredb.hightscores[p]+" ";
        // }
         using(StreamReader sr=File.OpenText(fpath))
         {
            string line;
            while((line = sr.ReadLine()) != null)
            {
                scores.Add(line);
                len++;
            }

         }

         List<int> last10 = new List<int>();
         int score_count = 10;
         
         if (scores.Count < 10)
            score_count = scores.Count;

         for(int j=0;j<score_count;j++)
         {
             if(len == 0)
                break;
            len--;
            last10.Add(Int32.Parse(scores[scores.Count - score_count + j]));
         }

         last10.Sort();
         last10.Reverse();
         
        
        // foreach (int score in scoredb.hightscores)
        // {
        //     // scores.Add(Int32.Parse(score));
        //     if(score>0){
        //         scores.Add(score);
        //     }
        // }

        //Now sorting the list
        // scores.Sort();

        //Reversing the sorted list
        // scores.Reverse();

        int i = 0;
        // scores=scores.Take(10);
        scores_text="";
        foreach (int score in last10)
        {
            scores_text += score + "\n";
        }
        // TextChange("scoresUI", scores_text);
        // TextChange(highScoreData);
        // scoreboardUIText.text=scores_text;
        // Debug.Log(scores_text);
        scoreboardUIText.text=scores_text; 
        Debug.Log(scores_text);
    }

    void Start()
    {
        fpath = Application.persistentDataPath + "/score.txt";
        perform();
    }

    // Update is called once per frame
    void Update()
    {
        // scoreboardUIText.text=scores_text;
        perform();

    }

    // public void TextChange(string gameObjectName, string txt) {
    //     changingText = GameObject.Find(gameObjectName).GetComponent<Text>();
    // 	changingText.text = txt;
    // }
}
