using UnityEngine;

public class AgentController : MonoBehaviour
{
    public float speed = 5f;
    public float maxSpeed = 7.5f;
    public float rotationSpeed = 200f;

    public float radiusOfSatisfaction = 2f;
    public float timeToTarget = 0.25f;

    private float orientation;

    public GameObject target;
    public SteeringOutput steering;
    public Rigidbody rb;

    public bool isSeek = true;
    public bool isFlee = false;
    public bool isArrival = false;
    public bool isWander = false;

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
        steering = new SteeringOutput();
        
        // Store outputs based on selected algorithm
        if(isSeek)
        {
            steering = KinematicMovement.runKinematicSeek(this.gameObject, target);
        }
        else if(isFlee)
        {
            steering = KinematicMovement.runKinematicFlee(this.gameObject, target);
        }
        else if(isArrival)
        {
            steering = KinematicMovement.runKinematicArrival(this.gameObject, target);
        }
        else if(isWander)
        {
            return;
        }
        else
        {
            Debug.Log("No movement algorithm selected.");
            steering.Linear = Vector3.zero;
            steering.Angular = Vector3.zero;
        }
        

        // Update Linear Velocity
        rb.linearVelocity = steering.Linear * Time.fixedDeltaTime;

        // Update Angular Velocity
        rb.angularVelocity = steering.Angular * Time.fixedDeltaTime;
    }
}
