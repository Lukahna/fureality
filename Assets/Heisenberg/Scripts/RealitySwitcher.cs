using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealitySwitcher : MonoBehaviour
{
    private int m_otherObjectLayerNumber;

    private GameObject m_otherGameObj;

    private RealityWarperBehavior m_realityWarper;

    private const int REALITY1 = 7;
    private const int REALITY2 = 8;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    //// Update is called once per frame
    //void Update()
    //{

    //}

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    Debug.Log("collision.name = " + collision.gameObject.name);

    //    if (collision.gameObject.TryGetComponent<Box>(out var comp))
    //    {
    //        //other.gameObject.layer.;
    //        //SwitchTo();
    //        Debug.Log("collision.name = " + collision.gameObject.name);
    //    }
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Box>(out var comp))
        {
            Debug.Log("collision.name = " + collision.name);

            m_otherObjectLayerNumber = collision.gameObject.layer;
            m_otherGameObj = collision.gameObject;
            SwitchTo(m_otherObjectLayerNumber, m_otherGameObj);
        }
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.TryGetComponent<Box>(out var comp))
    //    {
    //        //other.gameObject.layer.;
    //        //SwitchTo();
    //        Debug.Log("other.name = " + other.name);
    //    }
    //}

    public void SwitchTo(int realityLayerNumber, GameObject pushBox)
    {
        if (realityLayerNumber == REALITY1)
        {
            pushBox.layer = REALITY2;
        }
        else
        {
            pushBox.layer = REALITY1;
        }
    }
}
