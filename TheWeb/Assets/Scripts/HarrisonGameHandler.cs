using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class HarrisonGameHandler : NetworkBehaviour
{
    public NetworkVariable<int> eggHealth = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

    public GameObject [] eggs;

    // public GameObject CameraShaker;

    public float durationTime = 0.15f;
    public float magnitude = 0.3f;
    public int maxHealth = 120;
    private int healthStep;
    private int numEggs;

    void Start(){
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
            DespawnServerRpc();
            return;
        }
        int percentile = (numEggs) - (eggHealth.Value / healthStep);
        foreach(GameObject egg in eggs) {
            egg.SetActive(false);
        }
        eggs[percentile].SetActive(true);
        
    }

    

    // public void playerDies(){
    //         player.GetComponent<PlayerHurt>().playerDead();       //play Death animation
    //         StartCoroutine(DeathPause());
    // }
}
