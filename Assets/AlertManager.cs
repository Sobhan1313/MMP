using UnityEngine;
using TMPro; // Import the TextMeshPro namespace
using System.Collections;

public class AlertManager : MonoBehaviour
{
    public TMP_Text alertText; // Reference to the TextMeshPro text element
    public float alertDuration = 5f; // Duration for which the alert will be visible
    public float alertInterval = 60f; // Interval between alerts
    private int level = 1; // Level counter

    void Start()
    {
        // Start the coroutine to display alerts
        StartCoroutine(ShowAlertMessage());
    }

    IEnumerator ShowAlertMessage()
    {
        while (true) // Infinite loop to repeatedly show the alert
        {
            // Display the alert message with the current level number
            alertText.text = $"Level {level}";
            alertText.enabled = true;

            // Wait for the alert duration
            yield return new WaitForSeconds(alertDuration);

            // Hide the alert message
            alertText.enabled = false;

            // Increment the level number for the next alert
            level++;

            // Wait for the interval before showing the next alert
            yield return new WaitForSeconds(alertInterval);
        }
    }
}
