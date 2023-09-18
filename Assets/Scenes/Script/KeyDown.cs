using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Threading;
using System;
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;
using TMPro;

using static Score;
using static RankingUpdate;

public class KeyDown : MonoBehaviour
{
    public List<string> CurUserName = new List<string>();
    string EventButtonName;
    int NameI = 0;
    int CurLev;
    int CurScore;
    
    public static MySqlConnection SqlConn;
    static string host = "dancingcat-mysql.cmvyrdtvtlv3.ap-northeast-2.rds.amazonaws.com";
    static string user = "root";
    static string password = "20232023";
    static string db = "dancingCat";
    string Conn = string.Format("server={0}; uid={1}; pwd={2}; database={3}; charset=utf8mb4;", host, user, password, db);

    public TextMeshProUGUI NameCur;
    public GameObject NameInput;

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

        CurUserName.Add("_");
        CurUserName.Add("_");
        CurUserName.Add("_");
        CurUserName.Add("_");
        CurUserName.Add("_");
    }

    public void Start()
    {
        CurLev = RankingUpdate.lev;
        CurScore = Score.curScore;
        UnityEngine.Debug.Log("CurLev: " + CurLev + "CurScore: " + CurScore);
    }

    public static DataSet OnSelectRequest(string query, string tableName)
    {
        try
        {
            SqlConn.Open();
            UnityEngine.Debug.Log("sql connect");

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

    public static void OnUpdateRequest(string query, string tableName)
    {
        try
        {
            SqlConn.Open();
            UnityEngine.Debug.Log("sql connect");

            MySqlCommand cmd = new MySqlCommand(query, SqlConn);

            if (cmd.ExecuteNonQuery() == 1)
            {
                UnityEngine.Debug.Log("인서트 성공");
            }
            else
            {
                UnityEngine.Debug.Log("인서트 실패");
            }

            SqlConn.Close();

            SqlConn.Close();
        }

        catch (Exception e)
        {
            UnityEngine.Debug.Log("OnCountRequest: On client connect exception" + e);
        }
    }

    public void GetButtonName()
    {
        EventButtonName = EventSystem.current.currentSelectedGameObject.name;
        UnityEngine.Debug.Log(CurUserName.Count);

        if ((EventButtonName != "Delete") && (EventButtonName != "Enter"))
        {
            if (EventButtonName == "Dash")
            {
                EventButtonName = "-";
            }
            else if (EventButtonName == "Dot")
            {
                EventButtonName = ".";
            }
            else if (EventButtonName == "Snow")
            {
                EventButtonName = "*";
            }
            else if (EventButtonName == "Heart")
            {
                EventButtonName = "♥";
            }
            //CurUserName.Add(EventButtonName);
            CurUserName[NameI] = EventButtonName;
            NameI++;
        }
        else
        {
            if (EventButtonName == "Delete")
            {
                NameI--;
                CurUserName[NameI] = "_";
            }  
            else if (EventButtonName == "Enter")
            {
                char delim = ',';
                string str = String.Join(delim, CurUserName);
                string FullCurName = str.Replace(",", "");
                FullCurName = FullCurName.Replace("_", "");
                FullCurName = '"' + FullCurName + '"';
                UnityEngine.Debug.Log(FullCurName);
                string queryUpdate = string.Format("insert into ranking (user_name, level, score) values ({0}, {1}, {2})", FullCurName, CurLev, CurScore);
                OnUpdateRequest(queryUpdate, "ranking");
                NameInput.SetActive(false);
            }
        }

        //UnityEngine.Debug.Log("name idx: " + NameI);
        //UnityEngine.Debug.Log(CurUserName.Count);
        
        // UnityEngine.Debug.Log(CurUserName[0] + CurUserName[1] + CurUserName[2] + CurUserName[3] + CurUserName[4]);
        string updateTxt = string.Format("NAME: {0} {1} {2} {3} {4}", CurUserName[0], CurUserName[1], CurUserName[2], CurUserName[3], CurUserName[4]);
        NameCur.text = updateTxt;
    }
}