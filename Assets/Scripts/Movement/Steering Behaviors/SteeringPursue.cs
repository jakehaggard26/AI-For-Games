using UnityEngine;

public class SteeringPursue : SteeringBehavior
{
    public SteeringPursue() : base()
    {
        this.agent = null;
        this.target = null;
    }

    public SteeringPursue(Kinematic agent) : base(agent)
    {
        this.agent = agent;
        this.target = null;
    }

    public SteeringPursue(Kinematic agent, Kinematic target) : base(agent, target)
    {
        this.agent = agent;
        this.target = target;
    }

    public override SteeringOutput getSteering()
    {
        SteeringOutput output = new SteeringOutput();

        Debug.Log("Running " + nameof(getSteering) + " in Class: " + nameof(SteeringPursue));

        Vector3 direction = target.transform.position - agent.transform.position;
        float distance = direction.magnitude;

        // Work out speed
        float speed = agent.GetComponent<Rigidbody>().linearVelocity.magnitude;

        float prediction;

        // Check if speed allows for a reasonable prediction
        if(speed <= distance / agent.GetComponent<AgentController>().maxPrediction)
        {
            prediction = agent.GetComponent<AgentController>().maxPrediction;
        }
        else
        {
            prediction = distance / speed;
        }

        // Prep target for Seek
        output.Linear = target.transform.position + (target.GetComponent<Rigidbody>().linearVelocity * prediction);

        // Delegate to Seek or Arrival
        Kinematic newTarget = new Kinematic();
        newTarget.position = output.Linear;
        output = new SteeringSeek(agent, newTarget).getSteering();
        // output = new SteeringArrival(agent, newTarget).getSteering();

        return output;
    }
}
