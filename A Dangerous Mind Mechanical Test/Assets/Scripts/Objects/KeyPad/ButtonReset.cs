using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonReset : MonoBehaviour, IInteractable
{
    [SerializeField] private KeyPad keypad;

    public void Interact(PlayerCharacter player)
    {
        if (!keypad.Solved)
            keypad.ResetInput();
        else
            Debug.Log("Puzzle Already Solved");
    }
}
