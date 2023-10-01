using UnityEngine;
using DG.Tweening;

public class Dinosaur : MonoBehaviour
{
    private Transform target;
    private Animator animator;

    public Rigidbody tail;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Flag").transform;
        animator = GetComponent<Animator>();
    }

    public void Attack()
    {
        transform.DOLookAt(target.position, 0.5f);
        Vector3 targetPosition = new Vector3(target.position.x - 5f, transform.position.y, target.position.z);
        transform.DOMove(targetPosition, 0.5f);

        // // turn the dinosaur 90 degrees along the Y axis
        // transform.DORotate(new Vector3(0f, 0f, 0f), 0.5f);

        // Create an explosion to every "block" in the proximity of the tail rigidbody
        
        Collider[] colliders = Physics.OverlapSphere(transform.position, 10f);

        foreach (Collider collider in colliders)
        {
            Rigidbody rigidbody = collider.GetComponent<Rigidbody>();
            if (rigidbody != null)
            {
                rigidbody.AddExplosionForce(100f, transform.position, 100f, 3f, ForceMode.Impulse);
            }
        }
    }
}