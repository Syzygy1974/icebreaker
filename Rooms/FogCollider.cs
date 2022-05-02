using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogCollider : MonoBehaviour
{
    SpriteMask spriteMask;

    // Start is called before the first frame update
    void Awake()
    {
        spriteMask = GetComponentInChildren<SpriteMask> ();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision) {
        spriteMask.enabled = false;
    }
}
