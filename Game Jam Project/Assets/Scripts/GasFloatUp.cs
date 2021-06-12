using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasFloatUp : MonoBehaviour
{
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector3(0f, 5f, 0f);
    }
}
