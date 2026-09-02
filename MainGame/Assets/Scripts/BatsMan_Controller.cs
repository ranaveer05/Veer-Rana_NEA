using System.Collections;
using UnityEngine;

public class BatsMan_Controller : MonoBehaviour
{
     Animator playerAnimator;// animation for bat to hit ball
    [SerializeField] BoxCollider batCollider;// box collider for bat

    public void Start()
    {
        
        playerAnimator = GetComponent<Animator>();// defines aniamation at strat 
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) ) // when space bar is hit then the animation will run
        {
            playerAnimator.SetBool("Shot", true);
            StartCoroutine(BatsmanShot());// calls BatsmanShot
        }
    }
    IEnumerator BatsmanShot()
    {
        playerAnimator.SetBool("Shot", true);
        batCollider.enabled = true; // bat collider turns on to hit the ball only while in animation
        yield return new WaitForSeconds(0.5f);// the box collider stay on for 0.5 seconds.

        playerAnimator.SetBool("Shot", false);
        batCollider.enabled = false; // bat collider truns off 
    }
}
