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
using UnityEngine.Audio;
using UnityEngine.SceneManagement;


public class NetworkManagerUI : NetworkBehaviour
{
    [SerializeField]private Button hostButton;
    [SerializeField]private Button clientButton;
    [SerializeField]private Button pauseButton;
    [SerializeField]private Button backButton;
    [SerializeField]private Button playButton;
    [SerializeField]private Button creditsButton;
    [SerializeField]private Button mainControlsButton;
    [SerializeField]private Button spiderControlsButton;
    [SerializeField]private Button antControlsButton;
    [SerializeField]private Button exitMainControlsButton;
    [SerializeField]private Button exitButton;
    [SerializeField]private Button backCreditsButton;
    [SerializeField]private Button joinButton;
    [SerializeField]private Button joinBackButton;
    [SerializeField]private Button resumeButton;
    [SerializeField]private Button controlsButton;
    [SerializeField]private Button exitControlsButton;
    [SerializeField]private Button startSpider;
    [SerializeField]private Button startAnt;

    // Spider UI
    [SerializeField]private GameObject spider_web;
    [SerializeField]private GameObject spider_build;
    [SerializeField]private GameObject spider_movementUI;
    [SerializeField]private GameObject spider_rotationUI;
    [SerializeField]private GameObject spider_shootUI;
    [SerializeField]private GameObject spider_InitializeBuildUI;
    [SerializeField]private GameObject spider_buildUI;
    [SerializeField]private GameObject spider_ConnectBuildUI;
    [SerializeField]private GameObject spider_AddOnUI;
    [SerializeField]private GameObject spider_typesUI;
    [SerializeField]private GameObject spider_inventoryUI;
    [SerializeField]private GameObject spider_inventoryLayoutUI;
    [SerializeField]private GameObject spider_wins;
    [SerializeField]private GameObject spider_loses;
    [SerializeField]private GameObject spider_round;


    // Ant UI
    [SerializeField]private GameObject ant_spawnerUI;
    [SerializeField]private GameObject ant_inventoryUI;
    [SerializeField]private GameObject ant_inventoryLayoutUI;
    [SerializeField]private GameObject ant_typesUI;
    [SerializeField]private GameObject ant_wins;
    [SerializeField]private GameObject ant_loses;
    [SerializeField]private GameObject ant_round;

    [SerializeField]private InputField joinField;
    [SerializeField]private GameObject hostCode;
    [SerializeField]private GameObject clientUI;
    [SerializeField]private GameObject mainMenu;
    [SerializeField]private GameObject pauseMenu;
    [SerializeField]private GameObject creditsMenu;
    [SerializeField]private GameObject networkMenu;
    [SerializeField]private GameObject spiderUI;
    [SerializeField]private GameObject antUI;
    [SerializeField]private GameObject controlsMenu;
    [SerializeField]private GameObject spider;
    [SerializeField]private GameObject ant;
    [SerializeField]private GameObject introSpider;
    [SerializeField]private GameObject introAnt;
    [SerializeField]private GameObject VolumeSlider;
    [SerializeField]private GameObject RoundManager;
    [SerializeField]private Text joinCodeText;

    private string clientCode;

    public static bool GameisPaused = false;
    public static bool pauseActive = false;
    public int tutorial = 0;

    private Vector3 mainMenu_startScale;
    private Vector3 credits_startScale;
    private Vector3 networkMenu_startScale;
    private Vector3 pauseMenu_startScale;
    public float tweenTime = 1f;

    public AudioMixer mixer;
    public static float volumeLevel = 1f;
    private Slider sliderVolumeCtrl;

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
        backButton.onClick.AddListener(PlayScene);
        playButton.onClick.AddListener(StartGame);
        mainControlsButton.onClick.AddListener(Controls);
        spiderControlsButton.onClick.AddListener(Movement);
        antControlsButton.onClick.AddListener(Spawner);
        exitMainControlsButton.onClick.AddListener(MainMenu);
        creditsButton.onClick.AddListener(Credits);
        backCreditsButton.onClick.AddListener(MainMenu);
        exitButton.onClick.AddListener(QuitGame);
        pauseButton.onClick.AddListener(Pause);
        resumeButton.onClick.AddListener(Resume);
        controlsButton.onClick.AddListener(Controls);
        exitControlsButton.onClick.AddListener(Pause);
        startSpider.onClick.AddListener(StartSpider);
        startAnt.onClick.AddListener(StartAnt);

