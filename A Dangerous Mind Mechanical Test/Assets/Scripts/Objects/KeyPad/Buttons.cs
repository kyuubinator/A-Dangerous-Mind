using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buttons : MonoBehaviour, IInteractable
{
    [SerializeField] private int digit;

    [SerializeField] private KeyPad keypad;

    public void Interact(PlayerCharacter player)
    {
        keypad.NewInput(digit);
    }
}
