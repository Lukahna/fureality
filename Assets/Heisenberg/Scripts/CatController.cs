using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CatController : RealityWarperBehavior
{
    public float speed;
    public GameObject AltTrigger;
    public bool safeToSwitch; // USED IN ALT TRIGGER
    private Rigidbody2D myRigidBody;
    private Vector2 change;
    private Animator animator;
    private SpriteRenderer spriterRenderer;
    private GameObject Cat;
    private BoxCollider2D AltTriggerBox;
    private int lastLayer;
    private ItemAttach itemAttachPoint;

    Color PINK = new Color(255/255f, 136/255f, 179/255f, 120/255f);
    Color BLUE = new Color(0/255f, 203/255f, 255/255f, 120/255f);
    AudioSource BlueLoop;
    AudioSource PinkLoop;

    RealityWarperBehavior[] AllObjects;

    void Awake()
    {
        AllObjects = UnityEngine.Object.FindObjectsOfType<RealityWarperBehavior>();
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        spriterRenderer = GetComponent<SpriteRenderer>();
        myRigidBody = GetComponent<Rigidbody2D>();
        Cat = GameObject.Find("Cat");
        itemAttachPoint = Cat.GetComponentInChildren<ItemAttach>();

        Cat.layer = REALITY2;
        SwitchReality();
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
            MergeRealityForAllObjects();
        }
    }

    void FixedUpdate() 
    {
        AnimateAndMove();
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
        if( !safeToSwitch )
        {
            // Shake Screen and Make Sound
            return;
        }

        int currentLayer = Cat.layer;

        if( Cat.layer == MERGED )
        {
            Cat.layer = lastLayer;

            if( Cat.layer == REALITY2 )
            {
                SetSpriteColorByLayer( REALITY1, BLUE );
                SetSpriteColorByLayer( REALITY2, Color.white );
            }
            else
            {
                SetSpriteColorByLayer( REALITY2, PINK );
                SetSpriteColorByLayer( REALITY1, Color.white );
            }
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

        itemAttachPoint.UpdateLayer( Cat.layer );

        lastLayer = currentLayer;
        AltTrigger.layer = lastLayer;

        itemAttachPoint.CheckAndReleaseItem( currentLayer );

        // Lerp between music channels
        // StartCoroutine( FadeAudioSource.StartFade(BlueLoop, 2, 0) );
        // StartCoroutine( FadeAudioSource.StartFade(PinkLoop, 2, 1) );
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
            if( renderer != null )
            {
                renderer.color = color;
            }

            Tilemap tilemap = Object.GetComponent<Tilemap>();
            if( tilemap != null )
            {
                tilemap.color = color;
            }
        }
    }

    void MergeRealityForAllObjects()
    {
        if( !safeToSwitch || Cat.layer == MERGED)
        {
            // Shake Screen and Make Sound
            return;
        }

        lastLayer = Cat.layer;
        Cat.layer = MERGED;

        foreach( RealityWarperBehavior Object in AllObjects )
        {
            Object.MergeReality();
        }
    }
}
