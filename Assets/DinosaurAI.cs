using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DinosaurAI : MonoBehaviour
{
    // The dinosaur will move towards the building when it reaches the building it will try to use its tail to destroy the building

    public float speed = 1f;

    

    public GameObject target;

    public Transform tail;

    private void TailAttack()
    {

    }

    private void Walk()
    {
        // Dinosaur walks just like in toy by turning itself on the Y axis as it walks


    }
}
