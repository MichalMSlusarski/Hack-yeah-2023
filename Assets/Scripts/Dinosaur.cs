using UnityEngine;

public class Dinosaur : MonoBehaviour
{
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Attack()
    {
        rb.isKinematic = false;
    }
}