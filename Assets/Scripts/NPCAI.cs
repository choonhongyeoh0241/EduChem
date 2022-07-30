using UnityEngine;
using System.Collections.Generic;

public class NPCAI : MonoBehaviour
{ 
    private Vector3 directionVector;
    private new Transform transform;
    private new Rigidbody2D rigidbody;
    private Animator animator;
    [SerializeField] private float speed = 5;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private bool playerInRange;
    [SerializeField] private GameObject Bubble;
    [SerializeField] private DialogueData dialogue;
    private void Start()
    {
        transform = GetComponent<Transform>();
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        ChangeDirection();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && playerInRange)
        {
            Bubble.SetActive(false);
            DialogueManager.RequestDialogue(dialogue);
        }
    }

    private void FixedUpdate()
    {
        if (!playerInRange && directionVector!=Vector3.zero)
        {
            Vector3 tempPos = transform.position + directionVector * speed * Time.deltaTime;
            if (boxCollider.bounds.Contains(tempPos)) 
            {
                rigidbody.MovePosition(tempPos); 
            }
            else 
            {
                ChangeDirection();
            }
        }
    }

    private void ChangeDirection()
    {
        int direction = Random.Range(0, 4);
        switch(direction)
        {
            case 0:
                directionVector = Vector3.right;
                break;
            case 1:
                directionVector = Vector3.up;
                break;
            case 2:
                directionVector = Vector3.left;
                break;
            case 3:
                directionVector = Vector3.down;
                break;
            default:
                break;

        }

        UpdateAnimation();
    }

    private void UpdateAnimation()
    {
        if (directionVector != Vector3.zero)
        {
            animator.SetFloat("MoveX", directionVector.x);
            animator.SetFloat("MoveY", directionVector.y);
        } 
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        Vector3 tempPos = directionVector;
        ChangeDirection();

        int loops = 0;

        while(tempPos == directionVector && loops < 100)
        {
            loops++;
            ChangeDirection();
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(!other.isTrigger && other.CompareTag("Player"))
        {
            Bubble.SetActive(true);
            playerInRange = true;
        }
        
        if (other.CompareTag("Obstacle"))
        {
            ChangeDirection();
        }
        
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if(!other.isTrigger && other.CompareTag("Player"))
        {
            Bubble.SetActive(false);
            playerInRange = false;
        }
    }
}
