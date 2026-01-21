using UnityEngine;

public class SteeringVelocityMatching : SteeringBehavior
{
    public SteeringVelocityMatching() : base()
    {
        this.agent = null;
        this.target = null;
    }

    public SteeringVelocityMatching(Kinematic agent) : base(agent)
    {
        this.agent = agent;
        this.target = null;
    }

    public SteeringVelocityMatching(Kinematic agent, Kinematic target) : base(agent, target)
    {
        this.agent = agent;
        this.target = target;
    }

    public override SteeringOutput getSteering()
    {
        SteeringOutput output = new SteeringOutput();

        Debug.Log("Running " + nameof(getSteering) + " in Class: " + nameof(SteeringAlign));

        Debug.Log("Agent Velocity: " + agent.GetComponent<Rigidbody>().linearVelocity + "\nTarget Velocity: " + target.GetComponent<Rigidbody>().linearVelocity);
        
        // Gets target velocity
        output.Linear = target.velocity - agent.velocity;
        Debug.Log("Target Velocity - Agent Velocity: " + output.Linear);
        output.Linear /= agent.GetComponent<AgentController>().timeToTarget;
        Debug.Log("Scaled Target Velocity: " + output.Linear);

        Debug.Log("Agent acceleration: " + output.Linear.magnitude + " | Max Acceleration: " + agent.GetComponent<AgentController>().maxAcceleration);

        // Limit acceleration if going too fast
        if(output.Linear.magnitude > agent.GetComponent<AgentController>().maxAcceleration)
        {
            Debug.Log("Going to fast");
            output.Linear.Normalize();
            output.Linear *= agent.GetComponent<AgentController>().maxAcceleration;
            Debug.Log("Updated Velocity: " + output.Linear);
        }

        Debug.DrawLine(agent.transform.position, output.Linear, Color.red);


        return output;
    }
}
