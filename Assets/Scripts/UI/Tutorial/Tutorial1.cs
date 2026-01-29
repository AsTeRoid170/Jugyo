using UnityEngine;

public class Tutorial1 : MonoBehaviour
{
    public GameObject tutorialUI;

    void Start()
    {
        tutorialUI.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            tutorialUI.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            tutorialUI.SetActive(false);
        }
    }
}
