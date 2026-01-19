using UnityEngine;

public class SetVelocity : MonoBehaviour
{

    private Rigidbody rb;
    public Vector3 linearVelocity;
    public Vector3 angularVelocity;

    public bool useRandomLinearVelocity;
    public bool useRandomAngularVelocity;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();

        if(useRandomLinearVelocity)
        {
            linearVelocity = new Vector3(Random.Range(-5f, 5f), 0f, Random.Range(-5f, 5f));
        }
        else if(useRandomAngularVelocity)
        {
            angularVelocity = new Vector3(0f, Random.Range(-5f, 5f), 0f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        rb.linearVelocity = linearVelocity;
        rb.angularVelocity = angularVelocity;
    }
}
