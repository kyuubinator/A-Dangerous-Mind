using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendMessage : MonoBehaviour
{
    [SerializeField] private string text;

    public void SpeakMessage(UIManager ui)
    {
        ui.DisplayText(text);
    }
}
