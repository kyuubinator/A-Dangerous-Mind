using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private PlayerCharacter player;
    [SerializeField] private GameObject[] items;
    [SerializeField] private GameObject activeObject;
    [SerializeField] private int maxItems;
    [SerializeField] private int numHoldedItems;
    [SerializeField] private int selectedItem;

    public void AddItem(GameObject objects)
    {
        items[selectedItem] = objects;
        activeObject = objects;
    }

    public void RemoveItem(GameObject objects)
    {
        items[selectedItem] = null;
        activeObject = null;
    }

    public void CicleItems()
    {
        selectedItem++;
        DeactivateNonSelected();
        if (maxItems - 1 < selectedItem )
        {
            selectedItem = 0;
        }
        if (items[selectedItem] == null)
        {
            Debug.Log("DDDD");
        }
        activeObject = items[selectedItem];

        NewGrabbedObject();
        ActivateSelected();
    }

    private void DeactivateNonSelected()
    {
        if (items[selectedItem - 1] != null)
        {
            if (selectedItem - 1 < 0)
            {
                if (items[maxItems - 1] != null)
                {
                    items[maxItems - 1].SetActive(false);
                }
            }
            else
            {
                items[selectedItem - 1].SetActive(false);
            }
        }
    }

    private void ActivateSelected()
    {
        if (items[selectedItem] != null)
        {
            items[selectedItem].SetActive(true);
        }
    }

    private void NewGrabbedObject()
    {
        if (activeObject != null)
        {
            player.GrabbedObject = activeObject;
            player.IsGrabbing = true;
        }
        else
        {
            player.GrabbedObject = null;
            player.IsGrabbing = false;
        }
    }
}
