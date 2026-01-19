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
}
