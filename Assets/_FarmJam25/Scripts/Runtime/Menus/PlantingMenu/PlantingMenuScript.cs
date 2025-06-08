using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class PlantingMenu : MonoBehaviour
{
    public InputActionReference click;
    public Animator anim;
    public GameObject buttonPrefab;
    public Transform buttonContainer;
    public Image selectedItemImage;
    public Sprite noImage;
    public TextMeshProUGUI itemLabel;
    public List<PlantItem> items;
    private bool isOpen = false;
    private int selectedPlantID = 0;
    private List<PlantingMenuButton> buttons = new List<PlantingMenuButton>();

    void OnEnable()
    {
        click.action.performed += OnClick;
        click.action.Enable();
    }

    void OnDisable()
    {
        click.action.performed -= OnClick;
        click.action.Disable();
    }

    void Start()
    {
        items = new List<PlantItem>()
        {
            new PlantItem { id = 1, name = "Moss Patch", icon = noImage },
            new PlantItem { id = 2, name = "Puffball Mushroom", icon = noImage },
            new PlantItem { id = 3, name = "Shelf Mushroom", icon = noImage },
            new PlantItem { id = 4, name = "Deadwood", icon = noImage },
            new PlantItem { id = 5, name = "Bramble Bush", icon = noImage },
            new PlantItem { id = 6, name = "Fly Trap", icon = noImage },
            new PlantItem { id = 7, name = "Orchid Tree", icon = noImage },
            new PlantItem { id = 8, name = "Raspberry Bush", icon = noImage },
            new PlantItem { id = 9, name = "Baldrak’s Arm of Swiping", icon = noImage },
            new PlantItem { id = 10, name = "Teleportation Circle", icon = noImage },
            new PlantItem { id = 11, name = "Totem of Grave Relief", icon = noImage },
            new PlantItem { id = 12, name = "Urn of Enhanced Vitality", icon = noImage },
        };
        Debug.Log("helloooo");
        itemLabel.text = "hellooo";
        PopulateButtons();
    }

    void OnClick(InputAction.CallbackContext ctx)
    {
        if (ctx.control.IsPressed())
        {
            ToggleMenu();
        }
    }

    void ToggleMenu()
    {
        isOpen = !isOpen;
        anim.SetBool("OpenPlantingMenu", isOpen);
    }

    void PopulateButtons()
    {
        foreach (Transform child in buttonContainer)
            if (child.name != "SelectedText")
                Destroy(child.gameObject);

        buttons.Clear();

        int count = items.Count;
        float radius = 150f; // Adjust radius to control spacing
        float angleStep = 360f / count;

        for (int i = 0; i < count; i++)
        {
            var item = items[i];
            var btnGO = Instantiate(buttonPrefab, buttonContainer);
            var btn = btnGO.GetComponent<PlantingMenuButton>();

            // Calculate angle in radians
            float angle = i * angleStep * Mathf.Deg2Rad + Mathf.PI / 2;

            // Calculate position on circle
            Vector2 pos = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * radius;

            // Set position
            var rectTransform = btnGO.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = pos;

            // Set rotation so the button faces outward
            float angleDegrees = i * angleStep;
            rectTransform.localRotation = Quaternion.Euler(0, 0, angleDegrees);

            // Setup the button
            btn.Setup(item, this, itemLabel);
            buttons.Add(btn);
        }
    }

    public void SelectItem(PlantItem item)
    {
        Debug.Log("SELECTED");
        selectedPlantID = item.id;
        selectedItemImage.sprite = item.icon;

        foreach (var b in buttons)
            b.SetSelected(b.item.id == item.id);
    }

    public void Deselect()
    {
        selectedPlantID = 0;
        selectedItemImage.sprite = noImage;

        foreach (var b in buttons)
            b.SetSelected(false);
    }
}