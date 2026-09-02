using System.IO;
using UnityEngine;
using System;
using System.Data;
using System.Data.SQLite;
using static Unity.Collections.Unicode;

public class ScoreManager : MonoBehaviour
{
    private string dbPath1;

    private void Start()
    {
        dbPath1="C:/NEA Development/Cricket Game NEA";
    }
    public void SaveScore1(int userId, int score)
    {
        userId = SessionManager.Instance.LoggedInUser;
        
        if (SessionManager.Instance.LoggedInUser>0)
        {
            

            using (var connection = new SQLiteConnection("Data Source=" + dbPath1 + ";Version=3;"))
            {
                connection.Open();

                string query = "INSERT INTO scores (user_id, score ) VALUES (@user_id, @score)";
                using (var command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@user_id", userId);
                    command.Parameters.AddWithValue("@score", score);


                    command.ExecuteNonQuery();
                }

                Debug.Log($"Score Saved For : {userId} with runs : {score}");
            }
        }
        else
        {
            Debug.LogError("User is not Logged in!!");
        }
    }


}
