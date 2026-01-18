using UnityEngine;

public class KinematicMovement
{
    public static SteeringOutput runKinematicSeek(GameObject agent, GameObject target)
    {
        SteeringOutput output = new SteeringOutput();

        // Get a direction to the target
        output.Linear = target.transform.position - agent.transform.position;

        // Normalize and scale wrt speed
        output.Linear.Normalize();
        output.Linear *= agent.GetComponent<AgentController>().speed; 

        // Update orientation
        //output.Angular = updateOrientation(agent, output);
        //faceVelocityDirection(agent, output);
        output.Angular = generateAngularVelocity(agent, output.Linear, agent.GetComponent<Rigidbody>(), agent.GetComponent<AgentController>().rotationSpeed);

        // Draw line in desired direction for debugging
        Debug.DrawLine(agent.transform.position, output.Linear * agent.GetComponent<AgentController>().speed, Color.red);

        return output;
    }

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
}
