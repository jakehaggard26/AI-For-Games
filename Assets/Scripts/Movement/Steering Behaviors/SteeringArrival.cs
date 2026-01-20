using UnityEngine;

public class SteeringArrival : SteeringBehavior
{
    public Kinematic agent;
    public Kinematic target;

    public SteeringArrival() : base()
    {
        
    }

    public SteeringArrival(Kinematic agent) : base(agent)
    {
        
    }

    public SteeringArrival(Kinematic agent, Kinematic target) : base(agent, target)
    {
        
    }
    public override SteeringOutput getSteering()
    {
        SteeringOutput output = new SteeringOutput();

        Debug.Log("Running " + nameof(getSteering) + " in Class: " + nameof(SteeringArrival));

        float targetSpeed = 0f;

        // Get the direction to the target
        Vector3 direction = target.position - agent.position;
        float distance = direction.magnitude;

        Debug.Log(distance);

        // Check if we are within the radius of satisfaction
        if (distance < agent.GetComponent<AgentController>().radiusOfSatisfaction)
        {
            output.Linear = Vector3.zero;
            return output;
        }

        // Check if we are within the slow radius
        if(distance > agent.GetComponent<AgentController>().slowRadius)
        {
            targetSpeed = agent.GetComponent<AgentController>().maxSpeed;
        }
        else
        {
            // Calculate a scaled speed
            targetSpeed = agent.GetComponent<AgentController>().maxSpeed * distance / agent.GetComponent<AgentController>().slowRadius;
        }

        // Target velocity combines speed and direction
        Vector3 targetVelocity = direction;
        targetVelocity.Normalize();
        targetVelocity *= targetSpeed;

        // Acceleration tries to get to the target velocity
        output.Linear = targetVelocity - agent.velocity;
        output.Linear /= agent.GetComponent<AgentController>().timeToTarget;

        // Check if the acceleration is too fast
        if (output.Linear.magnitude > agent.GetComponent<AgentController>().maxAcceleration)
        {
            output.Linear.Normalize();
            output.Linear *= agent.GetComponent<AgentController>().maxAcceleration;
        }

        output.Angular = Vector3.zero;

        Debug.DrawLine(agent.transform.position, agent.transform.position + output.Linear, Color.red);

        return output;
    }
}
