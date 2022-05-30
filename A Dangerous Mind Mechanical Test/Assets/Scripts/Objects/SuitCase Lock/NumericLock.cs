using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumericLock : MonoBehaviour, IInteractable
{

    [SerializeField] private SuitCase suitcase;
    [SerializeField] private int number;
    [SerializeField] private int lockNumber;
    private int increaseValue = 36;

    public void Interact(PlayerCharacter player)
    {
        if (suitcase != null)
        {
            if (suitcase.Solved)
            {

            }
            else
            {
                number++;
                if (number > 9)
                {
                    number = 0;
                }
                //transform.rotation = new Quaternion(transform.rotation.x-36,0,transform.rotation.z, 0);    // rotação nao funciona direito
                //transform.rotation = new Quaternion(36, 0, 0, 0);
                transform.eulerAngles = new Vector3(20 - increaseValue * number, 180 ,-90);
                suitcase.NewInput(number, lockNumber);
            }
        }
    }
}
