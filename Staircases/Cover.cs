using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cover : MonoBehaviour
{
    Collider2D m_Collider;
    // Start is called before the first frame update
    void Awake()
    {
        m_Collider = GetComponent<Collider2D>();
    }

    void EnableCover()
    {
        m_Collider.enabled = true;
    }

    void DisableCover()
    {
        m_Collider.enabled = false;
    }
}

