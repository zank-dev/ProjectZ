using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AutoScrollDropdown : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private RectTransform scroll;
    private RectTransform content;
    private RectTransform selectedRect;
    private GameObject lastSelected;
    private Vector2 targetPosition;
    private bool mouseOver;
    private string[] joysticks;

    void Start()
    {
        scroll = GetComponent<RectTransform>();

        if (content == null) content = GetComponent<ScrollRect>().content;

        targetPosition = content.anchoredPosition;
        joysticks = Input.GetJoystickNames();
    }

    void Update()
    {
        if (!mouseOver) // if mouse is not in the menu or user uses controller allows autoscroller
        {
            if (content == null) content = GetComponent<ScrollRect>().content; // gets  the dropdowncontent

            GameObject selected = EventSystem.current.currentSelectedGameObject; // gets the selected item from eventsystem

            if (selected == null) return;
            if (selected.transform.parent != content.transform) return;
            if (selected == lastSelected) return;

            selectedRect = (RectTransform)selected.transform;

            targetPosition.x = content.anchoredPosition.x;
            targetPosition.y = -(selectedRect.localPosition.y) - (selectedRect.rect.height / 2); // position of selected minus selected height
            targetPosition.y = Mathf.Clamp(targetPosition.y, 0, content.sizeDelta.y - scroll.sizeDelta.y); // limits scroll up / down

            content.anchoredPosition = targetPosition; // sets the anchor of the content, therefore moves the dropdown
            lastSelected = selected;
        }
    }

    public void OnPointerEnter(PointerEventData eventData) // mouse entered menu
    {
        if (joysticks.Length != 0 && joysticks[0] != "") return;
        
        mouseOver = true; 
    }

    public void OnPointerExit(PointerEventData eventData) // mouse exited menu
    {
        mouseOver = false;
    }
}