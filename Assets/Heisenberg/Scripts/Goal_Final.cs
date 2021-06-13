using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal_Final : MonoBehaviour
{
    public string SceneToLoad;
    private CatController kitty;
    [SerializeField] private float _sceneDelay = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        kitty = FindObjectOfType<CatController>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    // Load the next level after a delay
    public void LoadNextLevel()
    {
        //StartCoroutine(NextLevelCo());
        SceneManager.LoadScene(SceneToLoad);
    }

    IEnumerator NextLevelCo() {
        yield return new WaitForSeconds(_sceneDelay);
        SceneManager.LoadScene(SceneToLoad);
    }
}
