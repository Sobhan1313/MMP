using UnityEngine;

public class UpgradeStation : MonoBehaviour
{
    public GameObject upgradePanel; // Reference to the Upgrade Panel GameObject

    private void Start()
    {
        if (upgradePanel != null)
        {
            upgradePanel.SetActive(false); // Initially hide the upgrade panel
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (upgradePanel != null)
            {
                upgradePanel.SetActive(true); // Show the upgrade panel when the player enters the trigger area
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (upgradePanel != null)
            {
                upgradePanel.SetActive(false); // Hide the upgrade panel when the player exits the trigger area
            }
        }
    }
}
