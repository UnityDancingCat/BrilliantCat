using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using TMPro;
using System;
using System.Threading;
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;

using static Score;
using static SquareImageWithOutline;

public class RankingUpdate : MonoBehaviour
{
    public TextMeshProUGUI RName1, RName2, RName3, RNameCur;
    public TextMeshProUGUI RScore1, RScore2, RScore3, RScoreCur;
    public TextMeshProUGUI RankCur;

    //public string Name1, Name2, Name3;
    public List<string> NameDB = new List<string>();
    string NameCur = "YOU!";
    public int ScoreCur = 0;
    // public int Score1, Score2, Score3
    public List<int> ScoreDB = new List<int>();
    public int RankCurrent;
    //public static Score instance;

    public string SceneName;
    public int lev;

    public static MySqlConnection SqlConn;

    static string host = "dancingcat-mysql.cmvyrdtvtlv3.ap-northeast-2.rds.amazonaws.com";
    static string user = "root";
    static string password = "20232023";
    static string db = "dancingCat";

    string Conn = string.Format("server={0}; uid={1}; pwd={2}; database={3}; charset=utf8mb4;", host, user, password, db);
    // string Conn = string.Format("server=dancingcat-mysql.cmvyrdtvtlv3.ap-northeast-2.rds.amazonaws.com; uid=root; pwd=20232023; database=dancingCat; charset=utf8mb4;");

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

    void select()
    {
        SceneName = SquareImageWithOutline.sceneName;

        if (sceneName == "Lev1Scene")
        {
            lev = 1;
        }
        else if (sceneName == "Lev2Scene")
        {
            lev = 2;
        }
        else if (sceneName == "Lev3Scene")
        {
            lev = 3;
        }
        else {lev = 0;}

        // string query1 = string.Format("select user_name, score from ranking where level = {0} ORDER BY score DESC LIMIT 3", lev);
        string query1 = string.Format("select user_name, score from ranking ORDER BY score DESC LIMIT 3");
        DataSet ds = OnSelectRequest(query1, "ranking");

        int i = 0;
        ScoreCur = Score.CatScore;
        foreach (DataRow r in ds.Tables[0].Rows)
            {
                NameDB.Add((string)(r["user_name"]));
                ScoreDB.Add((int)(r["score"]));
                // Name1 = (string)(r["user_name"]);
                // Score1 = (int)(r["score"]);
                //UnityEngine.Debug.Log(r["user_name"].GetType());
                //UnityEngine.Debug.Log(r["score"].GetType());
                UnityEngine.Debug.Log(NameDB[i] + ScoreDB[i]);
                i++;
            }
    }

    void Start()
    {
        select();
        ScoreCur = Score.CatScore;

        for (int i = 0; i < 3; i++)
        {
            if (ScoreCur > ScoreDB[i])
            {
                ScoreDB.Insert(i, ScoreCur);
                NameDB.Insert(i, NameCur);
            }
        }

        RName1.text = NameDB[0];
        RName2.text = NameDB[1];
        RName3.text = NameDB[2];
        RScore1.text = ScoreDB[0].ToString();
        RScore2.text = ScoreDB[1].ToString();
        RScore3.text = ScoreDB[2].ToString();

        int idx = NameDB.FindIndex(n => n.Contains("You!"));
        UnityEngine.Debug.Log(idx);

        

    }

    public void Update()
    {
    }
}
