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

    public static SteeringOutput runSteeringAlign(GameObject agent, GameObject target)
    {
        SteeringOutput output = new SteeringOutput();
        float targetRotation = 0f;
        float rotationSize = 0f;
        float rotation = 0f;
        float scale = 0f;
        
        // Get the desired orientation & Wrap the rotation to the range -180 to 180
        rotation = Mathf.DeltaAngle(agent.transform.eulerAngles.y, target.transform.eulerAngles.y);

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
