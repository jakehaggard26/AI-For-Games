using Unity.VisualScripting;
using UnityEngine;

public class SteeringSeek: SteeringBehavior
{
    public SteeringSeek() : base()
    {
        
    }

    public SteeringSeek(Kinematic agent) : base(agent)
    {
        
    }

    public SteeringSeek(Kinematic agent, Kinematic target) : base(agent, target)
    {
        
    }

    public override SteeringOutput getSteering()
    {
        SteeringOutput output = new SteeringOutput();

        Debug.Log("Running " + nameof(getSteering) + " in Class: " + nameof(SteeringSeek));

        // Get the direction to the target
        output.Linear = this.target.position - this.agent.position;

        // Normalize and scale wrt max acceleration
        output.Linear.Normalize();
        output.Linear *= this.agent.GetComponent<AgentController>().maxAcceleration;

        // Draw line in desired direction for debugging
        Debug.DrawLine(agent.position, agent.position + output.Linear, Color.red);

        return output;
    }
}
