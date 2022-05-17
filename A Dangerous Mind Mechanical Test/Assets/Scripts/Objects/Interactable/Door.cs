using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    private Animator animator;
    private bool opened;
    [SerializeField] private bool canOpen;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Interact(PlayerCharacter player)
    {

        if (canOpen)
        {
            opened = !opened;
            if (opened)
                animator.SetBool("Opened", true);
            else
                animator.SetBool("Opened", false);
            StartCoroutine(OpenCooldown());
        }
    }

    IEnumerator OpenCooldown()
    {
        canOpen = false;
        yield return new WaitForSeconds(1);
        canOpen = true;
    }

    public void UnlockDoor()
    {
        canOpen = true;
        Interact(null);
    }
}
