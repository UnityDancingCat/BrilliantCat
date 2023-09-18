using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //Text
using TMPro;

public class Score : MonoBehaviour
{
    public TextMeshProUGUI CurScore;

    public static int curScore;
    //public static Score instance;

    // void Awake()
    // {
    //     instance=this;
    // }
    
    void Start()
    {
        curScore = 0;
        //FixedUpdateScore();
    }

    public void Update()
    {
        CurScore.text = "Score : " + curScore;
    }
}
