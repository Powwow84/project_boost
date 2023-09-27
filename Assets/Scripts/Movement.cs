using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rb;
    AudioSource sfx;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] float thrustVar = 100f;
    [SerializeField] float rotationSpeed = 1f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        sfx = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            rb.AddRelativeForce(Vector3.up * Time.deltaTime * thrustVar);
            if(!sfx.isPlaying)
            {
                sfx.PlayOneShot(mainEngine);
            }
        }
        else
        {
            if(sfx.isPlaying)
            {
                sfx.Pause();
            }
        }
    }

    void ProcessRotation()
    {
        if(Input.GetKey(KeyCode.A))
        {
            Rotate(rotationSpeed);
        }
        else if(Input.GetKey(KeyCode.D)) 
        {
            Rotate(-rotationSpeed);
        }
    }

    private void Rotate(float rotationThisFrame)
    {
        rb.freezeRotation = true; // freezing rotation so we can manually rotate
        transform.Rotate(Vector3.forward * Time.deltaTime * rotationThisFrame);
        rb.freezeRotation = false; // unfreeze the rotation so physics can take back over
    }
}
