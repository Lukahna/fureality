using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealitySwitcher : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        
    }

    //// Update is called once per frame
    //void Update()
    //{
        
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Box>(out var comp))
        {
            //other.gameObject.layer.;
            //SwitchTo();
        }
    }

    public void SwitchTo(int realityLayerNumber, GameObject pushBox)
    {
        pushBox.layer = realityLayerNumber;

        if (realityLayerNumber == 1)
        {

        }
        else
        {

        }
    }
}
