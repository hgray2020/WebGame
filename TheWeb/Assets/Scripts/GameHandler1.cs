// using System.Collections.Generic;
// using System.Collections;
// using UnityEngine;
// using UnityEngine.UI;
// using UnityEngine.SceneManagement;

// public class GameHandler1 : MonoBehaviour {

//     public static int playerHearts = 5;
//     public GameObject heart1;
//     public GameObject heart2;
//     public GameObject heart3;
//     public GameObject heart4;
//     public GameObject heart5;
//     public static float heart1fill = 1.0f;
//     public static float heart2fill = 1.0f;
//     public static float heart3fill = 1.0f;
//     public static float heart4fill = 1.0f;
//     public static float heart5fill = 1.0f;

//     public static int playerHealth = 100;
//     public int StartPlayerHealth = 100;

// //     void Start(){
// //         playerHealth = StartPlayerHealth;
// //         updateStatsDisplay();

// //         heart1.SetActive(true);
// //         heart2.SetActive(true);
// //         heart3.SetActive(true);
// //         heart4.SetActive(true);
// //         heart5.SetActive(true);
// //         Debug.Log("pHealth = " + playerHealth + ". Hearts = " + playerHearts + ". h1fill = " + heart1fill + ". h2fill = " + heart2fill + ". h3fill = " + heart3fill );
// //     }

//     public void playerGetHit(int damage){
//         if (isDefending == false){
//             playerHealth -= damage;
//             if (playerHealth > 0){
//                 if (playerHearts == 3){
//                         heart3fill = (float)playerHealth / StartPlayerHealth;
//                 }
//                 else if (playerHearts == 2){
//                         heart2fill = (float)playerHealth / StartPlayerHealth;
//                 }
//                 else if (playerHearts == 1){
//                         heart1fill = (float)playerHealth / StartPlayerHealth;
//                 }
//                 Debug.Log("pHealth = " + playerHealth + ". Hearts = " + playerHearts + ". h1fill = " + heart1fill + ". h2fill = " + heart2fill + ". h3fill = " + heart3fill );
//                 updateStatsDisplay();
//             }

//             if (damage > 0){
//                 player.GetComponent<PlayerHurt>().playerHit();      //play GetHit animation
//             }
//         }

//         if (playerHealth <= 0){
//             playerHealth = 0;
//             playerHearts -=1;
//             updateStatsDisplay();
//             playerHealth = StartPlayerHealth;
//         }

//         if (playerHearts <= 0){
//             playerDies();
//         }

//         //if (playerHealth > StartPlayerHealth){
//         //       playerHealth = StartPlayerHealth;
//         //       updateStatsDisplay();
//         //}
//       }

//       public void updateStatsDisplay(){
//             if (playerHearts == 3){
//                   heart3.GetComponent<Image>().fillAmount = heart3fill;
//             }
//             if (playerHearts == 4){
//                   heart4.GetComponent<Image>().fillAmount = heart4fill;
//             }
//             if (playerHearts == 3){
//                   heart3.GetComponent<Image>().fillAmount = heart3fill;
//             }
//             else if (playerHearts == 2){
//                   heart3.SetActive(false);
//                   heart2.GetComponent<Image>().fillAmount = heart2fill;
//             }
//             else if (playerHearts == 1){
//                   heart2.SetActive(false);
//                   heart1.GetComponent<Image>().fillAmount = heart1fill;
//             }
//             else if (playerHearts == 0){
//                   heart1.SetActive(false);
//             }

//             //Text healthTextTemp = healthText.GetComponent<Text>();
//             // healthTextTemp.text = "HEALTH: " + playerHealth;

//             Text tokensTextTemp = tokensText.GetComponent<Text>();
//             tokensTextTemp.text = "GOLD: " + gotTokens;
//       }

//       public void playerDies(){
//             player.GetComponent<PlayerHurt>().playerDead();      //play Death animation
//             StartCoroutine(DeathPause());
//       }

//       IEnumerator DeathPause(){
//             player.GetComponent<PlayerMove>().isAlive = false;
//             player.GetComponent<PlayerJump>().isAlive = false;
//             yield return new WaitForSeconds(1.0f);
//             SceneManager.LoadScene("EndLose");
//       }

//       public void StartGame() {
//             SceneManager.LoadScene("Level1");
//       }

//       public void RestartGame() {
//             SceneManager.LoadScene("MainMenu");
//                 // Please also reset all static variables here, for new games!
//             playerHealth = StartPlayerHealth;
//       }

//       public void QuitGame() {
//                 #if UNITY_EDITOR
//                 UnityEditor.EditorApplication.isPlaying = false;
//                 #else
//                 Application.Quit();
//                 #endif
//       }

//       public void Credits() {
//             SceneManager.LoadScene("Credits");
//       }
// }
