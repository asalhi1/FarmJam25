using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlantingMenuButton : MonoBehaviour, 
    IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public TextMeshProUGUI label;
    private PlantingMenu menu;
    public PlantItem item;
    private bool isSelected = false;

    public void Setup(PlantItem plantItem, PlantingMenu plantingMenu, TextMeshProUGUI labelRef)
    {
        item = plantItem;   
        menu = plantingMenu;
        this.label = labelRef;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Pointer Enter: " + item.name);
        SetHover(true);
        label.text = item.name;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Pointer Exit: " + item.name);
        SetHover(false);
        label.text = "";
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Clicked on: " + item.name);
        if (isSelected)
            menu.Deselect();
        else
            menu.SelectItem(item);
    }

    public void SetHover(bool hovering)
    {
        GetComponent<Animator>().SetBool("Hover", hovering);
    }

    public void SetSelected(bool selected)
    {
        isSelected = selected;
        // Do stuff
    }
}