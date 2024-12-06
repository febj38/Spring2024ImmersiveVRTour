using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // Add this to use TextMeshPro

public class ChecklistManager : MonoBehaviour
{
    public static ChecklistManager Instance { get; private set; }  // Singleton instance

    [SerializeField] private GameObject checklistItemTemplate;  // Template for checklist items (Toggle UI)
    [SerializeField] private Transform checklistContainer;      // ScrollView Content to hold checklist items
    [SerializeField] private TextMeshProUGUI scoreText;         // TextMeshPro for the total score
    [SerializeField] private ScrollRect scrollView;             // Reference to the ScrollView component

    public List<string> checklistItems = new List<string>();   // List of all collectible items
    private Dictionary<string, Toggle> checklistToggles = new Dictionary<string, Toggle>();  // Dictionary to map item names to their Toggles
    public int totalScore = 0;                                 // Total score or items found count

    private void Awake()
    {
        // Singleton pattern: Ensure only one instance exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // Make this object persist across scenes
        }
        else
        {
            Destroy(gameObject);  // Destroy any duplicate instances
        }

        // Validate required references
        if (checklistItemTemplate == null) Debug.LogWarning("Checklist Item Template is not assigned!");
        if (checklistContainer == null) Debug.LogWarning("Checklist Container is not assigned!");
        if (scoreText == null) Debug.LogWarning("Score Text is not assigned!");
        if (scrollView == null) Debug.LogWarning("Scroll View is not assigned!");
    }

    private void Start()
    {
        PopulateChecklist();
        UpdateScoreDisplay();  // Initialize score display
        InitializeScrollView(); // Ensure the ScrollView starts at the top
    }

    // Adds all collectibles to the checklist at the start
    private void PopulateChecklist()
    {
        // Clear existing checklist items
        foreach (Transform child in checklistContainer)
        {
            Destroy(child.gameObject);
        }

        // Temporary test data for checklist
        string[] testItems = { "Ramen Bowl", "Buzz Prison", "Football", "Band Hat", "Rat Cap" };
        foreach (string itemName in testItems)
        {
            checklistItems.Add(itemName);

            GameObject newItem = Instantiate(checklistItemTemplate, checklistContainer);
            newItem.SetActive(true);

            TextMeshProUGUI label = newItem.GetComponentInChildren<TextMeshProUGUI>();
            if (label != null)
            {
                label.text = itemName;
            }

            Toggle toggle = newItem.GetComponent<Toggle>();
            checklistToggles[itemName] = toggle;
        }

        UpdateContentSize();
    }

    // Updates the content size of the ScrollView dynamically
    private void UpdateContentSize()
    {
        if (checklistContainer.TryGetComponent<RectTransform>(out RectTransform rectTransform))
        {
            float itemHeight = checklistItemTemplate.GetComponent<RectTransform>().rect.height;
            float spacing = checklistContainer.GetComponent<VerticalLayoutGroup>().spacing;
            float totalHeight = (itemHeight + spacing) * checklistItems.Count;

            rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, totalHeight);
        }
    }

    // Ensure the ScrollView starts at the top
    private void InitializeScrollView()
    {
        if (scrollView != null)
        {
            scrollView.verticalNormalizedPosition = 1f; // Position ScrollView to top
        }
    }

    // Method to mark an item as found
    public void MarkItemAsFound(string itemName)
    {
        if (checklistToggles.ContainsKey(itemName) && !checklistToggles[itemName].isOn)
        {
            checklistToggles[itemName].isOn = true;  // Check off the item
            IncrementScore();
        }
    }

    // Increments the score and updates the display
    private void IncrementScore()
    {
        totalScore++;
        UpdateScoreDisplay();
    }

    // Updates the score text display
    private void UpdateScoreDisplay()
    {
        if (scoreText != null)
        {
            scoreText.text = "Items Found: " + totalScore;
        }
    }

    // Method to refresh the checklist (e.g., for dynamic updates)
    public void RefreshChecklist()
    {
        PopulateChecklist();
        UpdateScoreDisplay();
    }
}