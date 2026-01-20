using UnityEngine;

public class SteeringBehavior
{
    public static SteeringOutput getSteering()
    {
        SteeringOutput output = new SteeringOutput();

        Debug.Log("Running " + nameof(getSteering) + " in Class: " + nameof(SteeringBehavior));

        return output;
    }
}
