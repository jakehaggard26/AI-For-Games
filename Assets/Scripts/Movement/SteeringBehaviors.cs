using System.ComponentModel.Design;
using UnityEngine;

public class SteeringBehaviors
{
    public static SteeringOutput runSteeringSeek(GameObject agent, GameObject target)
    {
        SteeringOutput output = new SteeringOutput();

        // Get the direction to the target
        output.Linear = target.transform.position - agent.transform.position;

        // Normalize and scale wrt max acceleration
        output.Linear.Normalize();
        output.Linear *= agent.GetComponent<AgentController>().maxAcceleration;

        // Draw line in desired direction for debugging
        Debug.DrawLine(agent.transform.position, agent.transform.position + output.Linear, Color.red);

        return output;
    }
    public static SteeringOutput runSteeringFlee(GameObject agent, GameObject target)
    {
        SteeringOutput output = new SteeringOutput();

        // Get the direction to the target
        output.Linear = agent.transform.position - target.transform.position;

        // Normalize and scale wrt max acceleration
        output.Linear.Normalize();
        output.Linear *= agent.GetComponent<AgentController>().maxAcceleration;

        // Draw line in desired direction for debugging
        Debug.DrawLine(agent.transform.position, agent.transform.position + output.Linear, Color.red);

        return output;
    }

    public static SteeringOutput runSteeringArrival(GameObject agent, GameObject target)
    {
        SteeringOutput output = new SteeringOutput();

        float targetSpeed = 0f;

        // Get the direction to the target
        output.Linear = target.transform.position - agent.transform.position;
        float distance = output.Linear.magnitude;

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
        Vector3 targetVelocity = output.Linear;
        targetVelocity.Normalize();
        targetVelocity *= targetSpeed;

        // Acceleration tries to get to the target velocity
        output.Linear = targetVelocity - agent.GetComponent<Rigidbody>().linearVelocity;
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
