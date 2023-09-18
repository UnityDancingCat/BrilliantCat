using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using static Score;
using static Player;
using static TutorialText;

public class GameManagerForTutorial : MonoBehaviour
{
    public bool tutorialEnd = false;

    void Update()
    {
        tutorialEnd = TutorialText.allDialEnd;

        if (tutorialEnd == true)
        {
            SceneManager.LoadScene("StartScene", LoadSceneMode.Single);
        }
    }
}
