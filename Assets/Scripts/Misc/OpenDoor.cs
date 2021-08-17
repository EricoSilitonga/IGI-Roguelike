using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{

    private bool playerDetected;
    public Transform doorPos;
    public float width;
    public float height;
    public LayerMask whatIsPlayer;

    Switch sceneSwitch;

    [SerializeField]
    private string sceneName;
    // Start is called before the first frame update
    private void Start()
    {
        sceneSwitch = FindObjectOfType<Switch>();
    }

    // Update is called once per frame
    private void Update()
    {
        playerDetected = Physics2D.OverlapBox(doorPos.position, new Vector2(width, height),0,whatIsPlayer);

        if (playerDetected)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                sceneSwitch.SwitchScene(sceneName);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(doorPos.position, new Vector3(width, height, 1));
    }
}
