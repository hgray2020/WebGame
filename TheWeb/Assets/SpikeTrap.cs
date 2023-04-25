using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void onTriggerEnter2D(Collider2D other) {
        Debug.Log("Hit");
        if (other.tag == "ant") {
            other.gameObject.BroadcastMessage("takeDamage", 5);
        }
    }
}
