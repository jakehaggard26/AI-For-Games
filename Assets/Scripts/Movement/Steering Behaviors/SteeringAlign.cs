using UnityEngine;

public class SteeringAlign : SteeringBehavior
{
    public Kinematic agent;
    public Kinematic target;

    public SteeringAlign() : base()
    {
        this.agent = null;
        this.target = null;
    }

    public SteeringAlign(Kinematic agent) : base(agent)
    {
        this.agent = agent;
        this.target = null;
    }

    public SteeringAlign(Kinematic agent, Kinematic target) : base(agent, target)
    {
        this.agent = agent;
        this.target = target;
    }

    /// <summary>
    /// Tries to match the orientation of the character that is the target. This behavior only turns.
    /// </summary>
    /// <param name="agent">A Kinematic object storing the agent's data.</param>
    /// <param name="target">A Kinematic object storing the target's data.</param>
    /// <returns></returns>
    public override SteeringOutput getSteering()
    {
        SteeringOutput output = new SteeringOutput();

        Debug.Log("Running " + nameof(getSteering) + " in Class: " + nameof(SteeringAlign));
        
        float targetRotation = 0f;
        float rotationSize = 0f;
        float rotation = 0f;
        float scale = 0f;
        
        // Get the desired orientation & Wrap the rotation to the range -180 to 180
        rotation = Mathf.DeltaAngle(agent.transform.eulerAngles.y, target.orientation);

        // Convert rotation to radians
        // rotation *= Mathf.Deg2Rad;

        rotationSize = Mathf.Abs(rotation);

        Debug.Log("Rotation: " + rotation);
        Debug.Log("Rotation Size: " + rotationSize);
        Debug.Log("Radius of Satisfaction: " + agent.GetComponent<AgentController>().radiusOfSatisfaction);


        // Check if we are within the radius of satisfaction
        if (rotationSize < agent.GetComponent<AgentController>().radiusOfSatisfaction)
        {
            Debug.Log("Failed first check (In the Radius of Satisfaction)");

            output.Angular = Vector3.zero;
            return output;
        }   

        Debug.Log("Passed first check (Not in the Radius of Satisfaction)");

        // If outside the slow down radius, use max rotation
        if(rotationSize > agent.GetComponent<AgentController>().slowRadius)
        {
            Debug.Log("Failed second check (In the Slow Radius)");

            targetRotation = agent.GetComponent<AgentController>().maxRotation;
        }
        else
        {
            Debug.Log("Passed second check (Not in the Slow Radius)");

            // Otherwise calculate a scaled rotation
            targetRotation = agent.GetComponent<AgentController>().maxRotation * rotationSize / agent.GetComponent<AgentController>().slowRadius;
        }

        // Combine speed and direction
        targetRotation *= rotation / rotationSize;

        // Accerleration tries to get to the target rotation
        output.Angular = new Vector3(0, targetRotation - agent.GetComponent<Rigidbody>().angularVelocity.y, 0);
        output.Angular /= agent.GetComponent<AgentController>().timeToTarget;

        // Check if the acceleration is too fast
        float angularAcceleration = Mathf.Abs(output.Angular.y);
        if (angularAcceleration > agent.GetComponent<AgentController>().maxAngularAcceleration)
        {
            scale = output.Angular.y / angularAcceleration;
            scale *= agent.GetComponent<AgentController>().maxAngularAcceleration;
            output.Angular = new Vector3(0, scale, 0);
        }

        // Draw a line pointing forward
        Debug.DrawLine(agent.transform.position, agent.transform.position + (agent.transform.forward * 5f), Color.red);

        return output;
    }
}
