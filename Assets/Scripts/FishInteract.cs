using UnityEngine;

public class FishInteract : MonoBehaviour
{
    private Flock flockScript;
    private QuestManager questManager;
    public GameObject interactUi;
    private bool isInteractable = false;

    void Start()
    {
        flockScript = FindObjectOfType<Flock>();
        if (flockScript != null && flockScript.interactUiPrefab != null)
        {
            interactUi = flockScript.interactUiPrefab;
        }
        else
        {
            Debug.LogError("Flock or Interact UI not found!");
        }

        questManager = FindObjectOfType<QuestManager>();
        if (questManager == null)
        {
            Debug.LogError("QuestManager not found in the scene!");
        }

        if (interactUi != null)
        {
            interactUi.SetActive(false);
        }
    }

    void Update()
    {
        if (isInteractable && Input.GetKeyDown(KeyCode.F))
        {
            if (questManager != null)
            {
                questManager.CompleteQuest(gameObject.tag);
            }

            Debug.Log("Quest completed for fish: " + gameObject.name);

            if (interactUi != null)
            {
                interactUi.SetActive(false);
            }

            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Submarine"))
        {
            if (gameObject.CompareTag("hiu") && flockScript != null)
            {
                flockScript.TriggerGameOver("Anda ditangkap hiu!");
                return;
            }

            isInteractable = true;

            if (interactUi != null)
            {
                interactUi.SetActive(true);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Submarine"))
        {
            isInteractable = false;

            // Nonaktifkan UI ketika keluar dari trigger
            if (interactUi != null)
            {
                interactUi.SetActive(false); // Menonaktifkan UI interaksi
            }
        }
    }
}
