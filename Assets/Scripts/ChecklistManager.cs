using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChecklistManager : MonoBehaviour
{
    [SerializeField] private GameObject checklistItemTemplate;  // Reference to the Toggle template
    [SerializeField] private Transform checklistContainer;      // Panel where items will be added
    [SerializeField] private Text scoreText;                    // Text field to display the score

    // List of items to display in the checklist
    private List<string> checklistItems = new List<string>
    {
        "Panda Box",
        "Blue Donkey Coffee",
        "AirPods Pro",
        "MacBook Air",
        "Ramen Pack"
    };

    private Dictionary<string, Toggle> checklistToggles = new Dictionary<string, Toggle>();
    private int totalScore = 0;

    private void Start()
    {
        PopulateChecklist();
        UpdateScore(0);  // Initialize score display
    }

    // Creates checklist items dynamically
    private void PopulateChecklist()
    {
        foreach (var item in checklistItems)
        {
            // Instantiate a new toggle for each checklist item
            GameObject newItem = Instantiate(checklistItemTemplate, checklistContainer);
            newItem.SetActive(true); // Ensure the item is visible

            // Set the label text to the item name
            Text label = newItem.GetComponentInChildren<Text>();
            if (label != null)
            {
                label.text = item;
            }

            // Get the Toggle component and store it in the dictionary for later reference
            Toggle toggle = newItem.GetComponent<Toggle>();
            checklistToggles[item] = toggle;
        }
    }

    // Method to check off an item in the checklist
    public void MarkItemAsFound(string itemName)
    {
        if (checklistToggles.ContainsKey(itemName))
        {
            checklistToggles[itemName].isOn = true; // Check off the item
            Debug.Log(itemName + " has been found!");
        }
    }

    // Method to update the score display
    public void UpdateScore(int newScore)
    {
        totalScore = newScore;
        if (scoreText != null)
        {
            scoreText.text = "Score: " + totalScore;
        }
    }
}