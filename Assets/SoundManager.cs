using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static AudioClip attack1, attack2, attack3, dash, jump, key, apiSound, airSound, tanahSound, sakit, walk;
    static AudioSource audioSrc;
    void Start()
    {
        attack1 = Resources.Load<AudioClip>("attack1");
        attack2 = Resources.Load<AudioClip>("attack 2");
        attack3 = Resources.Load<AudioClip>("attack3");
        dash = Resources.Load<AudioClip>("dash");
        jump = Resources.Load<AudioClip>("jump");
        key = Resources.Load<AudioClip>("key");
        apiSound = Resources.Load<AudioClip>("monster api");
        airSound = Resources.Load<AudioClip>("monster air");
        tanahSound = Resources.Load<AudioClip>("monster tanah");
        sakit = Resources.Load<AudioClip>("sakit");
        walk = Resources.Load<AudioClip>("walk");

        audioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void PlaySound(string clip)
    {
        switch (clip)
        {
            case "attack1":
                audioSrc.PlayOneShot(attack1, 0.3f);
                break;
            case "attack2":
                audioSrc.PlayOneShot(attack2, 0.3f);
                break;
            case "attack3":
                audioSrc.PlayOneShot(attack3, 0.3f);
                break;
            case "dash":
                audioSrc.PlayOneShot(dash, 0.3f);
                break;
            case "jump":
                audioSrc.PlayOneShot(jump, 0.3f);
                break;
            case "key":
                audioSrc.PlayOneShot(key, 0.3f);
                break;
            case "apiSound":
                audioSrc.PlayOneShot(apiSound, 0.31f);
                break;
            case "airSound":
                audioSrc.PlayOneShot(airSound, 0.3f);
                break;
            case "tanahSound":
                audioSrc.PlayOneShot(tanahSound, 0.3f);
                break;
            case "sakit":
                audioSrc.PlayOneShot(sakit, 0.3f);
                break;
            case "walk":
                audioSrc.PlayOneShot(walk, 0.3f);
                break;
        }
    }
}
