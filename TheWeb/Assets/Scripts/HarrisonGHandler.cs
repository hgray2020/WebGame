using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HarrisonGHandler : MonoBehaviour {

        private GameObject eggs;
        public static int eggHealth = 100;

        public GameObject egg1;
        public GameObject egg2;
        public GameObject egg3;
        public GameObject egg4;
        public GameObject egg5;

        void Start(){
                eggs = GameObject.FindWithTag("Eggs");
                egg5.SetActive(true);
        }

        public void eggsGetHit(int damage){
                eggHealth -= damage;

                if ((eggHealth <= 80) && (eggHealth > 60)) {
                        egg5.SetActive(false);
                        egg4.SetActive(true);
                } else if ((eggHealth <= 60) && (eggHealth > 40)) {
                        egg4.SetActive(false);
                        egg3.SetActive(true);                                   
                } else if ((eggHealth <= 40) && (eggHealth > 20)) {
                        egg3.SetActive(false);
                        egg2.SetActive(true);
                } else if ((eggHealth <= 20) && (eggHealth > 10)) {
                        egg2.SetActive(false);
                        egg1.SetActive(true);
                } else {
                        eggHealth = 0;
                        egg1.SetActive(false);
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