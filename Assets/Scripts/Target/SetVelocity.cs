using UnityEngine;

public class SetVelocity : MonoBehaviour
{

    private Rigidbody rb;
    public Vector3 linearVelocity;
    public Vector3 angularVelocity;

    public bool useRandomLinearVelocity;
    public bool useRandomAngularVelocity;

    [SerializeField] private float minX;
    [SerializeField] private float maxX;

    [SerializeField] private float minZ;
    [SerializeField] private float maxZ;

    [SerializeField] private float minY;
    [SerializeField] private float maxY;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();

        if(useRandomLinearVelocity)
        {
            linearVelocity = new Vector3(Random.Range(minX, maxX), 0f, Random.Range(minZ, maxZ));
        }
        else
        {
            linearVelocity = Vector3.zero;
        }
        
        if(useRandomAngularVelocity)
        {
            angularVelocity = new Vector3(0f, Random.Range(minY, maxY), 0f);
        }
        else
        {
            angularVelocity = Vector3.zero;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(useRandomAngularVelocity || useRandomLinearVelocity)
        {
            rb.linearVelocity = linearVelocity;
            rb.angularVelocity = angularVelocity;
        }
    }
}
