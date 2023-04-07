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
    [SerializeField]private Button pauseButton;
    [SerializeField]private Button backButton;
    [SerializeField]private Button playButton;
    [SerializeField]private Button creditsButton;
    [SerializeField]private Button exitButton;
    [SerializeField]private Button backCreditsButton;
    [SerializeField]private Button resumeButton;
    [SerializeField]private Button exitPauseButton;
    [SerializeField]private InputField joinField;
    [SerializeField]private GameObject hostCode;
    [SerializeField]private GameObject mainMenu;
    [SerializeField]private GameObject pauseMenu;
    [SerializeField]private GameObject creditsMenu;
    [SerializeField]private GameObject spider;
    [SerializeField]private GameObject ant;
    [SerializeField]private Text joinCodeText;
    private string clientCode;

    public static bool GameisPaused = false;

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
        backButton.onClick.AddListener(MainMenu);
        playButton.onClick.AddListener(StartGame);
        creditsButton.onClick.AddListener(Credits);
        backCreditsButton.onClick.AddListener(MainMenu);
        exitButton.onClick.AddListener(QuitGame);
        pauseButton.onClick.AddListener(Pause);
        resumeButton.onClick.AddListener(Resume);
        exitPauseButton.onClick.AddListener(MainMenu);

        hostButton.gameObject.SetActive(false);
        clientButton.gameObject.SetActive(false);
        joinField.gameObject.SetActive(false);
        backButton.gameObject.SetActive(false);
        hostCode.gameObject.SetActive(false);
        pauseButton.gameObject.SetActive(false);
        mainMenu.gameObject.SetActive(true);
        pauseMenu.gameObject.SetActive(false);
        creditsMenu.gameObject.SetActive(false);

        GameisPaused = false;
    }

    public void Update() {
        clientCode = joinField.text;

        if (Input.GetKeyDown(KeyCode.Escape)){
            if (GameisPaused){
                    Resume();
            }
            else{
                    Pause();
            }
        }
    }

    public void MainMenu() {
        hostButton.gameObject.SetActive(false);
        clientButton.gameObject.SetActive(false);
        joinField.gameObject.SetActive(false);
        backButton.gameObject.SetActive(false);
        hostCode.gameObject.SetActive(false);
        pauseButton.gameObject.SetActive(false);
        creditsMenu.gameObject.SetActive(false);
        pauseMenu.gameObject.SetActive(false);

        mainMenu.gameObject.SetActive(true);
        Time.timeScale = 1f;
        GameisPaused = false;
    }
    
    public void StartGame() {
        mainMenu.gameObject.SetActive(false);

        hostButton.gameObject.SetActive(true);
        clientButton.gameObject.SetActive(true);
        joinField.gameObject.SetActive(true);
        backButton.gameObject.SetActive(true);
        hostCode.gameObject.SetActive(true);
    }

    public void Credits() {
        mainMenu.gameObject.SetActive(false);
        creditsMenu.gameObject.SetActive(true);
    }

    public void QuitGame() {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }

    void Pause(){
        pauseMenu.gameObject.SetActive(true);
        pauseButton.gameObject.SetActive(false);
        Time.timeScale = 0f;
        GameisPaused = true;
    }

    public void Resume(){
        pauseMenu.gameObject.SetActive(false);
        pauseButton.gameObject.SetActive(true);
        Time.timeScale = 1f;
        GameisPaused = false;
    }

    private async void CreateRelay() {
        try {
            Allocation allocation = await RelayService.Instance.CreateAllocationAsync(1);

            string joinCode = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);
            Debug.Log("Join Code: " + joinCode);

            RelayServerData serverDat = new RelayServerData(allocation, "dtls");
            NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(serverDat);
            NetworkManager.Singleton.StartHost();

            hostButton.gameObject.SetActive(false);
            clientButton.gameObject.SetActive(false);
            joinField.gameObject.SetActive(false);
            backButton.gameObject.SetActive(false);
            pauseButton.gameObject.SetActive(true);

            joinCodeText.GetComponent<Text>().text = joinCode;
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

            hostButton.gameObject.SetActive(false);
            clientButton.gameObject.SetActive(false);
            joinField.gameObject.SetActive(false);
            backButton.gameObject.SetActive(false);
            pauseButton.gameObject.SetActive(true);
        } catch (RelayServiceException e) {
            Debug.Log(e);
        }
    }
}
