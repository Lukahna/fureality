using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    public string SceneToLoad;
    private Animator animator;
    private CatController kitty;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        kitty = FindObjectOfType<CatController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if( other.tag != "Reality1" )
            return;
        animator.SetBool("ReachGoal", true);
        kitty.transform.position = transform.position;
        kitty.enabled = false;
    }

    public void DisappearCat()
    {
        kitty.GetComponent<SpriteRenderer>().color = Color.clear;
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneToLoad);
    }
}
