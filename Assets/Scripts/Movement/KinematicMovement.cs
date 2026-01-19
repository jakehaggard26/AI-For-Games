using UnityEngine;

public class KinematicMovement
{
    #region Kinematic Movement Algorithms
    public static SteeringOutput runKinematicSeek(GameObject agent, GameObject target)
    {
        SteeringOutput output = new SteeringOutput();

        // Get a direction to the target
        output.Linear = target.transform.position - agent.transform.position;

        // Normalize and scale wrt speed
        output.Linear.Normalize();
        output.Linear *= agent.GetComponent<AgentController>().speed; 

        // Update orientation
        output.Angular = generateAngularVelocity(agent, agent.transform.position + output.Linear, agent.GetComponent<Rigidbody>(), agent.GetComponent<AgentController>().rotationSpeed);

        // Draw line in desired direction for debugging
        Debug.DrawLine(agent.transform.position, agent.transform.position + output.Linear, Color.red);

        return output;
    }

    public static SteeringOutput runKinematicFlee(GameObject agent, GameObject target)
    {
        SteeringOutput output = new SteeringOutput();

        // Get a direction away from the target
        output.Linear = agent.transform.position - target.transform.position;

        // Normalize and scale wrt speed
        output.Linear.Normalize();
        output.Linear *= agent.GetComponent<AgentController>().speed; 

        // Update orientation
        output.Angular = generateAngularVelocity(agent, agent.transform.position + output.Linear, agent.GetComponent<Rigidbody>(), agent.GetComponent<AgentController>().rotationSpeed);

        // Draw line in desired direction for debugging
        Debug.DrawLine(agent.transform.position, agent.transform.position + output.Linear, Color.red);

        return output;
    }

    public static SteeringOutput runKinematicArrival(GameObject agent, GameObject target)
    {
        SteeringOutput output = new SteeringOutput();

        // Calculate direction to target
        output.Linear = target.transform.position - agent.transform.position;

        // Check if within the radius of satisfaction
        if(output.Linear.magnitude < agent.GetComponent<AgentController>().radiusOfSatisfaction)
        {
            output.Linear = Vector3.zero;
            output.Angular = Vector3.zero;
            return output;
        }

        // Calculate time to target
        output.Linear /= agent.GetComponent<AgentController>().timeToTarget;

        // Check if speed is too fast
        if(output.Linear.magnitude > agent.GetComponent<AgentController>().maxSpeed)
        {
            output.Linear.Normalize();
            output.Linear *= agent.GetComponent<AgentController>().maxSpeed;
        }

        // Update orientation
        output.Angular = generateAngularVelocity(agent, agent.transform.position + output.Linear, agent.GetComponent<Rigidbody>(), agent.GetComponent<AgentController>().rotationSpeed);

        // Draw line in desired direction for debugging
        Debug.DrawLine(agent.transform.position, agent.transform.position + output.Linear, Color.red);

        return output;
    }

    public static SteeringOutput runKinematicWander(GameObject agent)
    {
        SteeringOutput output = new SteeringOutput();

        // Move forward at a constant speed
        output.Linear = agent.transform.forward * agent.GetComponent<AgentController>().speed * agent.GetComponent<AgentController>().wanderSpeedBuff;

        // Randomly change orientation
        output.Angular = Vector3.up * generateRandomBinomial() * agent.GetComponent<AgentController>().maxRotation * agent.GetComponent<AgentController>().rotationSpeed;
        Debug.Log("Wander Angular: " + output.Angular);

        Debug.DrawLine(agent.transform.position, agent.transform.position + output.Linear, Color.red);

        return output;
    }
    #endregion

    public static float newOrientation(float current, Vector3 velocity)
    {
        // Check if we have a velocity
        if (velocity.magnitude > 0)
        {
            return Mathf.Atan2(velocity.x, velocity.z) * Mathf.Rad2Deg;
        }
        else
        {
            return current;
        }
    }

    public static Vector3 generateAngularVelocity(GameObject agent, Vector3 target, Rigidbody rb, float rotationSpeed)
    {
        // Get direction to target
        Vector3 directionToTarget = (target - agent.transform.position).normalized;
        
        // Calculate the rotation axis (cross product of forward and desired direction)
        Vector3 rotationAxis = Vector3.Cross(agent.transform.forward, directionToTarget).normalized;
        
        // Calculate rotation speed (magnitude of the cross product gives us the sine of angle)
        float rotationMagnitude = Vector3.Cross(agent.transform.forward, directionToTarget).magnitude;
        
        // Apply angular velocity
        return rotationAxis * rotationMagnitude * rotationSpeed;
    }

    /// <summary>
    /// Generates a random binomial value between -1 and 1.
    /// </summary>
    /// <returns>A random binomial value between -1 and 1 stored in a float.</returns>
    public static float generateRandomBinomial()
    {
        return Random.Range(0f, 1f) - Random.Range(0f, 1f);
    }
}
