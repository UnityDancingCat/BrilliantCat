using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonOnClick : MonoBehaviour
{
    //public InputField playerNameInput;
    
    public void ReplayLv1()
    {
        SceneManager.LoadScene("Lev1Scene");
    }

    public void Quit() //게임 레벨 선택 씬
    {
        SceneManager.LoadScene("StartScene");
    }

    public void Home()
    {
        SceneManager.LoadScene("StartScene");
    }

    public void Play()
    {
        //PlayerPrefs.SetString("PlayerName", playerNameInput.text);
        SceneManager.LoadScene("LevSelecScene");
    }

    public void Tutorial()
    {
        SceneManager.LoadScene("TutorialScene");
    }

    public void Setting()
    {
        SceneManager.LoadScene("SelectScene");
    }

    public void Ranking()
    {
        SceneManager.LoadScene("RankingScene");
    }

    public void Level1()
    {
        SceneManager.LoadScene("Lev1Scene");
    }

    public void Level2()
    {
        SceneManager.LoadScene("Lev2Scene");
    }
    
    public void Level3()
    {
        SceneManager.LoadScene("Lev3Scene");
    }

    public void NameInputButton()
    {
        SceneManager.LoadScene("LevSelecScene");
    }
}
