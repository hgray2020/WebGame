using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    // Start is called before the first frame update
    private string playerType = "none";
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(playerType);
    }

    public void SetPlayerType(string type) {
        if (type != "spider" && type != "ant") {
            Debug.Log("Invalid Player Type");
            return;
        }
        playerType = type;
    }
    
    public string GetPlayerType() {
        return playerType;
    }
}
