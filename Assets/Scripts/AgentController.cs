using UnityEngine;

public class AgentController : MonoBehaviour
{
    public float speed = 5f;
    public float maxSpeed = 7.5f;
    public float maxAcceleration = 15f;
    public float maxAngularAcceleration = 15f;
    public float rotationSpeed = 200f;
    public float maxRotation = 60f;

    [Tooltip("How far away the circle is for choosing a random spot to seek.")]
    public float wanderOffset = 1f;

    [Tooltip("How big the circle is for choosing a random spot to seek.")]
    public float wanderRadius = 5f;
    [Tooltip("Current orientation of the wander target.")]
    public float wanderOrientation = 0f;
    [Tooltip("The max rate at which the wander orientation can change.")]
    public float wanderRate = 1f;

    public float radiusOfSatisfaction = 2f;
    public float slowRadius = 5f;
    [Tooltip("Time in seconds to achieve target speed. Higher values result in a more gentle decceleration. Lower values result in a more abrupt decceleration.")]
    public float timeToTarget = 0.25f;

    public float wanderSpeedBuff = 10f;

    public float maxPrediction = 1.5f;

    private float orientation;

    public GameObject target;
    public SteeringOutput steering;
    public Rigidbody rb;
    public GameObject tempTarget;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();   
        tempTarget = new GameObject();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
