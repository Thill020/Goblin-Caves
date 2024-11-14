using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickupable : MonoBehaviour
{
    bool twoHandedItem = false;
    float throwModifier = 1f;

    [SerializeField]
    GameObject _holdLocation;
    [SerializeField]
    GameObject _player;
    Rigidbody rb;
    Collider collider;
    private bool isHeld;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        collider = rb.GetComponent<Collider>();
    }

    private void Update()
    {
        if (!isHeld)
            return;

        transform.position = _holdLocation.transform.position;
    }

    public void Pickup()
    {
        isHeld = true;
        transform.position = _holdLocation.transform.position;
        rb.useGravity = false;
        collider.isTrigger = true;
    }

    public void Drop()
    {
        isHeld = false;
        rb.useGravity = true;
    }

    public void Throw()
    {
        isHeld = false;
        rb.useGravity = true;
        rb.AddForce(150f * _player.transform.forward * throwModifier, ForceMode.Impulse);
        collider.isTrigger = false;
    }
}
