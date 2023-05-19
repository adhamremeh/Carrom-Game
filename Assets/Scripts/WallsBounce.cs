using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallsBounce : MonoBehaviour
{

    public Vector3 multiplyVal;

    void OnTriggerEnter2D(Collider2D coll)
    {
        coll.gameObject.GetComponent<Rigidbody2D>().velocity = coll.gameObject.GetComponent<Rigidbody2D>().velocity * multiplyVal;
    }
}
