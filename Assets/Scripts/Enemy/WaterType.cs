using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterType : MonoBehaviour
{
    [SerializeField]
    private Transform model;

    private Rigidbody2D rb;

    private bool flip;
    private bool walkRight;

    public float scalingSpeed;
    public float lamaJalan,lamaJalan2;
    public float minTinggi = 0.1f;
    public float maxTinggi = 3f;
    public float moveSpeed = 1f;

    private Vector3 min = new Vector3(1, 0.1f, 0);
    private Vector3 max = new Vector3(1, 3f, 0);
    // Start is called before the first frame update
    void Start()
    {
        model = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(FlipSize());
        StartCoroutine(FlipWalk());
    }

    // Update is called once per frame
    void Update()
    {
        //Buat ukuran
        if (flip == true)
        {
            Smaller();
        } else if(flip == false)
        {
            Bigger();
        }

        //Buat gerak kanan-kiri
        if (walkRight == true)
        {
            rb.velocity = new Vector2(moveSpeed, 0);
        } else if (walkRight == false)
        {
            rb.velocity = new Vector2(-moveSpeed, 0);
        }
        
    }

    void Smaller()
    {
        Vector3 newScale = new Vector3();
        newScale.x = Mathf.Clamp(model.localScale.x - scalingSpeed, min.x, max.x);
        newScale.y = Mathf.Clamp(model.localScale.y - scalingSpeed, minTinggi, maxTinggi);
        newScale.z = Mathf.Clamp(model.localScale.z - scalingSpeed, min.z, max.z);

        model.localScale = newScale;
    }

    void Bigger()
    {
        Vector3 newScale = new Vector3();
        newScale.x = Mathf.Clamp(model.localScale.x + scalingSpeed, min.x, max.x);
        newScale.y = Mathf.Clamp(model.localScale.y + scalingSpeed, minTinggi, maxTinggi);
        newScale.z = Mathf.Clamp(model.localScale.z + scalingSpeed, min.z, max.z);

        model.localScale = newScale;
    }

    private IEnumerator FlipSize()
    {
        while (true)
        {
            flip = false;
            yield return new WaitForSeconds(lamaJalan);
            flip = true;
            yield return new WaitForSeconds(lamaJalan);

        }

    }

    private IEnumerator FlipWalk()
    {
        while (true)
        {
            walkRight = false;
            yield return new WaitForSeconds(lamaJalan2);
            walkRight = true;
            yield return new WaitForSeconds(lamaJalan2);
        }
    }
}
