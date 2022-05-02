using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleItems {
        public string name;
        public int type;  //1: Cash, 2: Key.
        public int number;
        public int value;
    }

public class Item : MonoBehaviour
{
    private PlayerController2D playerController;
    public CollectibleItems item;

    void Awake()
    {
        playerController = FindObjectOfType<PlayerController2D>();
        item = new CollectibleItems();
        ExtendedAwake();
    }

    void OnTriggerEnter2D (Collider2D collidion) {
        playerController.GetItem(item);
        Destroy (gameObject);
    }
    
    public virtual void ExtendedAwake() {
    }

}