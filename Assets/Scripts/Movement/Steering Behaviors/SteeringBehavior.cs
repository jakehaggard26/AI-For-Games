using UnityEngine;

public class SteeringBehavior
{
    public Kinematic agent;
    public Kinematic target;

    public SteeringBehavior()
    {
        this.agent = null;
        this.target = null;
    }

    public SteeringBehavior(Kinematic agent)
    {
        this.agent = agent;
        this.target = null;
    }

    public SteeringBehavior(Kinematic agent, Kinematic target)
    {
        this.agent = agent;
        this.target = target;
    }

    public virtual SteeringOutput getSteering()
    {
        SteeringOutput output = new SteeringOutput();

        Debug.Log("Running " + nameof(getSteering) + " in Class: " + nameof(SteeringBehavior));

        return output;
    }
    public virtual SteeringOutput getSteering(Kinematic agent)
    {
        SteeringOutput output = new SteeringOutput();

        Debug.Log("Running " + nameof(getSteering) + " in Class: " + nameof(SteeringBehavior));

        return output;
    }
    public virtual SteeringOutput getSteering(Kinematic agent, Kinematic target)
    {
        SteeringOutput output = new SteeringOutput();

        Debug.Log("Running " + nameof(getSteering) + " in Class: " + nameof(SteeringBehavior));

        return output;
    }
}
