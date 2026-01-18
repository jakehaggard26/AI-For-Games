using UnityEngine;

public class KinematicMovement
{
    public static SteeringOutput runKinematicSeek()
    {
        SteeringOutput output = new SteeringOutput();

        

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
}
