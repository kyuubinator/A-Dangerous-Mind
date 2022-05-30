using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    private Animator animator;
    private bool opened;
    [SerializeField] private bool canOpen;
    [SerializeField] private bool openebleByEnemy;

    private MeshCollider col;

    private void Start()
    {
        animator = GetComponent<Animator>();
        col = GetComponent<MeshCollider>();
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
        col.isTrigger = true;
        yield return new WaitForSeconds(1);
        canOpen = true;
        col.isTrigger = false;
    }

    public void UnlockDoor()
    {
        canOpen = true;
        Interact(null);
    }

    public void EnemyOpen()
    {
        StartCoroutine(EnemyOpenDoor());
    }

    IEnumerator EnemyOpenDoor()
    {
        yield return new WaitForSeconds(2);
        opened = false;
        Interact(null);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (openebleByEnemy)
        {
            Debug.Log(collision.ToString());
            WalkingEnemy enemy = collision.GetComponent<WalkingEnemy>();
            if (enemy != null)
            {
                int RandomNum = Random.Range(0, 10);
                if (RandomNum <= 10)
                {
                    EnemyOpen();
                }
            }
        }
    }
}
