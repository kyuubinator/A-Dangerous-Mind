using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Door : MonoBehaviour, IInteractable
{
    private Animator animator;
    private bool opened;
    [SerializeField] private int doorNumber;
    [SerializeField] private bool unlocked;
    [SerializeField] private bool canOpen;
    [SerializeField] private bool openableByEnemy;
    [SerializeField] private AudioSource[] doorSounds;

    private MeshCollider col;

    private void Start()
    {
        animator = GetComponent<Animator>();
        col = GetComponent<MeshCollider>();
    }

    public void Interact(PlayerCharacter player)
    {
        if (!unlocked)
        {
            if (player != null)
            {
                foreach(var key in player.KeysInInventory)
                {
                    Debug.Log("tried to unlock");
                    if (key == doorNumber)
                    {
                        unlocked = true;
                        OpenDoor();
                        Debug.Log("unlocked");
                    }
                }
                doorSounds[2].Play();
            }
        }
        else
        {
            OpenDoor();
        }
    }

    private void OpenDoor()
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
        doorSounds[0].Play();
        canOpen = false;
        col.isTrigger = true;
        yield return new WaitForSeconds(1);
        canOpen = true;
        col.isTrigger = false;
    }

    public void UnlockDoor()
    {
        canOpen = true;
        OpenDoor();
    }

    public void EnemyOpen()
    {
        StartCoroutine(EnemyOpenDoor());
    }

    IEnumerator EnemyOpenDoor()
    {
        doorSounds[1].Play();
        yield return new WaitForSeconds(2);
        opened = false;
        doorSounds[1].Stop();
        Interact(null);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (openableByEnemy)
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
