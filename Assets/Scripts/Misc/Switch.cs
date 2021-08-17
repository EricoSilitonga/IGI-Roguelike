using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Switch : MonoBehaviour
{

    public static string prevScene;
    public static string currentScene;
    // Start is called before the first frame update
    public virtual void Start()
    {
        currentScene = SceneManager.GetActiveScene().name;       
    }

    // Update is called once per frame
    public void SwitchScene(string sceneName)
    {
        prevScene = currentScene;
        SceneManager.LoadScene(sceneName);
    }
}
