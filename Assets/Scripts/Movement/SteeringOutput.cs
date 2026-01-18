using UnityEngine;

public class SteeringOutput
{
    private Vector3 linear;
    private float angular;

    public Vector3 Linear
    {
        get { return linear; }
        set { linear = value; }
    }

    public float Angular
    {
        get { return angular; }
        set { angular = value; }
    }

    public SteeringOutput()
    {
        linear = Vector3.zero;
        angular = 0f;
    }

    public SteeringOutput(Vector3 linear, float angular)
    {
        this.linear = linear;
        this.angular = angular;
    }
}
