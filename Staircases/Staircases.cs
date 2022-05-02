using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Staircases : MonoBehaviour
{
    private Collider2D staircasesCollider;
    // Start is called before the first frame update
    void Awake()
    {
        staircasesCollider = GetComponent<Collider2D>();
   
    }


    // void OnCollisionEnter2D(Collision2D collision) {

    //     Debug.Log ("STAIRCASES COLLIDER");
    //     Debug.Log (collision.transform.gameObject);
    //     collision.transform.gameObject.SendMessage ("UpdateFriction");
    // }
}
