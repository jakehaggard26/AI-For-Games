using UnityEngine;

/// <summary>
/// A data structure to hold kinematic movement information.
/// </summary>
public class Statics
{
    public Vector3 velocity;
    public float angularVelocity;

    public Statics()
    {
        velocity = Vector3.zero;
        angularVelocity = 0f;
    }

    public Statics(Vector3 velocity, float angularVelocity)
    {
        this.velocity = velocity;
        this.angularVelocity = angularVelocity;
    }
}