using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinosaurAI : MonoBehaviour
{
    // The dinosaur will move towards the building when it reaches the building it will try to use its tail to destroy the building

    public float speed = 1f;

    public Animator animator;

    public GameObject target;

    public Transform tail;

    void TailAttack()
    {
        
    }
}
