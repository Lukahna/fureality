using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAttach : MonoBehaviour
{
    GameObject _AttachableItem;
    GameObject AttachedItem;

    Transform AttachedItemsPreviousParent;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if( Input.GetButtonDown("Grab") )
        {
            AttachableItemInteract();
        }
    }

    void AttachableItemInteract()
    {
        if( AttachedItem == null )
        {
            if( _AttachableItem != null )
            {
                AttachItem();
            }
        }
        else
        {
            ReleaseItem();
        }
    }

    void AttachItem()
    {
        AttachedItem = _AttachableItem;

        AttachedItemsPreviousParent = AttachedItem.transform.parent;
        AttachedItem.transform.parent = transform;
        AttachedItem.transform.localPosition = new Vector3(0,0,0);
    }

    void ReleaseItem()
    {
        AttachedItem.transform.parent = AttachedItemsPreviousParent;
        AttachedItem = null;
    }

    public void CheckAndReleaseItem( int Layer )
    {
        if( AttachedItem != null && AttachedItem.layer == Layer )
            ReleaseItem();
    }

    public void UpdateLayer( int Layer )
    {
        gameObject.layer = Layer;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Box>() != null && _AttachableItem == null)
        {
            _AttachableItem = other.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if (other.GetComponent<Box>() != null && _AttachableItem == other.gameObject)
        {
            _AttachableItem = null;
        }
    }
}
