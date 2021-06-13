using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal_Final : MonoBehaviour
{
    public string SceneToLoad;
    [SerializeField] private float _sceneDelay = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    // Load the next level after a delay
    public void LoadNextLevel()
    {
        StartCoroutine(NextLevelCo());
    }

    IEnumerator NextLevelCo() {
        yield return new WaitForSeconds(_sceneDelay);
        SceneManager.LoadScene(SceneToLoad);
    }
}
