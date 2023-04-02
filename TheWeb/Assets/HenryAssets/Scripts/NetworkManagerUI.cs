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

    private void Awake() {
        hostButton.onClick.AddListener(() => {
            
            NetworkManager.Singleton.StartHost();
            GameObject spawnedSpider = (GameObject)Instantiate(spider);
            spawnedSpider.GetComponent<NetworkObject>().Spawn(true);
            hostButton.gameObject.SetActive(false);
            clientButton.gameObject.SetActive(false);
            

        });
        clientButton.onClick.AddListener(() => {
            NetworkManager.Singleton.StartClient();
            Debug.Log("HII");
            SpawnAntServerRpc();
            hostButton.gameObject.SetActive(false);
            clientButton.gameObject.SetActive(false);
            
        });
    }

    [ServerRpc(RequireOwnership=false)]
    private void SpawnAntServerRpc() {
        // Debug.Log("test ");
        GameObject spawnedAnt = (GameObject)Instantiate(ant);
        // Debug.Log(spawnedAnt.transform.position);
        spawnedAnt.GetComponent<NetworkObject>().Spawn(true);
    }
}
