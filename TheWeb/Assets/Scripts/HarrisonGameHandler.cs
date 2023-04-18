using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarrisonGameHandler : MonoBehaviour
{
    public int eggHealth = 100;

    public GameObject [] eggs;

    void Start(){
        foreach(GameObject egg in eggs) {
            egg.SetActive(false);
        }
        
        eggs[0].SetActive(true);
    }

    public void eggsGetHit(int damage){
        eggHealth -= damage;

        if ((eggHealth < 100) && (eggHealth > 80)) {
            eggs[0].SetActive(false);
            eggs[1].SetActive(true);
        } else if ((eggHealth <= 80) && (eggHealth > 60)) {
            eggs[1].SetActive(false);
            eggs[2].SetActive(true);
        } else if ((eggHealth <= 60) && (eggHealth > 40)) {
            eggs[2].SetActive(false);
            eggs[3].SetActive(true);                                   
        } else if ((eggHealth <= 40) && (eggHealth > 20)) {
            eggs[3].SetActive(false);
            eggs[4].SetActive(true);
        } else if ((eggHealth <= 20) && (eggHealth > 10)) {
            eggs[4].SetActive(false);
            eggs[5].SetActive(true);
        } else {
            eggHealth = 0;
            eggs[5].SetActive(false);
            // playerDies();
        }

        // if (damage > 0){
        // player.GetComponent<PlayerHurt>().playerHit();       //play GetHit animation
        // }
    }

    // public void playerDies(){
    //         player.GetComponent<PlayerHurt>().playerDead();       //play Death animation
    //         StartCoroutine(DeathPause());
    // }
}
