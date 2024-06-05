using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameClearManagerScript : MonoBehaviour
{
    [SerializeField] private string gameScene;
    [SerializeField] private string titleScene;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(gameScene);
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            SceneManager.LoadScene(titleScene);
        }
    }
}
