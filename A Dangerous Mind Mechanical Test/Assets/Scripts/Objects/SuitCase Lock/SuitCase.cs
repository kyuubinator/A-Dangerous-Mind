using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuitCase : MonoBehaviour
{
    [SerializeField] private int[] input;
    [SerializeField] private string Solution;
    [SerializeField] private string SolutionCheck;
    private float velocity = 10;
    private bool solved;

    [SerializeField] private GameObject suitcaseUpper;

    public bool Solved { get => solved; set => solved = value; }

    public void NewInput(int digit, int inputNum)
    {
        if (solved)
        {

        }
        else
        {
            input[inputNum] = digit;
            SolutionCheck = input[0].ToString() + input[1].ToString() + input[2].ToString();
            ConfirmInput();
        }
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
                suitcaseUpper.transform.eulerAngles = new Vector3(70, 180 ,0);   
                Debug.Log("Right Code");
                solved = true;
            }
            else
            {
                Debug.Log("Wrong Code");
            }
        }
    }
}
