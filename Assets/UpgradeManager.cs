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
    [SerializeField]
    private int upgradeCost; // Cost in XP for each upgrade

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
        // Update the button texts with current levels
        if (speedLevel < maxLevel)
        {
            speedText.text = "Speed Level: " + speedLevel;
        }
        else
        {
            speedText.text = "";
            speedMaxLevelText.gameObject.SetActive(true);
            speedButton.interactable = false; // Deactivate button
        }

        if (laserLevel < maxLevel)
        {
            laserText.text = "Laser Level: " + laserLevel;
        }
        else
        {
            laserText.text = "";
            laserMaxLevelText.gameObject.SetActive(true);
            laserButton.interactable = false; // Deactivate button
        }

        if (healthLevel < maxLevel)
        {
            healthText.text = "Health Level: " + healthLevel;
        }
        else
        {
            healthText.text = "";
            healthMaxLevelText.gameObject.SetActive(true);
            healthButton.interactable = false; // Deactivate button
        }
    }

    void UpgradeSpeed()
    {
        if (speedLevel < maxLevel && ScoreManager.currentScore >= upgradeCost)
        {
            speedLevel++;
            ScoreManager.instance.AddPoints(-upgradeCost); // Deduct XP
            playerController.UpgradeSpeed(0.5f); // Increase speed by 0.5
            UpdateUI();
        }
        else if (speedLevel >= maxLevel)
        {
            Debug.Log("Speed is at max level");
        }
        else
        {
            Debug.Log("Not enough XP to upgrade speed");
        }
    }

    void UpgradeLaser()
    {
        if (laserLevel < maxLevel && ScoreManager.currentScore >= upgradeCost)
        {
            laserLevel++;
            ScoreManager.instance.AddPoints(-upgradeCost); // Deduct XP
            playerController.UpgradeLaser(0.5f); // Increase laser speed by 0.5
            UpdateUI();
        }
        else if (laserLevel >= maxLevel)
        {
            Debug.Log("Laser is at max level");
        }
        else
        {
            Debug.Log("Not enough XP to upgrade laser");
        }
    }

    void UpgradeHealth()
    {
        if (healthLevel < maxLevel && ScoreManager.currentScore >= upgradeCost)
        {
            healthLevel++;
            ScoreManager.instance.AddPoints(-upgradeCost); // Deduct XP
            playerController.UpgradeHealth(1); // Increase health by 1
            UpdateUI();
        }
        else if (healthLevel >= maxLevel)
        {
            Debug.Log("Health is at max level");
        }
        else
        {
            Debug.Log("Not enough XP to upgrade health");
        }
    }
}
