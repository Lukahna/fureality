using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splitter : MonoBehaviour
{
    [SerializeField]
    private GameObject m_PushboxPrefab;
    
    private GameObject m_cloneReality1;
    private GameObject m_cloneReality2;

    private int m_otherObjectLayerNumber;

    private GameObject m_otherGameObj;

    private RealityWarperBehavior m_realityWarper;

    private const int REALITY1 = 7;
    private const int REALITY2 = 8;
    private const int MERGED = 9;

    private CatController kitty;

    // Start is called before the first frame update
    void Start()
    {
        kitty = FindObjectOfType<CatController>();
    }

    //// Update is called once per frame
    //void Update()
    //{

    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Box>(out var comp))
        {
            Debug.Log("collision.name = " + collision.name);

            m_otherObjectLayerNumber = collision.gameObject.layer;
            m_otherGameObj = collision.gameObject;
            SplitFrom(m_otherObjectLayerNumber, m_otherGameObj);
        }
    }

    public void SplitFrom(int realityLayerNumber, GameObject existingPushbox)
    {
        if (existingPushbox.name.ToString().Contains("m_cloneReality") )
        {
            Debug.Log("Cannot split because already split!!!");
            return;
        }

        //if (realityLayerNumber == MERGED)
        //{
        //    if (existingPushbox.tag == "Reality2")
        //    {
        //        m_cloneReality1 = Instantiate(m_PushboxPrefab);
        //        m_cloneReality1.layer = REALITY1;
        //        m_cloneReality1.tag = "Reality1";

        //        m_cloneReality2 = Instantiate(m_PushboxPrefab);
        //        m_cloneReality2.layer = REALITY2;
        //        m_cloneReality2.tag = "Reality2";
        //    }
        //    else
        //    {
        //        existingPushbox.tag = "Reality2";
        //    }
        //    existingPushbox.SetActive(false);

        //    kitty.GetComponentInChildren<ItemAttach>().CheckAndReleaseItem(MERGED);
        //}
        //else if (realityLayerNumber == REALITY1)
        //{
        //    existingPushbox.layer = REALITY2;
        //    existingPushbox.tag = "Reality2";
        //    kitty.GetComponentInChildren<ItemAttach>().CheckAndReleaseItem(REALITY2);
        //    existingPushbox.GetComponent<RealityWarperBehavior>().SwitchReality(kitty.gameObject.layer);
        //}
        //else
        //{
        //    existingPushbox.layer = REALITY1;
        //    existingPushbox.tag = "Reality1";
        //    kitty.GetComponentInChildren<ItemAttach>().CheckAndReleaseItem(REALITY1);
        //    existingPushbox.GetComponent<RealityWarperBehavior>().SwitchReality(kitty.gameObject.layer);
        //}

        m_cloneReality1 = Instantiate(m_PushboxPrefab);
        m_cloneReality1.name = "m_cloneReality1";
        m_cloneReality1.layer = REALITY1;
        m_cloneReality1.tag = "Reality1";
        m_cloneReality1.GetComponent<RealityWarperBehavior>().SwitchReality(kitty.gameObject.layer);
        kitty.AllObjects.Add(m_cloneReality1.gameObject.GetComponent<RealityWarperBehavior>());

        m_cloneReality2 = Instantiate(m_PushboxPrefab);
        m_cloneReality2.name = "m_cloneReality2";
        m_cloneReality2.layer = REALITY2;
        m_cloneReality2.tag = "Reality2";
        m_cloneReality2.GetComponent<RealityWarperBehavior>().SwitchReality(kitty.gameObject.layer);
        kitty.AllObjects.Add(m_cloneReality2.gameObject.GetComponent<RealityWarperBehavior>());

        existingPushbox.SetActive(false);

        //kitty.GetComponentInChildren<ItemAttach>().CheckAndReleaseItem(MERGED);

        float reality1XPos = this.transform.position.x;
        float reality1YPos = this.transform.position.y;
        m_cloneReality1.transform.position = new Vector2(reality1XPos + 1, reality1YPos);

        float reality2XPos = this.transform.position.x;
        float reality2YPos = this.transform.position.y;
        m_cloneReality2.transform.position = new Vector2(reality2XPos - 1, reality2YPos);
    }
}
