using UnityEngine;

public class SteeringOutput
{
    private Vector3 linear;
    private Vector3 angular;

    public Vector3 Linear
    {
        get { return linear; }
        set { linear = value; }
    }

    public Vector3 Angular
    {
        get { return angular; }
        set { angular = value; }
    }

    public SteeringOutput()
    {
        linear = Vector3.zero;
        angular = Vector3.zero;
    }

    public SteeringOutput(Vector3 linear, Vector3 angular)
    {
        this.linear = linear;
        this.angular = angular;
    }
}
