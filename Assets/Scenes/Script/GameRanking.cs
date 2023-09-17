using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameRanking : MonoBehaviour
{
    public Text RankNameCurrent;
    public Text RankScoreCurrent;
    public Text[] RankScoreText;
    public Text[] RankNameText;

    private GameRanking gameRanking;

    private float[] bestScore = new float[5];
    private string[] bestName = new string[5];

    private void Start()
    {
        UpdateRanking();
    }

    // 현재 플레이어의 점수와 이름을 받아 랭킹을 업데이트합니다.
    public void ScoreSet(float currentScore, string currentName)
    {
        // 일단 현재 플레이어의 점수와 이름을 PlayerPrefs에 저장
        PlayerPrefs.SetString("CurrentPlayerName", currentName);
        PlayerPrefs.SetFloat("CurrentPlayerScore", currentScore);

        for (int i = 0; i < 5; i++)
        {
            // 저장된 최고 점수와 이름을 가져옵니다.
            bestScore[i] = PlayerPrefs.GetFloat(i + "BestScore");
            bestName[i] = PlayerPrefs.GetString(i + "BestName");
        }

        // 현재 점수가 랭킹에 들어갈 수 있는지 확인하고 업데이트합니다.
        for (int i = 0; i < 5; i++)
        {
            if (currentScore > bestScore[i])
            {
                // 현재 점수가 i번째 순위에 들어갑니다.
                for (int j = 4; j > i; j--)
                {
                    bestScore[j] = bestScore[j - 1];
                    bestName[j] = bestName[j - 1];
                }

                bestScore[i] = currentScore;
                bestName[i] = currentName;

                // 랭킹에 맞게 PlayerPrefs에 저장합니다.
                PlayerPrefs.SetFloat(i + "BestScore", bestScore[i]);
                PlayerPrefs.SetString(i + "BestName", bestName[i]);

                break; // 랭킹 업데이트가 완료되었으므로 반복 종료
            }
        }

        // 랭킹 업데이트 후 UI를 다시 표시합니다.
        UpdateRanking();
    }

    private void UpdateRanking()
    {
        // 현재 플레이어의 이름과 점수를 UI에 표시합니다.
        RankNameCurrent.text = PlayerPrefs.GetString("CurrentPlayerName");
        RankScoreCurrent.text = string.Format("{0:N3}cm", PlayerPrefs.GetFloat("CurrentPlayerScore"));

        // 랭킹 순위별로 UI를 업데이트합니다.
        for (int i = 0; i < 5; i++)
        {
            RankScoreText[i].text = string.Format("{0:N3}cm", bestScore[i]);
            RankNameText[i].text = bestName[i];

            // 현재 플레이어와 같은 순위에 노란색 표시를 추가합니다.
            if (RankScoreCurrent.text == RankScoreText[i].text)
            {
                RankScoreText[i].color = Color.yellow;
                RankNameText[i].color = Color.yellow;
            }
            else
            {
                RankScoreText[i].color = Color.white;
                RankNameText[i].color = Color.white;
            }
        }
    }
}










// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UI;

// public class GameRanking : MonoBehaviour
// {
//     public Text RankNameCurrent;
//     public Text RankScoreCurrent;
//     public Text[] RankScoreText;
//     public Text[] RankNameText;

//     private List<PlayerRank> playerRanks = new List<PlayerRank>();

//     [System.Serializable]
//     public class PlayerRank
//     {
//         public string Name;
//         public float Score;

//         public PlayerRank(string name, float score)
//         {
//             Name = name;
//             Score = score;
//         }
//     }

//     private void Start()
//     {
//         LoadRankingData(); // 기존 랭킹 데이터 로드
//         UpdateRanking();
//     }

//     // 현재 플레이어의 점수와 이름을 받아서 실행
//     public void ScoreSet(float currentScore, string currentName)
//     {
//         PlayerPrefs.SetString("CurrentPlayerName", currentName);
//         PlayerPrefs.SetFloat("CurrentPlayerScore", currentScore);

//         UpdateRanking();
//     }

//     private void UpdateRanking()
//     {
//         // 현재 플레이어 정보
//         string currentPlayerName = PlayerPrefs.GetString("CurrentPlayerName");
//         float currentPlayerScore = PlayerPrefs.GetFloat("CurrentPlayerScore");

//         // 현재 플레이어 정보를 랭킹에 추가
//         PlayerRank currentPlayerRank = new PlayerRank(currentPlayerName, currentPlayerScore);
//         playerRanks.Add(currentPlayerRank);

//         // 랭킹 정렬
//         playerRanks.Sort((a, b) => b.Score.CompareTo(a.Score));

//         // 상위 5명만 표시 (또는 필요한 만큼)
//         for (int i = 0; i < Mathf.Min(playerRanks.Count, 5); i++)
//         {
//             RankScoreText[i].text = playerRanks[i].Score.ToString("N3");
//             RankNameText[i].text = playerRanks[i].Name;

