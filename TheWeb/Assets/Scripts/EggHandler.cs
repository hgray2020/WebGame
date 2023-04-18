using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggHandler : MonoBehaviour
{
    private string detectionTag = "ant";
    private int eggHealth;
    
    // Start is called before the first frame update
    void Start()
    {
        eggHealth = 10;
    }

    // Update is called once per frame
    void Update()
    {
        if(eggHealth == 0){
                print("egg dead");
        }
    }
        private void OnTriggerEnter2D(Collider2D collision){
                print("in here");
                if (collision.CompareTag(detectionTag)){
                        // Destroy(collision.gameObject);
                        eggHealth--;
                        print("collided");
                }
        }
}