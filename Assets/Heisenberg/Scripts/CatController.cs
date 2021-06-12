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
    private int lastLayer;

    const int REALITY1 = 7;
    const int REALITY2 = 8;
    const int MERGED = 9;
    Color PINK = new Color(255/255f, 136/255f, 179/255f, 50/255f);
    Color BLUE = new Color(0/255f, 203/255f, 255/255f, 50/255f);

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

        int currentLayer = Cat.layer;

        if( Cat.layer == MERGED )
        {
            Cat.layer = lastLayer;
        }
        else if( Cat.layer == REALITY1 )
        {
            Cat.layer = REALITY2;
            SetSpriteColorByLayer( REALITY1, BLUE );
            SetSpriteColorByLayer( REALITY2, Color.white );
        }
        else
        {
            Cat.layer = REALITY1;
            SetSpriteColorByLayer( REALITY2, PINK );
            SetSpriteColorByLayer( REALITY1, Color.white );
        }

        lastLayer = currentLayer;

        // Lerp between music channels
    }

    void SetSpriteColorByLayer( int Layer, Color color )
    {
        List<GameObject> Objects = new List<GameObject>();
        if( Layer == MERGED )
        {
            Objects.AddRange( GameObject.FindGameObjectsWithTag("Reality1") );
            Objects.AddRange( GameObject.FindGameObjectsWithTag("Reality2") );
        }
        else if( Layer == REALITY1 )
        {
            Objects.AddRange( GameObject.FindGameObjectsWithTag("Reality1") );
        }
        else
        {
            Objects.AddRange( GameObject.FindGameObjectsWithTag("Reality2") );
        }

        foreach( GameObject Object in Objects )
        {
            SpriteRenderer renderer = Object.GetComponent<SpriteRenderer>();
            renderer.color = color;
        }
    }
    void MakeOpaquetAndDecolorize( int Layer )
    {

    }

    bool CheckSafeToSwitch()
    {
        return true;
    }

    void MergeReality()
    {

    }
}
