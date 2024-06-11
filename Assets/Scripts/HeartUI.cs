using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HeartUI : MonoBehaviour
{
    public TMP_Text descriptionText;  // Use TMP_Text instead of Text
    public Button highlightButton;
    public Button deleteButton; // Add reference to the delete button
    public PlaceOnPlane placeOnPlane;  // Reference to PlaceOnPlane script

    private GameObject heartInstance;
    private HeartPart[] heartParts;
    private HeartPart selectedPart;

    // Names of parts to delete
    private string[] partsToDelete = { "General Surface1", "General Surface2"};

    void Start()
    {
        // Add listener for the button clicks
        highlightButton.onClick.AddListener(OnHighlightButtonClicked);
        deleteButton.onClick.AddListener(OnDeleteButtonClicked); // Add listener for delete button
    }

    void Update()
    {
        if (heartInstance == null)
        {
            heartInstance = placeOnPlane.GetSpawnedObject();
            if (heartInstance != null)
            {
                // Get all HeartPart components from the instantiated prefab
                heartParts = heartInstance.GetComponentsInChildren<HeartPart>();
            }
        }

        CheckForLookedAtPart();
    }

    void CheckForLookedAtPart()
    {
        // Raycast from the center of the screen
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            // Check if the hit object has a HeartPart component
            HeartPart heartPart = hit.collider.GetComponent<HeartPart>();
            if (heartPart != null)
            {
                // Store the selected part
                selectedPart = heartPart;
            }
        }
    }

    void OnHighlightButtonClicked()
    {
        if (selectedPart != null)
        {
            // Highlight the selected part and show its description
            foreach (var part in heartParts)
            {
                if (part == selectedPart)
                {
                    part.Highlight();
                    descriptionText.text = part.name;
                }
                else
                {
                    part.RemoveHighlight();
                }
            }
        }
    }

    void OnDeleteButtonClicked()
    {
        if (heartParts != null)
        {
            foreach (var part in heartParts)
            {
                if (System.Array.Exists(partsToDelete, element => element == part.name))
                {
                    Destroy(part.gameObject);
                }
            }
        }
    }
}
