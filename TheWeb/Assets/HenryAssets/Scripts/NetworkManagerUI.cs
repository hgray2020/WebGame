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
    [SerializeField]private Button controlsButton;
    [SerializeField]private Button exitControlsButton;
    [SerializeField]private Button nextMovementButton;
    [SerializeField]private Button nextRotationButton;
    [SerializeField]private Button backRotationButton;
    [SerializeField]private Button nextShootButton;
    [SerializeField]private Button backShootButton;
    [SerializeField]private Button nextBuildButton;
    [SerializeField]private Button backBuildButton;
    [SerializeField]private Button nextInventoryButton;
    [SerializeField]private Button backInventoryButton;
    [SerializeField]private Button backInventoryLayoutButton;
    [SerializeField]private InputField joinField;
    [SerializeField]private GameObject hostCode;
    [SerializeField]private GameObject mainMenu;
    [SerializeField]private GameObject pauseMenu;
    [SerializeField]private GameObject creditsMenu;
    [SerializeField]private GameObject networkMenu;
    [SerializeField]private GameObject spiderUI;
    [SerializeField]private GameObject antUI;
    [SerializeField]private GameObject spider_movementUI;
    [SerializeField]private GameObject spider_rotationUI;
    [SerializeField]private GameObject spider_shootUI;
    [SerializeField]private GameObject spider_buildUI;
    [SerializeField]private GameObject spider_inventoryUI;
    [SerializeField]private GameObject spider_inventoryLayoutUI;
    [SerializeField]private GameObject spider;
    [SerializeField]private GameObject ant;
    [SerializeField]private Text joinCodeText;
    private string clientCode;

    public static bool GameisPaused = false;
    public static bool pauseActive = false;

    private Vector3 mainMenu_startScale;
    private Vector3 credits_startScale;
    private Vector3 networkMenu_startScale;
    private Vector3 pauseMenu_startScale;
    public float tweenTime = 1f;


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
        controlsButton.onClick.AddListener(Controls);
        exitControlsButton.onClick.AddListener(Pause);
        nextMovementButton.onClick.AddListener(Rotation);
        nextRotationButton.onClick.AddListener(Shoot);
        backRotationButton.onClick.AddListener(Controls);
        nextShootButton.onClick.AddListener(Build);
        backShootButton.onClick.AddListener(Rotation);
        nextBuildButton.onClick.AddListener(Inventory_Spider);
        backBuildButton.onClick.AddListener(Shoot);
        nextInventoryButton.onClick.AddListener(InventoryLayout_Spider);
        backInventoryButton.onClick.AddListener(Build);
        backInventoryLayoutButton.onClick.AddListener(Inventory_Spider);

        networkMenu.gameObject.SetActive(false);
        hostCode.gameObject.SetActive(false);
        pauseButton.gameObject.SetActive(false);
        pauseMenu.gameObject.SetActive(false);
        creditsMenu.gameObject.SetActive(false);
        spiderUI.gameObject.SetActive(false);
        antUI.gameObject.SetActive(false);
        spider_movementUI.gameObject.SetActive(false);
        spider_rotationUI.gameObject.SetActive(false);
        spider_shootUI.gameObject.SetActive(false);
        spider_buildUI.gameObject.SetActive(false);
        spider_inventoryUI.gameObject.SetActive(false);
        spider_inventoryLayoutUI.gameObject.SetActive(false);
        mainMenu.gameObject.SetActive(true);

        GameisPaused = false;

        mainMenu_startScale = mainMenu.transform.localScale;
        credits_startScale = creditsMenu.transform.localScale;
        networkMenu_startScale = networkMenu.transform.localScale;
        pauseMenu_startScale = pauseMenu.transform.localScale;

        creditsMenu.transform.localScale = Vector2.zero;
        mainMenu.transform.localScale = Vector2.zero;
        networkMenu.transform.localScale = Vector2.zero;
        pauseMenu.transform.localScale = Vector2.zero;

        LeanTween.scale(mainMenu, mainMenu_startScale, tweenTime).setEase(LeanTweenType.easeOutElastic).setIgnoreTimeScale(true);
    }

    public void Update() {
        clientCode = joinField.text;
        Debug.Log("!" + clientCode + "!");

        if (Input.GetKeyDown(KeyCode.Escape) && (pauseActive)){
            if (GameisPaused){
                    Resume();
            }
            else{
                    Pause();
            }
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
        pauseActive = false;

        mainMenu.gameObject.SetActive(true);
        LeanTween.scale(mainMenu, mainMenu_startScale, tweenTime).setEase(LeanTweenType.easeOutElastic).setIgnoreTimeScale(true);
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

        hostCode.gameObject.SetActive(true);
        networkMenu.gameObject.SetActive(true);
        LeanTween.scale(networkMenu, networkMenu_startScale, tweenTime).setEase(LeanTweenType.easeOutElastic).setIgnoreTimeScale(true);
    }

    public void Credits() {
        mainMenu.gameObject.SetActive(false);
        mainMenu.transform.localScale = Vector2.zero;
        creditsMenu.gameObject.SetActive(true);

        LeanTween.scale(creditsMenu, credits_startScale, tweenTime).setEase(LeanTweenType.easeOutElastic).setIgnoreTimeScale(true);
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
        LeanTween.scale(pauseMenu, pauseMenu_startScale, tweenTime).setEase(LeanTweenType.easeOutElastic).setIgnoreTimeScale(true);
        pauseButton.gameObject.SetActive(false);
        spiderUI.gameObject.SetActive(false);
        antUI.gameObject.SetActive(false);
        spider_movementUI.gameObject.SetActive(false);
        spider_rotationUI.gameObject.SetActive(false);
        spider_buildUI.gameObject.SetActive(false);
        spider_inventoryUI.gameObject.SetActive(false);
        spider_inventoryLayoutUI.gameObject.SetActive(false);
        exitControlsButton.gameObject.SetActive(false);

        Time.timeScale = 0f;
        GameisPaused = true;
    }

    public void Resume(){
        pauseMenu.gameObject.SetActive(false);
        pauseMenu.transform.localScale = Vector2.zero;
        pauseButton.gameObject.SetActive(true);
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
        if (IsHost) {
            spider_movementUI.gameObject.SetActive(true);
            exitControlsButton.gameObject.SetActive(true);
            spider_rotationUI.gameObject.SetActive(false);
        }
        pauseMenu.gameObject.SetActive(false);
        pauseMenu.transform.localScale = Vector2.zero;
    }

    public void Rotation() {
        spider_movementUI.gameObject.SetActive(false);
        spider_rotationUI.gameObject.SetActive(true);
        spider_shootUI.gameObject.SetActive(false);
    }

    public void Shoot() {
        spider_rotationUI.gameObject.SetActive(false);
        spider_shootUI.gameObject.SetActive(true);
        spider_buildUI.SetActive(false);
    }

    public void Build() {
        spider_shootUI.gameObject.SetActive(false);
        spider_buildUI.gameObject.SetActive(true);
        spider_inventoryUI.gameObject.SetActive(false);
    }

    public void Inventory_Spider() {
        spider_buildUI.gameObject.SetActive(false);
        spider_inventoryUI.gameObject.SetActive(true);
        spider_inventoryLayoutUI.gameObject.SetActive(false);
    }

    public void InventoryLayout_Spider() {
        spider_inventoryUI.gameObject.SetActive(false);
        spider_inventoryLayoutUI.gameObject.SetActive(true);
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
            pauseButton.gameObject.SetActive(true);
            antUI.gameObject.SetActive(true);
        } catch (RelayServiceException e) {
            Debug.Log(e);
        }
    }

    [ClientRpc]
    void ServerShutdownClientRpc(){
        MainMenu();
    }
}
