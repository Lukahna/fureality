using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CatController : MonoBehaviour
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
    protected const int REALITY1 = 7;
    protected const int REALITY2 = 8;
    protected const int MERGED = 9;
    AudioSource[] AudioSources;
    GameObject UI_WarningHolder;
    GameObject UI_InsideWall;
    GameObject UI_BoxSplitWarning;


    [HideInInspector]
    public List<RealityWarperBehavior> AllObjects = new List<RealityWarperBehavior>();

    void Awake()
    {
        AllObjects.AddRange(UnityEngine.Object.FindObjectsOfType<RealityWarperBehavior>() );
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        spriterRenderer = GetComponent<SpriteRenderer>();
        myRigidBody = GetComponent<Rigidbody2D>();
        Cat = GameObject.Find("Cat");
        itemAttachPoint = Cat.GetComponentInChildren<ItemAttach>();
        AudioSources = GetComponents<AudioSource>();
        UI_WarningHolder = GameObject.FindGameObjectWithTag("Warning");
        UI_WarningHolder.SetActive( false );
        UI_InsideWall = GameObject.FindGameObjectWithTag("InsideWallWarning");
        UI_InsideWall.SetActive( false );
        UI_BoxSplitWarning = GameObject.FindGameObjectWithTag("BoxSplitWarning");
        UI_BoxSplitWarning.SetActive( false );
    }

    // Update is called once per frame
    void Update()
    {
        change = Vector2.zero;
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");

        if( Input.GetButtonDown("Switch") )
        {
            SwitchRealityForAllObjects();
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

    void SwitchRealityForAllObjects()
    {
        if( !safeToSwitch )
        {
            StartCoroutine(ShowWarningsCo());
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
        }
        else
        {
            Cat.layer = REALITY1;
        }

        foreach( RealityWarperBehavior Object in AllObjects )
        {
            Object.SwitchReality( Cat.layer );
        }

        itemAttachPoint.UpdateLayer( Cat.layer );

        lastLayer = currentLayer;
        AltTrigger.layer = lastLayer;

        itemAttachPoint.CheckAndReleaseItem( Cat.layer );

        SwitchMusic();
    }

    void MergeRealityForAllObjects()
    {
        if( !safeToSwitch )
        {
            StartCoroutine(ShowWarningsCo());
            return;
        }

        if( Cat.layer == MERGED )
            return;

        lastLayer = Cat.layer;
        Cat.layer = MERGED;

        foreach( RealityWarperBehavior Object in AllObjects )
        {
            Object.MergeReality();
        }

        SwitchMusic();
    }

    void SwitchMusic()
    {
        if( Cat.layer == REALITY1 )
        {
            AudioSources[0].volume = 1;
            AudioSources[1].volume = 0;
            AudioSources[2].volume = 0;
            AudioSources[3].volume = 0;
        }
        else if( Cat.layer == REALITY2 )
        {
            AudioSources[0].volume = 0;
            AudioSources[1].volume = 1;
            AudioSources[2].volume = 0;
            AudioSources[3].volume = 0;
        }
        else if( Cat.layer == MERGED )
        {
            AudioSources[0].volume = 0;
            AudioSources[1].volume = 0;
            AudioSources[2].volume = 1;
            AudioSources[3].volume = 0;
        }
    }

    IEnumerator ShowWarningsCo()
    {
        UI_WarningHolder.SetActive( true );
        UI_InsideWall.SetActive( true );
        yield return new WaitForSeconds(1f);
        UI_WarningHolder.SetActive( false );
        UI_InsideWall.SetActive( false );
    }
}
