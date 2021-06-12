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
        parent.safeToSwitch = true;
    }
    private void OnTriggerStay2D(Collider2D other) 
    {
        parent.safeToSwitch = false;
    }
}
