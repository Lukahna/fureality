using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LaserGate_FSM : MonoBehaviour
{
    #region Variables
    [SerializeField]
    private int m_idNumber;

    //[SerializeField]
    //private LaserToggle_FSM m_connectedLaserToggle;

    //[SerializeField]
    //private Collider2D m_laserCollider;
    //public Collider2D m_LaserCollider
    //{
    //    get { return m_laserCollider; }
    //    set { m_laserCollider = value; }
    //}

    [SerializeField]
    private GameObject m_laserOnGameObject;

    [SerializeField]
    private GameObject m_laserOffGameObject;

    //private SpriteRenderer m_laserGateSprite;
    #endregion

    #region State Machine-related
    private Laser_BaseState currentState;

    public Laser_BaseState CurrentState
    {
        get { return currentState; }
    }

    public readonly LaserActiveState ActiveLaserState = new LaserActiveState();
    public readonly LaserDeactivedState DeactivedLaserState = new LaserDeactivedState();
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        EventsBroker.OnPressLaserToggle += EventsBroker_OnPressLaserToggle;

        EventsBroker.OnStopPressLaserToggle += EventsBroker_OnStopPressLaserToggle;

        TransitionToState(ActiveLaserState);
    }

    public void EventsBroker_OnPressLaserToggle(int id)
    {
        if (id == this.m_idNumber)
        {
            DeactivateLaser();
        }
    }

    public void EventsBroker_OnStopPressLaserToggle(int id)
    {
        if (id == this.m_idNumber)
        {
            ActivateLaser();
        }
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState(this);
    }

    private void OnCollisionEnter(Collision collision)
    {
        currentState.OnCollisionEnterState(this);
    }

    public void TransitionToState(Laser_BaseState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }

    public void DeactivateLaser()
    {
        //if (this.m_laserCollider != null && this.m_laserCollider.isActiveAndEnabled == true)
        //{
        //    m_laserCollider.enabled = false;
        //    m_laserGateSprite.enabled = false;
        //}
        //m_laserCollider.enabled = false;
        //m_laserGateSprite.enabled = false;
        m_laserOnGameObject.SetActive(false);
        m_laserOffGameObject.SetActive(true);
    }

    public void ActivateLaser()
    {
        //if (this.m_laserCollider != null && this.m_laserCollider.isActiveAndEnabled == false)
        //{
        //    m_laserCollider.enabled = true;
        //    m_laserGateSprite.enabled = true;
        //}
        //m_laserCollider.enabled = true;
        //m_laserGateSprite.enabled = true;
        m_laserOnGameObject.SetActive(true);
        m_laserOffGameObject.SetActive(false);
    }
}
