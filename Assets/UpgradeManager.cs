using UnityEngine;
using TMPro; // Import TextMeshPro namespace
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    public TMP_Text speedText; // Reference to the Speed Text element
    public TMP_Text laserText; // Reference to the Laser Text element
    public TMP_Text healthText; // Reference to the Health Text element

    public TMP_Text speedMaxLevelText; // Reference to the Speed Max Level Text element
    public TMP_Text laserMaxLevelText; // Reference to the Laser Max Level Text element
    public TMP_Text healthMaxLevelText; // Reference to the Health Max Level Text element

    public Button speedButton; // Reference to the Speed Button
    public Button laserButton; // Reference to the Laser Button
    public Button healthButton; // Reference to the Health Button

    private int speedLevel = 1;
    private int laserLevel = 1;
    private int healthLevel = 1;
    private int maxLevel = 20; // Maximum upgrade level

    private PlayerController playerController;

    void Start()
    {
        // Find the PlayerController in the scene
        playerController = FindObjectOfType<PlayerController>();

        // Update the UI with initial values
        UpdateUI();

        // Add button click listeners
        speedButton.onClick.AddListener(UpgradeSpeed);
        laserButton.onClick.AddListener(UpgradeLaser);
        healthButton.onClick.AddListener(UpgradeHealth);

        // Hide max level texts initially
        speedMaxLevelText.gameObject.SetActive(false);
        laserMaxLevelText.gameObject.SetActive(false);
        healthMaxLevelText.gameObject.SetActive(false);
    }

    void UpdateUI()
    {
        speedText.text = "Speed Level: " + speedLevel;
        laserText.text = "Laser Level: " + laserLevel;
        healthText.text = "Health Level: " + healthLevel;

        // Show max level text if any level reaches maxLevel
        speedMaxLevelText.gameObject.SetActive(speedLevel >= maxLevel);
        laserMaxLevelText.gameObject.SetActive(laserLevel >= maxLevel);
        healthMaxLevelText.gameObject.SetActive(healthLevel >= maxLevel);
    }

    void UpgradeSpeed()
    {
        if (speedLevel < maxLevel)
        {
            speedLevel++;
            UpdateUI();
            playerController.UpgradeSpeed(0.5f); // Increase speed by 0.5
        }
        else
        {
            Debug.Log("Speed is at max level");
        }
    }

    void UpgradeLaser()
    {
        if (laserLevel < maxLevel)
        {
            laserLevel++;
            UpdateUI();
            playerController.UpgradeLaser(0.5f); // Increase laser speed by 0.5
        }
        else
        {
            Debug.Log("Laser is at max level");
        }
    }

    void UpgradeHealth()
    {
        if (healthLevel < maxLevel)
        {
            healthLevel++;
            UpdateUI();
            playerController.UpgradeHealth(1); // Increase health by 1
        }
        else
        {
            Debug.Log("Health is at max level");
        }
    }
}
