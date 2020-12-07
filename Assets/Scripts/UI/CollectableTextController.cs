using UnityEngine;
using TMPro;

public class CollectableTextController : MonoBehaviour
{
    private TMP_Text text;
    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        text = GetComponent<TMP_Text>();
        gameManager.valueChanged.AddListener(UpdateCollectableChanged); // conects the text with the valueChangedEvent
        UpdateCollectable();
    }

    private void UpdateCollectableChanged(string name, float value) // on event trigger updates the text
    {
        if (name == "leafs")
        {
            text.text = (value + PlayerPrefs.GetInt("leafs", 0)).ToString();
        }
    }

    private void UpdateCollectable()
    {
        // gets the current leafs 
        text.text = (gameManager.GetValues("leafs") + PlayerPrefs.GetInt("leafs", 0)).ToString();
    }
}