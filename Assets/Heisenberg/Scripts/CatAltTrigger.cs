using UnityEngine;

public class CatAltTrigger : MonoBehaviour
{
    private CatController parent;
    void Start()
    {
        parent = transform.parent.GetComponent<CatController>();
    }

    void FixedUpdate() 
    {
        transform.position = transform.parent.transform.position;
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if( other.CompareTag("Reality1") || other.CompareTag("Reality2") )
        {
            parent.safeToSwitch = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if( other.CompareTag("Reality1") || other.CompareTag("Reality2") )
        {
            parent.safeToSwitch = false;
        }
    }
}
