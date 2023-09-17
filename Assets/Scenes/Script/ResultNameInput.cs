using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResultNameInput : MonoBehaviour
{
    public InputField playerNameInput;
    private string playerName = "";

    private int playerScore; // 플레이어 점수를 저장할 변수

    private void Start()
    {
        // FinalScore 스크립트를 가진 GameObject
        GameObject finalScoreObject = GameObject.Find("YourFinalScoreObjectName");

        // FinalScore 스크립트가 있는 경우 ScoreF 변수를 가져옴
        if (finalScoreObject != null)
        {
            FinalScore finalScoreScript = finalScoreObject.GetComponent<FinalScore>();
            if (finalScoreScript != null)
            {
                playerScore = finalScoreScript.ScoreF;
            }
        }
    }

    private void Update()
    {
        // 키보드
        if (playerName.Length > 0 && Input.GetKeyDown(KeyCode.Return))
        {
            InputName();
        }
    }

    // 마우스
    public void InputName()
    {
        playerName = playerNameInput.text;
        PlayerPrefs.SetString("CurrentPlayerName", playerName);

        // GameManager의 ScoreSet 메서드 호출 (플레이어 점수 전달)
        GameRanking gameRanking = FindObjectOfType<GameRanking>();
        gameRanking.ScoreSet(playerScore, playerName);
    }

    public void NameInput()
    {
        SceneManager.LoadScene("RankingScene");
    }
}





// public class ResultNameInput : MonoBehaviour
// {
//     public InputField playerNameInput;
//     private string playerName = null;

//     private int playerScore; // 플레이어 점수를 저장할 변수
 
//     private void Awake()
//     {
//         playerName = playerNameInput.GetComponent<InputField>().text;
//     }
 
//     private void Update()
//     {
//         //키보드
//         if (playerName.Length > 0 && Input.GetKeyDown(KeyCode.Return))
//         {
//             InputName();
//         }
//     }
 
//     //마우스
//     public void InputName()
//     {
//         playerName = playerNameInput.text;
//         PlayerPrefs.SetString("CurrentPlayerName", playerName);
//         //GameManager.Score.ScoreSet(GameManager.Score.CatScore, playerName);

//         GameRanking gameRanking = FindObjectOfType<GameRanking>();
//         gameRanking.ScoreSet(playerScore, playerName);
//     }

//     public void NameInput()
//     {
//         SceneManager.LoadScene("RankingScene");
//     }
// }
