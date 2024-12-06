using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarController : MonoBehaviour
{
    public Image fillImage; // Reference to the fill image
    private int totalItemsToFind; // Total number of items to find

    private void Start()
    {
    // Auto-detect the fill image from children
    fillImage = GetComponentInChildren<Image>();

    if (fillImage != null)
    {
        fillImage.fillAmount = 0;
    }

    // Fetch total items to find
    if (ChecklistManager.Instance != null)
    {
        totalItemsToFind = ChecklistManager.Instance.checklistItems.Count;
    }
}


    private void Update()
    {
        // Get the current score from ChecklistManager
        int currentScore = ChecklistManager.Instance != null ? ChecklistManager.Instance.totalScore : 0;

        // Update the fill amount based on the current score
        if (fillImage != null && totalItemsToFind > 0)
        {
            float progress = (float)currentScore / totalItemsToFind;
            fillImage.fillAmount = Mathf.Clamp01(progress); // Clamp the value between 0 and 1
        }
    }
}

