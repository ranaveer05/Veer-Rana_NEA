using UnityEngine;
using System.Data.SQLite;
using System.IO;
using System.Transactions;
using UnityEngine.UI;
using System;

public class PointsManager : MonoBehaviour
{
    
    private string dbPath;
    private string FolderPath = "C:/NEA Development/Cricket Game NEA";//db location
    private int loggedInUser;// variable 
    private int ballTouchedCount=0;// ball bounces to ground
    private int totalPoints = 0;// total points 

    public Text pointsText;// to display points 

   

    void Start()
    {
        dbPath = Path.Combine(FolderPath, "MainUsersDB.sqlite");// assiging db to db location
        loggedInUser = SessionManager.Instance.LoggedInUser;// setting variable to logged in user id 
        ldtotalPoints();// loads total points 
    }
    private void Update()
    {
        if (pointsText != null)
        {
            pointsText.text = $"Total Points: {totalPoints}";// shows points at all time not only when starting
        }
    }


    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))// check if ball has collided with "Ground"
        {
            // The ball is on the ground, mark it
            ballTouchedCount ++ ;// adds up count every time ball is in contact 
            Debug.Log("Ball Touched ground " + ballTouchedCount);// shows contact
        }

       
    }
    void OnTriggerEnter(Collider other) // Using Trigger for Boundary Detection
    {
        if (other.gameObject.CompareTag("Boundary"))
        {
            Debug.Log("Ball crossed the boundary!");
            AddPoints();  // Award points when ball crosses boundary
            ballTouchedCount = 0;
        }
    }




    public void AddPoints()
    {
        int points = 0;



        if (ballTouchedCount ==2)// conditon for 6
        {
            points = 2;//if 6 is hit then 2 point will be added 
            Debug.Log("six and points added: "+ points);
        }
        else if  (ballTouchedCount > 2)// condition for 4
            {
                 points = 1;// if 4 then 1 point will be added 
                Debug.Log("four and points added " + points);
            }
        
    

        if (points > 0)// saves point under user id logged in 
            {
                using(var connection = new SQLiteConnection("Data Source=" + dbPath + ";Version=3;"))
                {
                    connection.Open();

                    using(var updatepoints = new SQLiteCommand($"UPDATE users SET points = points + {points} WHERE id = {loggedInUser}", connection))
                    {
                        updatepoints.ExecuteNonQuery();
                    }
                    totalPoints += points;
                string cntTime = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                    string addPoints = $"INSERT INTO transactions (user_id, points_added, points_removed, transaction_date) " +// insert points earned into table called transaction
                    $"VALUES ({loggedInUser}, {points}, 0, '{cntTime}')";
                    using (var transactionCommand = new SQLiteCommand(addPoints, connection)) 
                    {
                        transactionCommand.ExecuteNonQuery();   
                    }
                    
                }
                
                Debug.Log($"{points} points have been added to your wallet!");
            }
    }
    private void ldtotalPoints()// load total points 
    {
        using (var connection = new SQLiteConnection("Data Source=" + dbPath + ";Version=3;")) 
        {
            connection.Open();

            using (var command = new SQLiteCommand($"SELECT points FROM users WHERE id = {loggedInUser}", connection))
            {
                var result = command.ExecuteScalar();

                if (result != null )
                {
                    totalPoints = int.Parse(result.ToString());// converts into string
                }
            }
        }

    }
    
    
}
