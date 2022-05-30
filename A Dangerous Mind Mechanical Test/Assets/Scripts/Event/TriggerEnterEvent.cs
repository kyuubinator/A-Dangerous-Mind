using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEnterEvent : MonoBehaviour
{
    [SerializeField] private EventManager eventManager;

    private void OnTriggerEnter(Collider other)
    {
        PlayerCharacter player = other.GetComponent<PlayerCharacter>();
        if (player != null)
        {
            eventManager.StartEvent1();
        }
    }
}
