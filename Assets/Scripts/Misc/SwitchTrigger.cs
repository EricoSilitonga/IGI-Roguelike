using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchTrigger : MonoBehaviour
{
    [SerializeField]
    private string sceneName;
    Switch sceneSwitch;
    // Start is called before the first frame update
    private void Start()
    {
        sceneSwitch = FindObjectOfType<Switch>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            sceneSwitch.SwitchScene(sceneName);
        }
    }
}
