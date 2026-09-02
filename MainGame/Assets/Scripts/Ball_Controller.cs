using UnityEngine;

public class Ball_Controller : MonoBehaviour
{

    Rigidbody rb; // defining rigidbody, Gravity is On
    public float ballSpeed = 5f; // defines ball speed which the ball will be thrown
   
    public ScoreSaver saver; //to update score and save on database
    public int ballTouchedCount = 0; // integer to show how many times ball has touched the ground
    

    private void Awake()
    {
        
        rb = GetComponent<Rigidbody>(); // to keep gravity effect on at all time on object( Ball )
    }
    void Start()
    {
        rb = GetComponent<Rigidbody>(); // to start gravity effect on object( Ball )
        rb.AddForce(Vector3.back * ballSpeed, ForceMode.Impulse);// adds forces to ball

        
        if(saver == null)
        {
            saver = GetComponent<ScoreSaver>(); // stating saver Database at start of the game
        }
    }

  
    private void DestroyBall()
    {
       
        Destroy(this.gameObject);// destroys game object (Ball)
    }

    public void OnTriggerEnter(Collider other) // checks if ball collided with other object with colliders
    {
        if (other.gameObject.CompareTag("Bat")) // check for Tag Bat on object 
        {
            float ballForce = Random.Range(-5f, 5f); // ball collided with bat then ball recieve forces in the range defined
            float ballHeight = Random.Range(0f, 5f); // after ball and bat collides then ball can recieve hright in the range defined 
            float ballPosition = Random.Range(5f, 20f);// afetr ball and bat collides then ball position could be random but in range defined

            Vector3 ballDirection = new Vector3(ballForce, ballHeight, ballPosition); // add 3D forces to new variable
            
            rb.AddForce( ballDirection , ForceMode.Impulse);// uses variable to add force on rigidbody Ball
        }

        if (other.gameObject.CompareTag("Boundary")) // check is ball crossed object tagged boundary 
        {
            if (ballTouchedCount <=2) // check for times ball touched ground
            {
                GameManager.Instance.UpdateRuns(6); // if less then 2 then Six runs would be added to totalruns variable in Game manager
                Debug.Log("six"); // for test to see that 6 runs were issued 
            }
            else
            {
                GameManager.Instance.UpdateRuns(4);// if more then 2 then 4 runs would be added to totalruns variable 
                Debug.Log("Four");// test to see that 4 runs were issued 
            }
            
        }


        if (other.gameObject.CompareTag("Out")) // checks if ball collied with object which has tag of Out
        {
            int finalruns = GameManager.Instance.totalruns;// new variable is defined and that will be equal to totalruns from GameManager

            if (SessionManager.Instance.LoggedInUser > 0) // to get userid when user logs on and checks that user exists 
            {
                if (GameManager.Instance.scoreSaver  != null)
                {
                    GameManager.Instance.scoreSaver.SaveScore( SessionManager.Instance.LoggedInUser, finalruns); // save total runs score in Database under the user who is logged on

                }
                else
                {
                    Debug.LogError(" ScoreSaver is null! Cannot save score."); // if no user logged in then this will pop up
                    
                }
            }
            else
            {
                Debug.LogError("User is not logged in!");// no user id found then this will pop up
            }

            GameManager.Instance.ResetRuns();// to reset Score Board because player got out
            UnityEngine.SceneManagement.SceneManager.LoadScene("Main Game");// returns player to Home Page
            
        }
    }
    public void OnCollisionEnter(Collision collision)// check for collision 
    {
        if (collision.gameObject.CompareTag("Ground"))// check for ball has touched the ground
        {
            ballTouchedCount++; // yes then this variable count will increse by 1 each time ball touchs ground 

        }
    }
}

    
