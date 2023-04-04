using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;
using Unity.Services.Core;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using Unity.Services.Authentication;
using Unity.Networking.Transport.Relay;
using Unity.Netcode.Transports.UTP;


public class NetworkManagerUI : NetworkBehaviour
{
    [SerializeField]private Button hostButton;
    [SerializeField]private Button clientButton;
    [SerializeField]private InputField joinField;
    [SerializeField]private GameObject spider;
    [SerializeField]private GameObject ant;
    private string clientCode;

    public async void Start() {
        await UnityServices.InitializeAsync();

        AuthenticationService.Instance.SignedIn += () => {
            Debug.Log("Signed In " + AuthenticationService.Instance.PlayerId);
        };

        await AuthenticationService.Instance.SignInAnonymouslyAsync();
    }

    public void Awake() {
        hostButton.onClick.AddListener(CreateRelay);

        clientButton.onClick.AddListener(JoinRelay);
    }

    public void Update() {
        clientCode = joinField.text;
    }
    
    

    private async void CreateRelay() {
        try {
            Allocation allocation = await RelayService.Instance.CreateAllocationAsync(1);

            string joinCode = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);
            Debug.Log("Join Code: " + joinCode);

            RelayServerData serverDat = new RelayServerData(allocation, "dtls");
            NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(serverDat);
            NetworkManager.Singleton.StartHost();
        } catch(RelayServiceException e) {
            Debug.Log(e);
        }
    }

    private async void JoinRelay() {
        try {
            JoinAllocation joinAlloc = await RelayService.Instance.JoinAllocationAsync(clientCode);
            RelayServerData serverDat = new RelayServerData(joinAlloc, "dtls");
            NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(serverDat);
            NetworkManager.Singleton.StartClient();
        } catch (RelayServiceException e) {
            Debug.Log(e);
        }
    }
}
