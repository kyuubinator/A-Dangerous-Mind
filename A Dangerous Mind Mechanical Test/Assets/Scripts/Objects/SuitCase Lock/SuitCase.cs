using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuitCase : MonoBehaviour
{
    [SerializeField] private int[] input;
    [SerializeField] private string Solution;
    [SerializeField] private string SolutionCheck;
    private bool solved;

    [SerializeField] private GameObject suitcaseUpper;

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
                suitcaseUpper.transform.rotation = new Quaternion(180,0,0,0);   // rotação nao funciona direito
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
