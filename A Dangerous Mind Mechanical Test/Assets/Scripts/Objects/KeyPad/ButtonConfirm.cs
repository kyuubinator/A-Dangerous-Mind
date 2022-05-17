using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonConfirm : MonoBehaviour, IInteractable
{
    [SerializeField] private KeyPad keypad;

    public void Interact(PlayerCharacter player)
    {
        if (!keypad.Solved)
            keypad.ConfirmInput();
        else
            Debug.Log("Puzzle Already Solved");
    }
}
