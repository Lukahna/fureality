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
