using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealityWarperBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    public int originalLayer;

    protected const int REALITY1 = 7;
    protected const int REALITY2 = 8;
    protected const int MERGED = 9;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MergeReality()
    {
        gameObject.layer = MERGED;
    }

    public void SplitReality()
    {
        gameObject.layer = originalLayer;
    } 
}
