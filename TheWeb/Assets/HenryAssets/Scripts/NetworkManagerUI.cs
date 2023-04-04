using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;

public class NetworkManagerUI : NetworkBehaviour
{
    [SerializeField]private Button hostButton;
    [SerializeField]private Button clientButton;
    [SerializeField]private GameObject spider;
    [SerializeField]private GameObject ant;

    public void Awake() {
        hostButton.onClick.AddListener(() => {
            
            NetworkManager.Singleton.StartHost();
            
            hostButton.gameObject.SetActive(false);
            clientButton.gameObject.SetActive(false);
            

        });

        clientButton.onClick.AddListener(() => {
            NetworkManager.Singleton.StartClient();
            
            hostButton.gameObject.SetActive(false);
            clientButton.gameObject.SetActive(false);
            
        });
    }
    
    

    [ServerRpc(RequireOwnership = false)]
    public void SpawnAntServerRpc() {
        Debug.Log("test ");
        // GameObject spawnedAnt = (GameObject)Instantiate(ant);
        // // Debug.Log(spawnedAnt.transform.position);
        // spawnedAnt.GetComponent<NetworkObject>().Spawn(true);
    }
}
