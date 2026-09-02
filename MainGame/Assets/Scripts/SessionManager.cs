using UnityEngine;

public class SessionManager : MonoBehaviour
{
    public static SessionManager Instance { get; private set; }// to be exccessed from other scripts
    public int LoggedInUser { get; private set; } // Store logged-in user ID
   
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Make this object persist across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Set the logged-in user ID (this would be set upon successful login)
    public void SetUserId(int userId)
    {
        LoggedInUser = userId;
    }

  
}

