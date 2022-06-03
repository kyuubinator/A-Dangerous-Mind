using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candles : MonoBehaviour, IInteractable
{
    [SerializeField] private float Value;

    private void Start()
    {
        FindObjectOfType<AudioManager>().Play("LitCandle");
    }

    public void Interact(PlayerCharacter player)
    {
        player.AddLightCandle(Value);
        Destroy(gameObject);
    }
}