        joinButton.onClick.AddListener(Client);
        joinBackButton.onClick.AddListener(StartGame);

        networkMenu.gameObject.SetActive(true);
        hostCode.gameObject.SetActive(false);
        pauseButton.gameObject.SetActive(false);
        pauseMenu.gameObject.SetActive(false);
        creditsMenu.gameObject.SetActive(false);
        spiderUI.gameObject.SetActive(false);
        antUI.gameObject.SetActive(false);

        // Spider UI
        spider_web.gameObject.SetActive(false);
        spider_build.gameObject.SetActive(false);
        spider_movementUI.gameObject.SetActive(false);
        spider_rotationUI.gameObject.SetActive(false);
        spider_shootUI.gameObject.SetActive(false);
        spider_InitializeBuildUI.gameObject.SetActive(false);
        spider_buildUI.gameObject.SetActive(false);
        spider_ConnectBuildUI.gameObject.SetActive(false);
        spider_AddOnUI.gameObject.SetActive(false);
        spider_typesUI.gameObject.SetActive(false);
        spider_inventoryUI.gameObject.SetActive(false);
        spider_inventoryLayoutUI.gameObject.SetActive(false);
        spider_wins.gameObject.SetActive(false);
        spider_loses.gameObject.SetActive(false);
        spider_round.gameObject.SetActive(false);

        // Ant UI
        ant_spawnerUI.gameObject.SetActive(false);
        ant_inventoryUI.gameObject.SetActive(false);
        ant_inventoryLayoutUI.gameObject.SetActive(false);
        ant_typesUI.gameObject.SetActive(false);
        ant_wins.gameObject.SetActive(false);
        ant_loses.gameObject.SetActive(false);
        ant_round.gameObject.SetActive(false);

        controlsMenu.gameObject.SetActive(false);
        exitMainControlsButton.gameObject.SetActive(false);
        introSpider.gameObject.SetActive(false);
        mainMenu.gameObject.SetActive(false);
        VolumeSlider.gameObject.SetActive(false);

        GameisPaused = false;
        tutorial = 0;

        // Audio
        SetLevel(volumeLevel);
        GameObject sliderTemp = GameObject.FindWithTag("VolumeSlider");
        if (sliderTemp != null){
            sliderVolumeCtrl = sliderTemp.GetComponent<Slider>();
            sliderVolumeCtrl.value = volumeLevel;
        }

