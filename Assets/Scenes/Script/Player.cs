using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System;
using System.IO;
using System.Diagnostics;
using System.Threading;
using UnityEngine;
using System.Runtime.InteropServices;
using System.Text;
using System.Data;
using MySql.Data.MySqlClient;

using static SquareImageWithOutline;

public class Player : MonoBehaviour
{
    private Animator animator;

    public static MySqlConnection SqlConn;

    static string host = "dancingcat-mysql.cmvyrdtvtlv3.ap-northeast-2.rds.amazonaws.com";
    static string user = "root";
    static string password = "20232023";
    static string db = "dancingCat";

    string Conn = string.Format("server={0}; uid={1}; pwd={2}; database={3}; charset=utf8mb4;", host, user, password, db);
    // string Conn = string.Format("server=dancingcat-mysql.cmvyrdtvtlv3.ap-northeast-2.rds.amazonaws.com; uid=root; pwd=20232023; database=dancingCat; charset=utf8mb4;");

    // bool isSatisfied = false;
    float timer;
    int watingTime;
    int wave;
    List<int> stimList = new List<int>() {5};

    public static bool gameEnd = false;
    public static bool playGame;

    private void Awake()
    {
        try
        {
            SqlConn = new MySqlConnection(Conn);
        }

        catch (Exception e)
        {
            UnityEngine.Debug.Log("Awake: On client connect exception" + e);
        }
    }

    private void Start()
    {
        animator = GetComponent<Animator>();

        timer = 0.0f;
        watingTime = 6;
        
        gameEnd = false;
        playGame = true;
    }

    public static DataSet OnSelectRequest(string query, string tableName)
    {
        try
        {
            SqlConn.Open();
            // UnityEngine.Debug.Log("sql connect");

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = SqlConn;
            cmd.CommandText = query;

            MySqlDataAdapter sd = new MySqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            sd.Fill(ds, tableName);

            SqlConn.Close();

            return ds;
        }
        catch (Exception e)
        {
            UnityEngine.Debug.Log("OnSelectRequest: On client connect exception" + e);

            return null;
        }
    }

    public void IncreaseScore(int wave)
    {
        if (wave == 0)
        {
            Score.curScore = Score.curScore + 1;
            UnityEngine.Debug.Log("score update");
        }
        else if (wave == 1)
        {
            Score.curScore = Score.curScore + 10;
            UnityEngine.Debug.Log("score update");
        }
        else if (wave == 2)
        {
            Score.curScore = Score.curScore + 100;
            UnityEngine.Debug.Log("score update");
        }
        else if (wave == 3)
        {
            Score.curScore = Score.curScore + 1000;
            UnityEngine.Debug.Log("score update");
        }
    }

    public void GameStatus(int stim)
    {
        gameEnd = false;
        if (stim == 99)
        {
            gameEnd = true;
        }
    }

    private void Update()
    {
        if (SqlConn != null)
        {
            if (timer > watingTime)
            {
                string query = "select class from predictedClass ORDER BY timestamp DESC LIMIT 1";
                DataSet ds = OnSelectRequest(query, "predictedClass");

                int stim = SquareImageWithOutline.currentPosition;
                // UnityEngine.Debug.Log("stimList: " + stimList[stimList.Count -1]);
                /* if (stim == 99) {gameEnd = true;}
                else {gameEnd = false;} */
                GameStatus(stim);

                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    wave = (int)(r["class"]);
                    //UnityEngine.Debug.Log(r["class"].GetType());
                }
                
                UnityEngine.Debug.Log("wave: " + wave + " stim: " + stim);
                if ((wave == stim) && (stim != stimList[stimList.Count -1]))
                {
                    if (wave == 0)
                    {
                        //PerformAnimationAndIncreaseScore("doJump");
                        animator.SetTrigger("doJump");
                        UnityEngine.Debug.Log("doJump + 1");
                    }
                    else if (wave == 1)
                    {
                        //PerformAnimationAndIncreaseScore("doHi");
                        animator.SetTrigger("doHi");
                        UnityEngine.Debug.Log("doHi + 10");
                    }
                    else if (wave == 2)
                    {
                        //PerformAnimationAndIncreaseScore("doSit");
                        animator.SetTrigger("doSit");
                        UnityEngine.Debug.Log("doSit + 100");
                    }
                    else if (wave == 3)
                    {
                        //PerformAnimationAndIncreaseScore("isVictory");
                        animator.SetTrigger("isVictory");
                        UnityEngine.Debug.Log("isVictory + 1000");
                    }
                    IncreaseScore(wave);
                    //StartCoroutine("Delay", wave);
                    playGame = false;
                }
                timer = 0;
                stimList.Add(stim);
                //isSatisfied = false;
            }
            else
            {
                timer += Time.deltaTime;
            }
        }
    }

    IEnumerator Delay(int wave)
    {
        yield return new WaitForSeconds(2f);
        IncreaseScore(wave); 
    }
}

