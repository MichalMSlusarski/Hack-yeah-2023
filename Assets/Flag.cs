using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag : MonoBehaviour
{
    private bool hasCollided = false;
    public GameObject failedPanel;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Tile") && !hasCollided)
        {
            hasCollided = true;
            Time.timeScale = 0f; // Pause the game
            // Trigger the "failed" end screen panel here
            failedPanel.SetActive(true);
        }
    }
}
