using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RotateCamera : MonoBehaviour
{
    public float speed = 1f;

    public Transform rotationPoint;

    private Quaternion initialRotation;

    private void Start()
    {
        initialRotation = transform.rotation;
    }

    public void RotateLeft()
    {
        rotationPoint.DORotate(new Vector3(0, initialRotation.y + 90, 0), speed);
    }

    public void RotateRight()
    {
        rotationPoint.DORotate(new Vector3(0, initialRotation.y, 0), speed);
    }
}
