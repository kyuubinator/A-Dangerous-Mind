using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DocumentRead : MonoBehaviour, IReadable
{
    [SerializeField] private GameObject docUI;
    [SerializeField] private GameObject blur;
    [SerializeField] private bool interacting;

    public void Read()
    {
        if (!interacting)
        {
            docUI.SetActive(true);
            blur.SetActive(true);
        }
        else
        {
            docUI.SetActive(false);
            blur.SetActive(false);
        }
        interacting = !interacting;
    }
}
