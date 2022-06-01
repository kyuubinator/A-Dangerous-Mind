using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpAndMesage : MonoBehaviour, IPickable
{
    [SerializeField] private string text;
    [SerializeField] private int value;

    public void Pickup(PlayerCharacter player, UIManager ui)
    {
        player.EnableBools(value);
        ui.DisplayText(text);
        Destroy(gameObject);
    }
}
