using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LaserToggle_FSM : MonoBehaviour
{
    #region Variables
    [HideInInspector]
    public Collider2D m_ColliderOfOther;

    [SerializeField]
    private int m_connectedLaserID;

    //[SerializeField]
    //private GameObject m_connectedLaserGO;

    #region Button Sprite
    private SpriteRenderer m_spriteRenderer;

    [SerializeField]
    private GameObject m_notPressedButtonGO;

    [SerializeField]
    private GameObject m_pressedButtonGO;
    #endregion

    //[SerializeField]
    //Collider2D m_laserCollider;
    //public Collider2D m_LaserCollider
    //{
    //    get { return m_laserCollider; }
    //    set { m_laserCollider = value; }
    //}
    #endregion

    #region State Machine-related
    private LaserToggle_BaseState currentState;

    public LaserToggle_BaseState CurrentState
    {
        get { return currentState; }
    }

    public readonly LaserToggleNotPressedState NotPressedState = new LaserToggleNotPressedState();
    public readonly LaserTogglePressedState PressedState = new LaserTogglePressedState();
    #endregion

    private void Awake()
    {
        m_spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        TransitionToState(NotPressedState);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        m_ColliderOfOther = collision;

        if (collision.GetComponent<IInteractable>() != null)
        {
            currentState.OnTriggerEnterState(this);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        m_ColliderOfOther = collision;

        if (collision.GetComponent<IInteractable>() != null)
        {
            currentState.OnTriggerExitState(this);
        }
    }

    public void TransitionToState(LaserToggle_BaseState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }

    public void WeightOnButton()
    {
        //m_spriteRenderer.sprite = m_pressedButtonGO;

        EventsBroker.PressToggle(m_connectedLaserID);
    }

    public void NoWeightOnButton()
    {
        //m_spriteRenderer.sprite = m_notPressedButtonGO;

        EventsBroker.StopPressToggle(m_connectedLaserID);
    }
}
