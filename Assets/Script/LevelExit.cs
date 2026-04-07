using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
public class LevelExit : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] float levelLoadDelay = 1f;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Something entered: " + other.name);

        if (other.CompareTag("Player"))
        {

            Debug.Log("Player reached exit");
            GameManager.Instance.Win();

        }
    }


}
