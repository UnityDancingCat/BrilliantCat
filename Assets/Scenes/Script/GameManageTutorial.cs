using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using static Score;
using static Player;

public class GameManageTutorial : MonoBehaviour
{
    //public float transitionTime = 60f;
    //public int scoreThreshold = 2000;

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
        
    }
}
