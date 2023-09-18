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
    public TextMeshProUGUI RName1, RName2, RName3, RName4;
    public TextMeshProUGUI RScore1, RScore2, RScore3, RScore4;
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
    public static int lev;

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

    public static int OnCountRequest(string query, string tableName)
    {
        int myRank = 0;
        try
        {
            SqlConn.Open();
            UnityEngine.Debug.Log("sql connect");

            MySqlCommand cmd = new MySqlCommand(query, SqlConn);
            // cmd.Connection = SqlConn;
            // cmd.CommandText = query;

            myRank = int.Parse(cmd.ExecuteScalar().ToString());

            SqlConn.Close();

            return myRank;
        }
        catch (Exception e)
        {
            UnityEngine.Debug.Log("OnCountRequest: On client connect exception" + e);

            return -1;
        }
    }

    void select()
    {
        SceneName = SquareImageWithOutline.sceneName;

        if (SceneName == "Lev1Scene")
        {
            lev = 1;
        }
        else if (SceneName == "Lev2Scene")
        {
            lev = 2;
        }
        else if (SceneName == "Lev3Scene")
        {
            lev = 3;
        }
        else {lev = 1;}

        // string query1 = string.Format("select user_name, score from ranking where level = {0} ORDER BY score DESC LIMIT 3", lev);
        string query1 = string.Format("select user_name, score from ranking ORDER BY score DESC LIMIT 3");
        DataSet ds = OnSelectRequest(query1, "ranking");

        int i = 0;
        // ScoreCur = Score.curScore;
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

    int MyRanking(int lev, int scoreCur)
    {
        string query_rank = string.Format("SELECT COUNT(*) + 1 FROM ranking WHERE level = {0} AND score >= {1}", lev, scoreCur);
        UnityEngine.Debug.Log(query_rank);
        int myRank = OnCountRequest(query_rank, "ranking");

        return myRank;
    }

    void Start()
    {
        select();
        ScoreCur = Score.curScore;
        // ScoreCur = 10;

        NameDB.Add(NameCur);
        ScoreDB.Add(ScoreCur);

        for (int i = 0; i < 3; i++)
        {
            // UnityEngine.Debug.Log(ScoreDB[i]);
            if (ScoreCur > ScoreDB[i])
            {
                ScoreDB.Insert(i, ScoreCur);
                NameDB.Insert(i, NameCur);
                break;
            }
        }

        RName1.text = NameDB[0];
        RName2.text = NameDB[1];
        RName3.text = NameDB[2];
        RScore1.text = ScoreDB[0].ToString();
        RScore2.text = ScoreDB[1].ToString();
        RScore3.text = ScoreDB[2].ToString();

        int idx = NameDB.FindIndex(n => n.Contains(NameCur));
        UnityEngine.Debug.Log(idx);

        if (idx == 3)
        {
            int myFRank = MyRanking(lev, ScoreCur);
            RankCur.text = myFRank.ToString();
            RName4.text = NameDB[3];
            RScore4.text = ScoreDB[3].ToString();
            UnityEngine.Debug.Log("my rank is .. " + myFRank);
        }
        else
        {
            UnityEngine.Debug.Log("my rank is ..");
        }

    }

    public void Update()
    {
    }
}
