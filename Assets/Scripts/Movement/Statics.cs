using UnityEngine;

/// <summary>
/// A data structure to hold kinematic movement information.
/// 
/// As of 1/18/2026, this class is not being used. More or less for reference of what static data is.
/// </summary>
public class Statics
{
    public Vector3 position;
    public float orientation; // in degrees
    public Vector3 velocity;
    public float rotation; // in degrees per second

    public Statics()
    {
        position = Vector3.zero;
        orientation = 0f;
        velocity = Vector3.zero;
        rotation = 0f;
    }

    public Statics(Vector3 position, float orientation, Vector3 velocity, float rotation)
    {
        this.position = position;
        this.orientation = orientation;
        this.velocity = velocity;
        this.rotation = rotation;
    }
}