        StartGame();
    }

    public void Update() {
        clientCode = joinField.text;

        if (Input.GetButtonDown("Pause") && (pauseActive)){
            if (GameisPaused){
                    Resume();
            }
            else{
                    Pause();
            }
        }

        if (Input.GetButton("Shoot") && tutorial < 1) {
            spider_build.gameObject.SetActive(true);
            spider_web.gameObject.SetActive(false);
            tutorial += 1;
        }
        if (Input.GetButton("WebBuild") && tutorial == 1) {
            spider_build.gameObject.SetActive(false);
            tutorial += 1;
        }
    }

    public void MainMenu() {
        networkMenu.gameObject.SetActive(false);
        networkMenu.transform.localScale = Vector2.zero;
        hostCode.gameObject.SetActive(false);
        pauseButton.gameObject.SetActive(false);
        creditsMenu.gameObject.SetActive(false);
        creditsMenu.transform.localScale = Vector2.zero;
        pauseMenu.gameObject.SetActive(false);
        pauseMenu.transform.localScale = Vector2.zero;
        spiderUI.gameObject.SetActive(false);
        antUI.gameObject.SetActive(false);
        controlsMenu.gameObject.SetActive(false);
        VolumeSlider.gameObject.SetActive(true);

        // Spider UI
        spider_build.gameObject.SetActive(false);
        spider_web.gameObject.SetActive(false);
        spider_movementUI.gameObject.SetActive(false);
        spider_rotationUI.gameObject.SetActive(false);
        spider_shootUI.gameObject.SetActive(false);
        spider_InitializeBuildUI.gameObject.SetActive(false);
        spider_buildUI.gameObject.SetActive(false);
        spider_ConnectBuildUI.gameObject.SetActive(false);
        spider_AddOnUI.gameObject.SetActive(false);
        spider_typesUI.gameObject.SetActive(false);
        spider_inventoryUI.gameObject.SetActive(false);
        spider_inventoryLayoutUI.gameObject.SetActive(false);
        exitMainControlsButton.gameObject.SetActive(false);
        clientUI.gameObject.SetActive(false);
        spider_wins.gameObject.SetActive(false);
        spider_loses.gameObject.SetActive(false);
        spider_round.gameObject.SetActive(false);

        // Ant UI
        ant_spawnerUI.gameObject.SetActive(false);
        ant_inventoryUI.gameObject.SetActive(false);
        ant_inventoryLayoutUI.gameObject.SetActive(false);
        ant_typesUI.gameObject.SetActive(false);
        ant_wins.gameObject.SetActive(false);
        ant_loses.gameObject.SetActive(false);
        ant_round.gameObject.SetActive(false);

        pauseActive = false;

        mainMenu.gameObject.SetActive(true);
        Time.timeScale = 1f;
        GameisPaused = false;

        NetworkManager.Singleton.Shutdown();
        if (IsHost) {
            ServerShutdownClientRpc();
        }
    }

    public void StartGame() {
        mainMenu.gameObject.SetActive(false);
        mainMenu.transform.localScale = Vector2.zero;
        clientUI.gameObject.SetActive(false);
        VolumeSlider.gameObject.SetActive(false);

        hostCode.gameObject.SetActive(true);
        networkMenu.gameObject.SetActive(true);
    }

    public void Client() {
        hostCode.gameObject.SetActive(false);
        networkMenu.gameObject.SetActive(false);
        clientUI.gameObject.SetActive(true);
    }

    public void MainControls() {
        mainMenu.gameObject.SetActive(false);
        mainMenu.transform.localScale = Vector2.zero;

        controlsMenu.gameObject.SetActive(true);
        exitMainControlsButton.gameObject.SetActive(true);
    }

    public void Credits() {
        mainMenu.gameObject.SetActive(false);
        mainMenu.transform.localScale = Vector2.zero;
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
        spiderUI.gameObject.SetActive(false);
        antUI.gameObject.SetActive(false);
        VolumeSlider.gameObject.SetActive(true);

        // Spider UI
        spider_web.gameObject.SetActive(false);
        spider_build.gameObject.SetActive(false);
        spider_movementUI.gameObject.SetActive(false);
        spider_rotationUI.gameObject.SetActive(false);
        spider_shootUI.gameObject.SetActive(false);
        spider_InitializeBuildUI.gameObject.SetActive(false);
        spider_buildUI.gameObject.SetActive(false);
        spider_ConnectBuildUI.gameObject.SetActive(false);
        spider_AddOnUI.gameObject.SetActive(false);
        spider_typesUI.gameObject.SetActive(false);
        spider_inventoryUI.gameObject.SetActive(false);
        spider_inventoryLayoutUI.gameObject.SetActive(false);
        spider_round.gameObject.SetActive(false);
        exitControlsButton.gameObject.SetActive(false);

        //Ant UI
        ant_spawnerUI.gameObject.SetActive(false);
        ant_inventoryUI.gameObject.SetActive(false);
        ant_inventoryLayoutUI.gameObject.SetActive(false);
        ant_typesUI.gameObject.SetActive(false);
        ant_round.gameObject.SetActive(false);

        Time.timeScale = 0f;
        GameisPaused = true;
    }

    public void Resume(){
        pauseMenu.gameObject.SetActive(false);
        pauseButton.gameObject.SetActive(true);
        VolumeSlider.gameObject.SetActive(false);
        if (IsHost) {
            spiderUI.gameObject.SetActive(true);
        }
        if (!IsHost) {
            antUI.gameObject.SetActive(true);
        }
        Time.timeScale = 1f;
        GameisPaused = false;
    }

    public void Controls() {
        pauseMenu.gameObject.SetActive(false);
        // pauseMenu.transform.localScale = Vector2.zero;

        if (IsHost) {
            Movement();
        } else if (!GameisPaused) {
            MainControls();
        } else {
            Spawner();
        }
    }

    public void Movement() {
        controlsMenu.gameObject.SetActive(false);
        spider_movementUI.gameObject.SetActive(true);
        if (GameisPaused) {
            exitControlsButton.gameObject.SetActive(true);
        }
        spider_rotationUI.gameObject.SetActive(false);
    }

    public void Spawner() {
        controlsMenu.gameObject.SetActive(false);
        ant_spawnerUI.gameObject.SetActive(true);
        if (GameisPaused) {
            exitControlsButton.gameObject.SetActive(true);
        }
        ant_inventoryUI.gameObject.SetActive(false);
    }

    public void StartSpider() {
        introSpider.gameObject.SetActive(false);
        spider_web.gameObject.SetActive(true);
    }

    public void StartAnt() {
        introAnt.gameObject.SetActive(false);
    }

    public void SpiderWins() {
        Time.timeScale = 0f;
        GameisPaused = true;

        if (IsHost) {
            spider_wins.gameObject.SetActive(true);
        }
        if (!IsHost) {
            ant_loses.gameObject.SetActive(true);
        }
    }

    public void AntWins() {
        Time.timeScale = 0f;
        GameisPaused = true;

        if (IsHost) {
            spider_loses.gameObject.SetActive(true);
        }
        if (!IsHost) {
            ant_wins.gameObject.SetActive(true);
        }
    }

     public void SetLevel (float sliderValue){
        mixer.SetFloat("MusicVolume", Mathf.Log10 (sliderValue) * 20);
        volumeLevel = sliderValue;
    }

    private async void CreateRelay() {
        try {
            Allocation allocation = await RelayService.Instance.CreateAllocationAsync(1);

            string joinCode = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);
            Debug.Log("Join Code: " + joinCode);

            RelayServerData serverDat = new RelayServerData(allocation, "dtls");
            NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(serverDat);
            NetworkManager.Singleton.StartHost();

            networkMenu.gameObject.SetActive(false);
            networkMenu.transform.localScale = Vector2.zero;
            pauseButton.gameObject.SetActive(true);
            spiderUI.gameObject.SetActive(true);
            introSpider.gameObject.SetActive(true);
            pauseActive = true;

            joinCodeText.GetComponent<Text>().text = joinCode;
            joinCodeText.gameObject.SetActive(true);

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

            networkMenu.gameObject.SetActive(false);
            networkMenu.transform.localScale = Vector2.zero;
            clientUI.gameObject.SetActive(false);
            pauseButton.gameObject.SetActive(true);
            introAnt.gameObject.SetActive(true);
            antUI.gameObject.SetActive(true);
        } catch (RelayServiceException e) {
            Debug.Log(e);
        }
    }

    public void PlayScene() {
        AuthenticationService.Instance.SignOut();
        NetworkManager.Singleton.Shutdown();
        Cleanup();
        SceneManager.LoadScene("MainMenu");
    }

    [ClientRpc]
    public void ServerShutdownClientRpc(){
        Time.timeScale = 1;
        AuthenticationService.Instance.SignOut();
        NetworkManager.Singleton.Shutdown();
        Cleanup();

        SceneManager.LoadScene("MainMenu");
    }

    void Cleanup() {
        if (NetworkManager.Singleton != null) {
            Destroy(NetworkManager.Singleton.gameObject);
        }
    }
}
