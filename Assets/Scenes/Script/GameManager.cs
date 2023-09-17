using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using static Score;
using static Player;

public class GameManager : MonoBehaviour
{
    //public float transitionTime = 60f;
    //public int scoreThreshold = 2000;

    //public Text[] scoreData;

    void Update()
    {
        /* if (Input.GetKeyDown(KeyCode.A))
        {
            SceneManager.LoadScene("SuccessScene");
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            SceneManager.LoadScene("FailScene");
        } */
        int score = Score.CatScore;
        bool gameEnd = Player.gameEnd;

        if (gameEnd == true)
        {
            // UnityEngine.Debug.Log("score: " + score + ", gameEnd: " + gameEnd);
            if (score < 100)
            {
                SceneManager.LoadScene("FailScene", LoadSceneMode.Single);
            }

            else
            {
                SceneManager.LoadScene("SuccessScene", LoadSceneMode.Single);
            }
        }
        //사용자 점수를 PlayerPrefs에 저장
        //PlayerPrefs.SetFloat("PlayerScore", playerScore);
        //SceneManager.LoadScene(nextScene);
        
    }

    // public void Save()
    // {
    //     int score = Score.CatScore;
    //     if (score < PlayerPrefs.GetInt("BestScore")) {
    //         if (score < PlayerPrefs.GetInt("SecondScore"))
    //         {
    //             if (score < PlayerPrefs.GetInt("ThirdScore"))
    //                 return;
    //             //3등일 때
    //             PlayerPrefs.SetInt("ThirdScore", score);
    //             return;
    //         }
    //         //2등일 때
    //         PlayerPrefs.SetInt("ThirdScore", PlayerPrefs.GetInt("SecondScore"));
    //         PlayerPrefs.SetInt("SecondScore", score);
    //         return;
    //     }
    //     //1등일 때
    //     if (score == PlayerPrefs.GetInt("BestScore")) return;
    //     PlayerPrefs.SetInt("ThirdScore", PlayerPrefs.GetInt("SecondScore"));
    //     PlayerPrefs.SetInt("SecondScore", PlayerPrefs.GetInt("BestScore"));
    //     PlayerPrefs.SetInt("BestScore", score);
    // }

    // private void Load()
    // {
    //     scoreData[0].text = PlayerPrefs.GetInt("BestScore").ToString() + "점";
    //     scoreData[1].text = PlayerPrefs.GetInt("SecondScore").ToString() + "점";
    //     scoreData[2].text = PlayerPrefs.GetInt("ThirdScore").ToString() + "점";
    // }
}