//             if (playerRanks[i].Name == currentPlayerName && playerRanks[i].Score == currentPlayerScore)
//             {
//                 RankScoreText[i].color = Color.yellow;
//                 RankNameText[i].color = Color.yellow;
//             }
//             else
//             {
//                 RankScoreText[i].color = Color.white;
//                 RankNameText[i].color = Color.white;
//             }
//         }

//         // 랭킹 데이터 저장
//         SaveRankingData();
//     }

//     private void LoadRankingData()
//     {
//         playerRanks.Clear();

//         for (int i = 0; i < 5; i++)
//         {
//             string playerName = PlayerPrefs.GetString(i + "BestName");
//             float playerScore = PlayerPrefs.GetFloat(i + "BestScore");
//             PlayerRank playerRank = new PlayerRank(playerName, playerScore);
//             playerRanks.Add(playerRank);
//         }
//     }

//     private void SaveRankingData()
//     {
//         for (int i = 0; i < 5; i++)
//         {
//             if (i < playerRanks.Count)
//             {
//                 PlayerPrefs.SetString(i + "BestName", playerRanks[i].Name);
//                 PlayerPrefs.SetFloat(i + "BestScore", playerRanks[i].Score);
//             }
//             else
//             {
//                 // 랭킹 데이터가 부족한 경우 초기화
//                 PlayerPrefs.SetString(i + "BestName", "");
//                 PlayerPrefs.SetFloat(i + "BestScore", 0f);
//             }
//         }
//     }
// }






// public class GameRanking : MonoBehaviour
// {
//     public Text RankNameCurrent;
//     public Text RankScoreCurrent;
//     public Text[] RankScoreText;
//     public Text[] RankNameText;
//     //public Text[] RankText;
    
//     private float[] bestScore = new float[5];
//     private string[] bestName = new string[5];

//     private void Start()
//     {
//         UpdateRanking();
//     }

//     //현재 플레이어의 점수와 이름을 받아서 실행
//     public void ScoreSet(float currentScore, string currentName)
//     {
//         //일단 현재에 저장하고 시작
//         PlayerPrefs.SetString("CurrentPlayerName", currentName);
//         PlayerPrefs.SetFloat("CurrentPlayerScore", currentScore);

//         //float tmpScore = 0f;
//         //string tmpName = "";

//         for (int i = 0; i < 5; i++)
//         {
//             //저장된 최고점수와 이름을 가져오기
//             bestScore[i] = PlayerPrefs.GetFloat(i + "BestScore");
//             bestName[i] = PlayerPrefs.GetString(i + "BestName");
//         }

//             //현재 점수가 랭킹에 오를 수 있을 때
//             //while (bestScore[i] < currentScore)
//             //{
//                 //자리바꾸기
//                 //tmpScore = bestScore[i];
//                 //tmpName = bestName[i];
//                 //bestScore[i] = currentScore;
//                 //bestName[i] = currentName;

//                 //랭킹에 저장
//                 //PlayerPrefs.SetFloat(i + "BestScore")
//                 //PlayerPrefs.SetString(i.ToString() + "BestName", currentName);

//                 //다음 반복을 위한 준비
//                 //currentScore = tmpScore;
//                 //currentName = tmpName;
//             //}
        
//         //랭킹에 맞춰 점수와 이름 저장
//         for (int i = 0; i < 5; i++)
//         {
//             if (currentScore > bestScore[i])
//             {
//                 // 현재 점수가 i번째 순위에 들어갑니다.
//                 for (int j = 4; j > i; j--)
//                 {
//                     bestScore[j] = bestScore[j - 1];
//                     bestName[j] = bestName[j - 1];
//                 }

//                 bestScore[i] = currentScore;
//                 bestName[i] = currentName;

//                 // 랭킹에 맞게 PlayerPrefs에 저장합니다.
//                 PlayerPrefs.SetFloat(i + "BestScore", bestScore[i]);
//                 PlayerPrefs.SetString(i + "BestName", bestName[i]);

//                 break; // 랭킹 업데이트가 완료되었으므로 반복 종료
//             }
//         }

//         UpdateRanking();

//     }

//     private void UpdateRanking()
//     {
//         //플레이어 이름, 점수 텍스트를 현재 '나'의 이름과 점수에 표시
//         RankNameCurrent.text = PlayerPrefs.GetString("CurrentPlayerName");
//         RankScoreCurrent.text = string.Format("{0:N3}cm", PlayerPrefs.GetFloat("CurrentPlayerScore"));

//         //랭킹에 맞춰 불러온 점수와 이름을 표시하는 부분
//         for (int i = 0; i < 5; i++)
//         {
//             RankScoreText[i].text = string.Format("{0:N3}cm", bestScore[i]);
//             RankNameText[i].text = bestName[i];

//             // 현재 플레이어와 같은 순위에 노란색 표시를 추가합니다.
//             if (RankScoreCurrent.text == RankScoreText[i].text)
//             {
//                 RankScoreText[i].color = Color.yellow;
//                 RankNameText[i].color = Color.yellow;
//             }
//             else
//             {
//                 RankScoreText[i].color = Color.white;
//                 RankNameText[i].color = Color.white;
//             }
//         }
//     }

// }
