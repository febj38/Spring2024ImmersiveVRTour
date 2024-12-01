using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChecklistManager : MonoBehaviour
{
    public static ChecklistManager Instance { get; private set; }  // Singleton instance

    [SerializeField] private GameObject checklistItemTemplate;  // Template for checklist items (Toggle UI)
    [SerializeField] private Transform checklistContainer;      // ScrollView Content to hold checklist items
    [SerializeField] private Text scoreText;                    // Text to display the total score
    [SerializeField] private ScrollRect scrollView;             // Reference to the ScrollView component

    private List<string> checklistItems = new List<string>();   // List of all collectible items
    private Dictionary<string, Toggle> checklistToggles = new Dictionary<string, Toggle>();  // Dictionary to map item names to their Toggles
    private int totalScore = 0;                                 // Total score or items found count

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
        // Find all collectible items in the scene
        Collectible[] collectibles = FindObjectsOfType<Collectible>();
        foreach (var collectible in collectibles)
        {
            string itemName = collectible.gameObject.name;
            checklistItems.Add(itemName);

            // Instantiate a new toggle for each checklist item
            GameObject newItem = Instantiate(checklistItemTemplate, checklistContainer);
            newItem.SetActive(true);  // Ensure the item is visible

            // Set the label text to the item name
            Text label = newItem.GetComponentInChildren<Text>();
            if (label != null)
            {
                label.text = itemName;
            }

            // Get the Toggle component and store it in the dictionary for later reference
            Toggle toggle = newItem.GetComponent<Toggle>();
            checklistToggles[itemName] = toggle;

            // Subscribe to the collectible's event
            collectible.OnCollected += () => MarkItemAsFound(itemName);
        }

        // Adjust Content Size for ScrollView
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

            // Update the height of the Content's RectTransform
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
            scoreText.text = "Items Found: " + totalScore + "/" + checklistItems.Count;
        }
    }
}using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChecklistManager : MonoBehaviour
{
    public static ChecklistManager Instance { get; private set; }  // Singleton instance

    [SerializeField] private GameObject checklistItemTemplate;  // Template for checklist items (Toggle UI)
    [SerializeField] private Transform checklistContainer;      // ScrollView Content to hold checklist items
    [SerializeField] private Text scoreText;                    // Text to display the total score
    [SerializeField] private ScrollRect scrollView;             // Reference to the ScrollView component

    private List<string> checklistItems = new List<string>();   // List of all collectible items
    private Dictionary<string, Toggle> checklistToggles = new Dictionary<string, Toggle>();  // Dictionary to map item names to their Toggles
    private int totalScore = 0;                                 // Total score or items found count

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
        // Find all collectible items in the scene
        Collectible[] collectibles = FindObjectsOfType<Collectible>();
        foreach (var collectible in collectibles)
        {
            string itemName = collectible.gameObject.name;
            checklistItems.Add(itemName);

            // Instantiate a new toggle for each checklist item
            GameObject newItem = Instantiate(checklistItemTemplate, checklistContainer);
            newItem.SetActive(true);  // Ensure the item is visible

            // Set the label text to the item name
            Text label = newItem.GetComponentInChildren<Text>();
            if (label != null)
            {
                label.text = itemName;
            }

            // Get the Toggle component and store it in the dictionary for later reference
            Toggle toggle = newItem.GetComponent<Toggle>();
            checklistToggles[itemName] = toggle;

            // Subscribe to the collectible's event
            collectible.OnCollected += () => MarkItemAsFound(itemName);
        }

        // Adjust Content Size for ScrollView
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

            // Update the height of the Content's RectTransform
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
            scoreText.text = "Items Found: " + totalScore + "/" + checklistItems.Count;
        }
    }
}
