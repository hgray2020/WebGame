using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.Audio;

public class HarrisonGameHandler : NetworkBehaviour
{
    public NetworkVariable<int> eggHealth = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

    public GameObject [] eggs;
    public GameObject [] UIeggs;
    public GameObject NetworkUI;

    // public GameObject CameraShaker;

    public float durationTime = 0.15f;
    public float magnitude = 0.3f;
    public int maxHealth = 120;
    private int healthStep;
    private int numEggs;
    private int prevpercentile = 6;

    public AudioSource hissSFX;

    void Start(){
        NetworkUI = GameObject.FindGameObjectWithTag("NetworkUI");
        numEggs = eggs.Length;
        healthStep = maxHealth / (numEggs);
        eggHealth.Value = maxHealth;
        foreach(GameObject egg in eggs) {
            egg.SetActive(false);
            
        }
        eggs[0].SetActive(true);
    }

    [ServerRpc(RequireOwnership = false)]
    public void DespawnServerRpc() {
        Debug.Log("despawn rpc (eggs)");
        transform.parent.gameObject.GetComponent<NetworkObject>().Despawn();
    }

    [ServerRpc(RequireOwnership = false)]
    public void EggHitServerRpc(int damage) {
        Debug.Log("hit rpc (eggs)");
        eggHealth.Value -= damage;
    }

    public void eggsGetHit(int damage){
        EggHitServerRpc(damage);
       
        if (damage > 0){
            // CameraShaker.GetComponent<CameraShake>().ShakeCamera(durationTime, magnitude);
        }
    }

    public void FixedUpdate() {
        if (eggHealth.Value <= 0) {
            Debug.Log("rahh");
            NetworkUI.gameObject.SendMessage("AntWins");
            DespawnServerRpc();
            return;
        }
        float f_percentile = ((float)numEggs) - ((float)eggHealth.Value / (float)healthStep);
        int percentile = (int)Mathf.Floor(f_percentile);
        foreach(GameObject egg in eggs) {
            egg.SetActive(false);
        }
        eggs[percentile].SetActive(true);
        for (int i = numEggs - 1; i > numEggs - percentile - 1; i--) {
            UIeggs[i].SetActive(false);
        }
        if (percentile < prevpercentile) {
            if (hissSFX.isPlaying == false){
                    hissSFX.Play();
            }
        }
        prevpercentile = percentile;
    }

    

    // public void playerDies(){
    //         player.GetComponent<PlayerHurt>().playerDead();       //play Death animation
    //         StartCoroutine(DeathPause());
    // }
}
