using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class AntSpawner : NetworkBehaviour
{
    // Start is called before the first frame update
    private Vector3 mouseScreenPos;
    private Vector3 mouseWorldPos;
    private Vector2 mouseCoords;
    private bool canSpawn = false;
    public GameInventory gameInventory;
    private int cd;
    public bool init = false;
    private int cooldown = 15;
    private GameObject[] spawnable;
    [SerializeField]private GameObject[] ants;
    
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
        if (IsHost) {
            // Debug.Log(init);
            return;
        }
        if (!init) {
            GameObject tmp = GameObject.FindWithTag("ant_inv");
            if (tmp == null) {
                return;
            }
            init = true;
            gameInventory = tmp.GetComponent<GameInventory>();
        }
        
        mouseScreenPos = Input.mousePosition;
        mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);
        mouseCoords = new Vector2(mouseWorldPos.x, mouseWorldPos.y);
        spawnable = GameObject.FindGameObjectsWithTag("spawnbox");
        canSpawn = false;
        foreach (GameObject spawner in spawnable) {
            if (spawner.GetComponent<BoxCollider2D>().OverlapPoint(mouseCoords)) {
                canSpawn = true;
                break;
            }
        }
        // Debug.Log(gameInventory.GetSelected());
        if (Input.GetMouseButtonDown(0) && canSpawn && cd == 0 && gameInventory.coins > 0) {
            cd = cooldown;
            SpawnAntServerRpc(gameInventory.GetSelected(), mouseCoords.x, mouseCoords.y);
            gameInventory.CoinChange(-1);
        }
    }

    void FixedUpdate() {
        if (IsHost) {
            return;
        }
        if (cd > 0) {
            cd--;
        }
    }

   

    [ServerRpc(RequireOwnership = false)]
    public void SpawnAntServerRpc(int ant, float x, float y) {
        Debug.Log(ant);
        GameObject spawned = (GameObject)Instantiate(ants[ant], new Vector3(x, y, 1), Quaternion.identity);
        spawned.GetComponent<NetworkObject>().SpawnWithOwnership(1);
        spawned.transform.position = new Vector3(x, y, 1);
        Debug.Log(new Vector3(x, y, 1));
    }
}
