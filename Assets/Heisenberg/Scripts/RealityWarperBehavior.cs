using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RealityWarperBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    public int originalLayer;

    protected const int REALITY1 = 7;
    protected const int REALITY2 = 8;
    protected const int MERGED = 9;

    Color PINK = new Color(255/255f, 136/255f, 179/255f, 120/255f);
    Color BLUE = new Color(0/255f, 203/255f, 255/255f, 120/255f);
    void Start()
    {
        if( gameObject.tag == "Reality1" )
        {
            originalLayer = REALITY1;
        }
        else if( gameObject.tag == "Reality2" )
        {
            originalLayer = REALITY2;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchReality( int CatLayer )
    {
        gameObject.layer = originalLayer;
        if( CatLayer == originalLayer )
        {
            SetSpriteColor( gameObject, Color.white );
        }
        else
        {
            if( originalLayer == REALITY1 )
                SetSpriteColor( gameObject, BLUE );
            else
                SetSpriteColor( gameObject, PINK );
        }
    }

    public void MergeReality()
    {
        gameObject.layer = MERGED;
        SetSpriteColor( gameObject, Color.white );
    }

    void SetSpriteColor( GameObject Object, Color color )
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
