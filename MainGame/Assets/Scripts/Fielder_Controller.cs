using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class Fielder_Controller : MonoBehaviour
{
    private SphereCollider sphereCollider;
    private Rigidbody _rb;
    public float FiederSpeed = 10;

    void Start()
    {
        sphereCollider = GetComponent<SphereCollider>();
        _rb = GetComponent<Rigidbody>();
    }

    
    
    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Ball")
        {
            _rb.position = Vector3.MoveTowards(transform.position, other.transform.position, FiederSpeed * Time.deltaTime);

        }
    }
}
