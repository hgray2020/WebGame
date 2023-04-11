using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class AntMiddle : MonoBehaviour {
    
       public float speed;
       public Transform[] moveSpots;
       public Transform antNPC;
       private int randomSpot;
       private string detectionTag = "player";
       
        void Start(){
                randomSpot = Random.Range(0, moveSpots.Length);
        }
       
        void Update(){
                transform.position = Vector2.MoveTowards(transform.position, moveSpots[randomSpot].position, speed * Time.deltaTime);
                if (Vector2.Distance(transform.position, moveSpots[randomSpot].position) < 0.2f){
                        randomSpot = Random.Range(0, moveSpots.Length);
                }
                
        
        }

        private void OnTriggerEnter2D(Collider2D collision) {
                if (collision.CompareTag(detectionTag))
                {
                        //do something in our game
                }
        }
     
}