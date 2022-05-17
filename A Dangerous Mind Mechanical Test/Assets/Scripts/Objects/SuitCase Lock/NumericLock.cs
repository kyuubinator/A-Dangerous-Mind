using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumericLock : MonoBehaviour, IInteractable
{

    [SerializeField] private SuitCase suitcase;
    [SerializeField] private int number;
    [SerializeField] private int lockNumber;

    public void Interact(PlayerCharacter player)
    {
        if (suitcase != null)
        {
            number++;
            if (number > 9)
            {
                number = 0;
            }
            transform.rotation = new Quaternion(transform.rotation.x-36,0,transform.rotation.z, 0);    // rotação nao funciona direito
            suitcase.NewInput(number, lockNumber);
        }
    }
}
