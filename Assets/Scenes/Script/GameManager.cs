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
    public bool gameEnd;
    public bool playGame;

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
        int score = Score.curScore;
        gameEnd = Player.gameEnd;
        playGame = Player.playGame;

        if ((gameEnd == true) && (playGame == false))
        {
            // UnityEngine.Debug.Log("score: " + score + ", gameEnd: " + gameEnd);
            if (score < 50)
            {
                SceneManager.LoadScene("FailScene", LoadSceneMode.Single);
            }

            else
            {
                SceneManager.LoadScene("SuccessScene", LoadSceneMode.Single);
            }
        }
    }
}
