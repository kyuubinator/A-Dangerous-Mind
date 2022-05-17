using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KeyPad : MonoBehaviour
{
    [SerializeField] private int[] input;
    [SerializeField] private string Solution;
    [SerializeField] private string SolutionCheck;
    private bool solved;

    [SerializeField] private Door door;
    [SerializeField] private TextMeshPro display;

    public bool Solved { get => solved; }

    public void NewInput(int digit)
    {
        if (solved)
        {

        }
        else
        { 
            if (input[0] != 0)
            {

            }
            else
            {
                input[0] = input[1];
                input[1] = input[2];
                input[2] = input[3];
                input[3] = digit;

                SolutionCheck = input[0].ToString() + input[1].ToString() + input[2].ToString() + input[3].ToString();
                UpdateDisplay();
            }
        }
    }
    
    public void ResetInput()
    {
        for (int i = 0; i < input.Length; i++)
        {
            input[i] = 0;
        }
        SolutionCheck = input[0].ToString() + input[1].ToString() + input[2].ToString() + input[3].ToString();
        UpdateDisplay();
    }

    public void ConfirmInput()
    {
        if (solved)
        {

        }
        else
        {
            if (Solution == SolutionCheck)
            {
                door.UnlockDoor();
                solved = true;
            }
            else
            {
                Debug.Log("Wrong Code");
                ResetInput();
            }
        }
    }
    
    public void UpdateDisplay()
    {
        display.text = SolutionCheck;
    }
}
