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
        transform.DOMove(target.position, 0.5f).OnComplete(() =>
        {
            animator.SetBool("isAttacking", false);
        });
        animator.SetBool("isAttacking", true);

        // turn the dinosaur 90 degrees along the Y axis
        transform.DORotate(new Vector3(0f, 90f, 0f), 0.5f);

        // Create an explosion to every "block" in the proximity of the tail rigidbody
        Collider[] colliders = Physics.OverlapSphere(tail.transform.position, 1f);
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Block"))
            {
                Rigidbody rb = collider.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.AddExplosionForce(100f, tail.transform.position, 1f);
                }
            }
        }
    }
}