using System.Diagnostics.Contracts;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    private SQLiteManager sqlitemanager; // connect to database

    public ScoreSaver scoreSaver; // for saving score in database

    public static GameManager Instance; // links this script to other script 
    public GameObject ballPrefab; // in game ball object which is independent to move
    public GameObject CurrentBall; // shows loaded ball 
    
    public Vector3 ballSpawnPosition; // helps state spawn location of ball in 3D as the game is 3D

    public Text Runtext; // Shows how much runs the player has scored
    
    public int totalruns; // to calculate total runs scored 

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // does not destory logged in user - user will be logged on in every scene
        }
        else
        {
            Destroy(gameObject);
        }
        UpdateRuns(0);// updates score to 0 runs so then when code is running the variable is defined
        
    }

    void Start()
    {
        sqlitemanager = FindFirstObjectByType<SQLiteManager>(); // linking variable and SQLite database Manager
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) // when pressed P on keyboard the ball will be thrown
        {
            ThrowBall(); // function to throw ball
        }
    }
    public void ThrowBall()
    {
        Instantiate(ballPrefab, ballSpawnPosition, Quaternion.identity); // throws ball from the ball spawn position
    }
    public void UpdateRuns(int Score) // when run is scored, the run score is held in this and adds up
    {
        
        totalruns =totalruns + Score;
        Runtext.text = "Score  - " + totalruns; // to display and update total runs scored from player 
    }
    public void ResetRuns()
    {
        totalruns = 0; // reset runs to 0 when player is Out

    }

 }
