using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.U2D.Animation;

public class RightHandGreeting : MonoBehaviour
{
    private SpriteResolver spriteResolver;

    // Start is called before the first frame update
    void Start()
    {
        spriteResolver = GetComponent<SpriteResolver>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeHand()
    {
        spriteResolver.SetCategoryAndLabel("hand", "saludando");
        spriteResolver.ResolveSpriteToSpriteRenderer();
    }


}
