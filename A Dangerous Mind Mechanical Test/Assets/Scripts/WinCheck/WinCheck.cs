using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCheck : MonoBehaviour
{
    [SerializeField] private UIManager ui;

    private void OnTriggerEnter(Collider collision)
    {
        PlayerCharacter player = collision.GetComponent<PlayerCharacter>();
        if (player != null)
        {
            ui.ShowWin();
        }
    }
}
