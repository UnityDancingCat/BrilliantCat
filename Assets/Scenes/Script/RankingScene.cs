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

public class RankingScene : MonoBehaviour
{
    public TextMeshProUGUI RName1, RName2, RName3, RName4, RName5;
    public TextMeshProUGUI RScore1, RScore2, RScore3, RScore4, RScore5;
    public TextMeshProUGUI RLv1, RLv2, RLv3, RLv4, RLv5;
    // public TextMeshProUGUI RankCur;

    //public string Name1, Name2, Name3;
    public List<string> NameDB = new List<string>();
    // public int Score1, Score2, Score3
    public List<int> ScoreDB = new List<int>();
    //public static Score instance;
    public List<int> LevelDB = new List<int>();

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
        // string query1 = string.Format("select user_name, score from ranking where level = {0} ORDER BY score DESC LIMIT 3", lev);
        string query1 = string.Format("select user_name, score, level from ranking ORDER BY score DESC LIMIT 5");
        DataSet ds = OnSelectRequest(query1, "ranking");

        int i = 0;
        // ScoreCur = Score.curScore;
        foreach (DataRow r in ds.Tables[0].Rows)
            {
                NameDB.Add((string)(r["user_name"]));
                ScoreDB.Add((int)(r["score"]));
                LevelDB.Add((int)(r["level"]));
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

        RName1.text = NameDB[0];
        RName2.text = NameDB[1];
        RName3.text = NameDB[2];
        RName4.text = NameDB[2];
        RName5.text = NameDB[3];
        RScore1.text = ScoreDB[0].ToString();
        RScore2.text = ScoreDB[1].ToString();
        RScore3.text = ScoreDB[2].ToString();
        RScore4.text = ScoreDB[3].ToString();
        RScore5.text = ScoreDB[4].ToString();
        RLv1.text = LevelDB[0].ToString();
        RLv2.text = LevelDB[1].ToString();
        RLv3.text = LevelDB[2].ToString();
        RLv4.text = LevelDB[3].ToString();
        RLv5.text = LevelDB[4].ToString();
    }

    public void Update()
    {
    }
}
