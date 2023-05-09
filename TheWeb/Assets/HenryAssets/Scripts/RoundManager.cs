using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;

public class RoundManager : NetworkBehaviour
{
    // Start is called before the first frame update
    public NetworkVariable<int> roundTimer = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    public NetworkVariable<int> rounds = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    private float timer;
    public Text timerText;
    public Text counterText;
    private int maxTime = 10;
    private bool connected = false;
    private bool buildable;
    private GameInventory antInv;
    private int antsPerRound = 100;
    private int numRounds = 0;
    private bool buildTime = false;
    void Start()
    {
        GameObject tmp = GameObject.FindWithTag("ant_inv");
        antInv = tmp.GetComponent<GameInventory>();
    }

    // Update is called once per frame
    void Update()
    {
        
        
        if (IsHost && NetworkManager.ConnectedClients.Count < 2) {
            timerText.gameObject.SetActive(false);
            return;
        } 
        
        
        if (!connected) {
            timer = maxTime;
            connected = true;
            buildTime = true;
        }
        if (GameObject.FindWithTag("egg") != null) {
            timerText.gameObject.SetActive(true);
            counterText.gameObject.SetActive(true);
        }
        if (IsHost) {
            if (timer > 0) {
                timer -= Time.deltaTime;
            }
            roundTimer.Value = (int)Mathf.Round(timer);
        }
        if (roundTimer.Value > 0) {
            timerText.GetComponent<Text>().text = "Build: " + roundTimer.Value;
        } else {
            if (IsHost) {
                timerText.GetComponent<Text>().text = "Defend!";
            } else {
                timerText.GetComponent<Text>().text = "Attack!";
            }
        }
        counterText.GetComponent<Text>().text = "Round: " + (rounds.Value + 1);
        if (roundTimer.Value <= 0) {
            
            if (!IsHost && antInv.coins <= 0) {
                antInv.coins = (rounds.Value + 1) * antsPerRound;
                NewRoundServerRpc();
            }
        }
        Debug.Log(roundTimer.Value);
        

    }

    [ServerRpc(RequireOwnership=false)]
    public void NewRoundServerRpc() {
        rounds.Value += 1;
        timer = maxTime;
    }

    public bool canBuild() {
        return roundTimer.Value <= 0;
    }
}
