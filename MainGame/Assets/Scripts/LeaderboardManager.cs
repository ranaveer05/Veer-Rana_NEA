using System;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardManager : MonoBehaviour
{
    public Text leaderboardtext;
    private string dbPath;
    private string FolderPath = "C:/NEA Development/Cricket Game NEA";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dbPath = Path.Combine(FolderPath, "MainUsersDB.sqlite");// assigns path to and from database location
        UpdateLeaderboard();
    }
    public void UpdateLeaderboard()// updates code when ever there is high score entry
    {
        using (var  dbconnection = new SQLiteConnection("Data Source=" + dbPath + ";Version=3;"))// connection to database
        {
            dbconnection.Open();// keeps open for to access data 
            string findScore = "SELECT username, score FROM scores ORDER BY Score DESC LIMIT 10;";
            // selects username ans score form scores table and displays 10 entries in decesnding order 
            using (var command = new SQLiteCommand(findScore, dbconnection))// find scores find data from database connected
                using (var dataReader = command.ExecuteReader())
            {
                int position = 1;
                if (!dataReader.HasRows)
                {
                    leaderboardtext.text += "No Score available";// if there arent any data then this will pop up
                    return;
                }
                string leaderboard = "Top 10 Scores - ";
                while (dataReader.Read() && position <= 10)// increments position by 1 every cycle 
                {
                    string playerName = dataReader["username"].ToString();// usernmae is assigned to variable 
                    int playerScore = Convert.ToInt32(dataReader["score"]);// score is assigned to variable 
                    Debug.Log($"Player: {playerName}, Score: {playerScore}");// diaplays table in console menu
                    leaderboard += $"{position}. {playerName} - {playerScore}\n";// save table in format needed 
                    position++;// position increment by 1 
                }
                leaderboardtext.text = leaderboard;// uses text UI to show the results.
            }
        }
    }
}
