using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;

public class RoundManager : NetworkBehaviour
{
    // Start is called before the first frame update
    public NetworkVariable<int> roundTimer = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    private float timer;
    public Text timerText;
    private int maxTime = 60;
    private bool connected = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (IsHost && NetworkManager.ConnectedClients.Count < 2) {
            timerText.gameObject.SetActive(false);
            Debug.Log("rhhh");
            
            return;
        }
        if (!connected) {
            timer = maxTime;
            connected = true;
        }
        if (IsHost) {
            timer -= time.deltaTime;
        }
        timerText.GetComponent<Text>().text = "" + (int)Mathf.Round(timer)


    }
}
