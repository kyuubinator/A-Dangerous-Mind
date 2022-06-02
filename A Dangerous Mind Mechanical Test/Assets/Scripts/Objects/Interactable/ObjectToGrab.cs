using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectToGrab : MonoBehaviour, IGrabable
{
    private Transform newPosition;
    private bool grabbed;
    private Rigidbody rb;
    private BoxCollider col;
    private float cd;
    [SerializeField] private bool locked;
        
       
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<BoxCollider>();
    }

    private void FixedUpdate()
    {
        if (grabbed)
        {
            transform.position = newPosition.position;
            transform.rotation = newPosition.rotation;

        }
        else
            return;
        rb.velocity = Vector3.zero;
    }

    public void Grab(Transform grabposition)
    {
        if (!grabbed)
        {
            newPosition = grabposition;
            grabbed = true;
        }
        else
        {
            grabbed = false;
        }
    }

    public void DeactivateCol()
    {
        col.isTrigger = true;
    }

    public void ActivateCol()
    {
        col.isTrigger = false;
    }

    public void Something()
    {
        cd += Time.deltaTime;
        if (cd > 0)
        {
            if (Mathf.Abs(newPosition.position.x - transform.position.x) <= 0.1f && Mathf.Abs(newPosition.position.z - transform.position.z) <= 0.1f)
            {
                locked = true;
            }
            cd = 0;
        }
        else if (Mathf.Abs(newPosition.position.x - transform.position.x) >= 0.1f && Mathf.Abs(newPosition.position.z - transform.position.z) >= 0.1f)
        {
            Debug.Log("Else");
                Vector3 dir = (newPosition.position - transform.position);
                rb.MovePosition(transform.position + dir.normalized * 6 * Time.fixedDeltaTime);
                rb.MoveRotation(newPosition.rotation);
        }
    }
}
    
