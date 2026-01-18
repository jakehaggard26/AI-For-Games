using UnityEngine;

public class AgentController : MonoBehaviour
{
    public float speed = 5f;
    public float rotationSpeed = 200f;

    private float orientation;

    public GameObject target;
    public SteeringOutput steering;
    public Rigidbody rb;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        // Store outputs
        steering = KinematicMovement.runKinematicSeek(this.gameObject, target);

        // Update Linear Velocity
        rb.linearVelocity = steering.Linear * Time.fixedDeltaTime;

        // Update Angular Velocity
        rb.angularVelocity = steering.Angular * Time.fixedDeltaTime;
    }
}
