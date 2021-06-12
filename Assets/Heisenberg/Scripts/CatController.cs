using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    walk,
    attack,
    interact
}
public class CatController : MonoBehaviour
{
    public PlayerState currentState;
    public float speed;
    private Rigidbody2D myRigidBody;
    private Vector2 change;
    private Animator animator;
    private SpriteRenderer spriterRenderer;
    private GameObject Cat;

    void Start()
    {
        animator = GetComponent<Animator>();
        spriterRenderer = GetComponent<SpriteRenderer>();
        myRigidBody = GetComponent<Rigidbody2D>();
        Cat = GameObject.Find("Cat");
        currentState = PlayerState.walk;
    }

    // Update is called once per frame
    void Update()
    {
        change = Vector2.zero;
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");

        if( Input.GetButtonDown("Switch") )
        {
            SwitchReality();
        }

        if( Input.GetButtonDown("Merge") )
        {
            MergeReality();
        }
    }

    void FixedUpdate() 
    {
        if( currentState == PlayerState.walk )
        {
            AnimateAndMove();
        }
    }

    void MoveCharacter()
    {
        myRigidBody.MovePosition(myRigidBody.position + change.normalized * speed * Time.deltaTime);
    }

    void AnimateAndMove()
    {
        if( change != Vector2.zero )
        {
            MoveCharacter();
            animator.SetFloat("moveX", change.x);
            animator.SetFloat("moveY", change.y);
            animator.SetBool("moving", true);

            if( change.x < 0 )
            {
                spriterRenderer.flipX = true;
            }
            else
            {
                spriterRenderer.flipX = false;
            }
        }
        else
        {
            animator.SetBool("moving", false);
        }
    }

    void SwitchReality()
    {
        if( !CheckSafeToSwitch() )
        {
            // Shake Screen and Make Sound
            return;
        }

        string LayerToSwitchTo = "Reality1";

        if( LayerMask.LayerToName( Cat.layer) == LayerToSwitchTo )
        {
            LayerToSwitchTo = "Reality2";
        }

        Cat.layer = LayerMask.NameToLayer( LayerToSwitchTo );
        // Swap All Collision 
        // Change Colors/alpha
        // Lerp between music channels
    }

    private bool CheckSafeToSwitch()
    {
        return true;
    }

    void MergeReality()
    {

    }
}
