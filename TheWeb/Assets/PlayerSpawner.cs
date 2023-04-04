using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PlayerSpawner : NetworkBehaviour
{
    // Start is called before the first frame update
    [SerializeField]private GameObject[] objs;
    private GameObject spawned;
    void Start()
    {
        spawned = objs[0];
        if (IsHost && IsOwner) {
            Debug.Log("Spider");
            spawned = (GameObject)Instantiate(objs[0]);
            spawned.GetComponent<NetworkObject>().Spawn(true);
            

        } else {
            Debug.Log("Ant");
            SpawnServerRpc();
            
        } 
        
        
        Destroy(gameObject);
    }

    [ServerRpc(RequireOwnership = false)]
    void SpawnServerRpc() {
        Debug.Log("test");
        spawned = (GameObject)Instantiate(objs[1]);
        spawned.GetComponent<NetworkObject>().SpawnWithOwnership(1);
       

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
