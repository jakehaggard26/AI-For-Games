using UnityEditor.EditorTools;
using UnityEngine;

public class Kinematic : MonoBehaviour
{
    [Tooltip("Where the agent is located in the world.")]
    public Vector3 position;

    [Tooltip("The direction the agent is facing")]
    public float orientation;

    [Tooltip("The linear velocity of the agent")]
    public Vector3 velocity;

    [Tooltip("The angular velocity of the agent")]
    public Vector3 rotation;

    public Rigidbody rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(this.GetComponent<Rigidbody>())
        {
            rb = this.GetComponent<Rigidbody>();
        }
        else
        {
            rb = null;
        }
            
    }

    // Update is called once per frame
    void Update()
    {
        // Update Kinematic values
        position = this.transform.position;
        orientation = transform.eulerAngles.y; 
        
        if(rb)
        {
            velocity = rb.linearVelocity;
            rotation = rb.angularVelocity;
        }
        else
        {
            velocity = Vector3.zero;
            rotation = Vector3.zero;
        }
        
    }
}
