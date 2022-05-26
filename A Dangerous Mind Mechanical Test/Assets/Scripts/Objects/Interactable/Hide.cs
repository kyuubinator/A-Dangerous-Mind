using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hide : MonoBehaviour, IHideble
{

    [SerializeField] private GameObject hideCamera;
    [SerializeField] private GameObject playerh;
    private bool hidden;

    private void Update()
    {
        if (hidden)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Unhide();
            }
        }
    }

    public void Unhide()
    {
        playerh.SetActive(true);
        hideCamera.SetActive(false);
        playerh = null;

        hidden = false;
    }

    void IHideble.Hide(UnityEngine.GameObject player)
    {
        playerh = player;
        playerh.SetActive(false);
        hideCamera.SetActive(true);

        hidden = true;
    }
}
