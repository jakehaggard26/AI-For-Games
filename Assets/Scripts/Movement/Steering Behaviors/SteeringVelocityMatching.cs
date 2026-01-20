using UnityEngine;

public class SteeringVelocityMatching : SteeringBehavior
{
    public Kinematic agent;
    public Kinematic target;

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

        return output;
    }
}
