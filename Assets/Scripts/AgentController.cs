using UnityEngine;

public class AgentController : MonoBehaviour
{
    public float speed = 5f;
    public float maxSpeed = 7.5f;
    public float rotationSpeed = 200f;
    public float maxRotation = 60f;

    public float radiusOfSatisfaction = 2f;
    public float timeToTarget = 0.25f;

    public float wanderSpeedBuff = 10f;

    private float orientation;

    public GameObject target;
    public SteeringOutput steering;
    public Rigidbody rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
