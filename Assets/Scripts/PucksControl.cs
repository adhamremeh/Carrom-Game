using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PucksControl : MonoBehaviour
{

    void Update()
    {
        if (GetComponent<CircleCollider2D>().enabled == false)
        {
            Destroy(gameObject);
        }
    }

    public void destroyPuck()
    {
        GetComponent<Animator>().Play("PucksDestroy");

        Destroy(GetComponent<Rigidbody2D>());
    }
}
