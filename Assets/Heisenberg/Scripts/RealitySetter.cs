using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealitySetter : MonoBehaviour
{
    // Start is called before the first frame update
    private void AddDescendantsWithTag( Transform parent )
    {
        foreach (Transform child in parent)
        {
            child.gameObject.tag = gameObject.tag;
            child.gameObject.layer = gameObject.layer;
            child.GetComponent<RealityWarperBehavior>().SwitchReality( 7 );

            AddDescendantsWithTag(child );
        }
    }
     
    void Start()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.tag = gameObject.tag;
            child.gameObject.layer = gameObject.layer;
            child.GetComponent<RealityWarperBehavior>().SwitchReality( 7 );
        }

        AddDescendantsWithTag(transform);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
