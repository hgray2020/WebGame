using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// public class InventoryItem {
//       public GameObject image { get; set; }
//       public int amount { get; set; }
//       public int price { get; set; }
//       public GameObject bg { get; set; }

//       public InventoryItem() {
            
//       }

// }

public class GameInventory : MonoBehaviour {
      public GameObject InventoryMenu;
      public bool InvIsOpen = true;
      
      // private InventoryItem[] items; 

      //5 Inventory Items:
      public static bool item1bool = false;
      public static bool item2bool = false;
      public static bool item3bool = false;

      public static int item1num = 0;
      public static int item2num = 0;
      public static int item3num = 0;
      public int coins = 5;

      public int[] prices = {1, 2, 3};

      [Header("Add item image objects here")]
      public GameObject item1image;
      public GameObject item2image;
      public GameObject item3image;
      public GameObject coinText;

      public GameObject item1bg;
      public GameObject item2bg;
      public GameObject item3bg;

      // Item number text variables. Comment out if each item is unique (1/2).
      [Header("Add item number Text objects here")]
      // public Text item1Text;
      // public Text item2Text;
      // public Text item3Text;

      public Text item1Text_price;
      public Text item2Text_price;
      public Text item3Text_price;
      public Text item4Text_price;
      public bool spiderInv = false;
      public string coin;
      int selected;
 
      void Start(){
            InventoryMenu.SetActive(true);
            InvIsOpen = true;
            InventoryDisplay();
            item1bg.GetComponent<Image>().color = new Color (0, 0, 0, 1);
      }

      void Update(){
            if (Input.GetButtonDown("WebBuild1")) { 
                  item1bg.GetComponent<Image>().color = new Color (0, 0, 0, 1);
                  selected = 0;
                  ChangeAllColor(selected);
            }
            if (Input.GetButtonDown("WebBuild2")) { 
                  item2bg.GetComponent<Image>().color = new Color (0, 0, 0, 1);
                  selected = 1;
                  ChangeAllColor(selected);
            }
            if (Input.GetButtonDown("WebBuild3")) { 
                  item3bg.GetComponent<Image>().color = new Color (0, 0, 0, 1);
                  selected = 2;
                  ChangeAllColor(selected);
            }
            if (Input.GetButtonDown("Inventory")) {
                  OpenCloseInventory();
            }
      }

      void InventoryDisplay(){
            item1image.SetActive(true);
            item2image.SetActive(true);
            item3image.SetActive(true);

            Text coinTextB = coinText.GetComponent<Text>();
            coinTextB.text = (coin + coins);

            // Item number updates. Comment out if each item is unique (2/2).
            // Text item1TextB = item1Text.GetComponent<Text>();
            // item1TextB.text = ("" + item1num);

            // Text item2TextB = item2Text.GetComponent<Text>();
            // item2TextB.text = ("" + item2num);

            // Text item3TextB = item3Text.GetComponent<Text>();
            // item3TextB.text = ("" + item3num);

            // Text item4TextB = item4Text.GetComponent<Text>();
            // item4TextB.text = ("" + item4num);

            // Text item1Text_priceB = item1Text_price.GetComponent<Text>();
            // item1Text_priceB.text = ("" + prices[0]);

            // Text item2Text_priceB = item2Text_price.GetComponent<Text>();
            // item2Text_priceB.text = ("" + prices[1]);

            // Text item3Text_priceB = item3Text_price.GetComponent<Text>();
            // item3Text_priceB.text = ("" + prices[2]);

            // Text item4Text_priceB = item4Text_price.GetComponent<Text>();
            // item4Text_priceB.text = ("" + prices[3]);
      }

      public void InventoryAdd(int item){
            
            if ((item == 0) && (coins >= prices[0])) {
                  item1bool = true; 
                  item1num ++;
                  CoinChange(0 - prices[0]);
            }
            else if ((item == 1) && (coins >= prices[1])) {
                  item2bool = true; 
                  item2num ++;
                  CoinChange(0 - prices[1]);
            }
            else if ((item == 2) && (coins >= prices[2])) {
                  item3bool = true; 
                  item3num ++;
                  CoinChange(0 - prices[2]);
            }
            else { Debug.Log("This item does not exist to be added"); }
            InventoryDisplay();

            if (!InvIsOpen){
                  OpenCloseInventory();
            }
      }

      // public void InventoryRemove(int item, int num){
      //       if (item == 0) {
      //             item1num -= num;
      //             if (item1num <= 0) { item1bool =false; }
      //             // Add any other intended effects: new item crafted, speed boost, slow time, etc
      //        }
      //       else if (item == 1) {
      //             item2num -= num;
      //             if (item2num <= 0) { item2bool =false; }
      //             // Add any other intended effects
      //        }
      //       else if (item == 2) {
      //             item3num -= num;
      //             if (item3num <= 0) { item3bool =false; }
      //               // Add any other intended effects
      //       }
      //       else if (item == 3) {
      //             item4num -= num;
      //             if (item4num <= 0) { item4bool =false; }
      //               // Add any other intended effects
      //       }
      //       else { Debug.Log("This item does not exist to be removed"); }
      //       InventoryDisplay();
      // }

      public void CoinChange(int amount){
            coins +=amount;
            InventoryDisplay();
      }

      // Open and Close the Inventory. Use this function on a button next to the inventory bar.
      public void OpenCloseInventory(){
            if (InvIsOpen){ InventoryMenu.SetActive(false); }
            else { InventoryMenu.SetActive(true); }
            InvIsOpen = !InvIsOpen;
      }

      // Reset all static inventory values on game restart.
      // public void ResetAllInventory(){
      //       item1bool = false;
      //       item2bool = false;
      //       item3bool = false;
      //       item4bool = false;

      //       item1num = 0; // object name
      //       item2num = 0; // object name
      //       item3num = 0; // object name
      //       item4num = 0; // object name
      // }

      void ChangeAllColor(int item) {
            if (item != 0) {
                  item1bg.GetComponent<Image>().color = new Color (1, 1, 1, 1);
            }
            if (item != 1) {
                  item2bg.GetComponent<Image>().color = new Color (1, 1, 1, 1);
            }
            if (item != 2) {
                  item3bg.GetComponent<Image>().color = new Color (1, 1, 1, 1);
            }
      }
      
      public int GetSelected() {
            return selected;
      }

      public bool Purchase() {
            if (prices[selected] > coins) {
                  return false;
            }
            CoinChange(0 - prices[selected]);
            return true;
      }

}
