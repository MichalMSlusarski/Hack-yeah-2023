using UnityEngine;

public class Wheel : MonoBehaviour
{
    [SerializeField] private GameObject carBody;

    [SerializeField] float offset = 0.2f;

    // Update carBody position BUT not rotation relative to the wheel

    private void Update()
    {

        // Set the position of the car body relative to the wheel
        carBody.transform.position = new Vector3(transform.position.x, carBody.transform.position.y, carBody.transform.position.z);
    }
}
