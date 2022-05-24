using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DocumentRead : MonoBehaviour, IReadable
{
    [SerializeField] private GameObject docUI;
    [SerializeField] private bool interacting;

    public void Read()
    {
        if (!interacting)
        {
            docUI.SetActive(true);
        }
        else
        {
            docUI.SetActive(false);
        }
        interacting = !interacting;
    }
}
