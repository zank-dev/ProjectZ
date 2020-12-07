using UnityEngine;
using UnityEngine.EventSystems;

public class AddMenuButtonToEventSystem : MonoBehaviour
{
    public GameObject selectedGameObject;

    private bool selected = false;

    private void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(null);
        selected = true;
    }

    private void Update()
    {
        if(selected) // selects the first object in the menu has to be solved like this, because other methodes sadly didn't work
        {
            EventSystem.current.SetSelectedGameObject(selectedGameObject);
            selected = false;
        }
    }
}