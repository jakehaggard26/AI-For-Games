using UnityEngine;

public class SteeringController : MonoBehaviour
{    
    public SteeringOutput steering;
    public AgentController agent;

    public bool isSeek = true;
    public bool isFlee = false;
    public bool isArrival = false;
    public bool isWander = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = this.GetComponent<AgentController>();
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
            steering = SteeringBehaviors.runSteeringSeek(agent.gameObject, agent.target);
        }
        else if(isFlee)
        {
            steering = SteeringBehaviors.runSteeringFlee(agent.gameObject, agent.target);
        }
        else if(isArrival)
        {
            steering = SteeringBehaviors.runSteeringArrival(agent.gameObject, agent.target);
        }
        else if(isWander)
        {
            steering.Linear = Vector3.zero;
            steering.Angular = Vector3.zero;
            return;
        }
        else
        {
            Debug.Log("No movement algorithm selected.");
            steering.Linear = Vector3.zero;
            steering.Angular = Vector3.zero;
        }

        // Update Linear Velocity
        agent.rb.linearVelocity = steering.Linear * Time.fixedDeltaTime;

        // Update Angular Velocity
        agent.rb.angularVelocity = steering.Angular * Time.fixedDeltaTime;
        
        // Limit speed
        if(steering.Linear.magnitude > agent.maxSpeed)
        {
            steering.Linear.Normalize();
            steering.Linear *= agent.maxSpeed;
        }
    }
}
