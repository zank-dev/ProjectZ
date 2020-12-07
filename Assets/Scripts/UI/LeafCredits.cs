using TMPro;
using UnityEngine;

public class LeafCredits : MonoBehaviour
{
    // Displays the leafs in the credits

    private TMP_Text text;

    private void Start()
    {
        text = GetComponent<TMP_Text>();
        text.text = $"{PlayerPrefs.GetInt("leafs", 0)} / 128";
    }
}