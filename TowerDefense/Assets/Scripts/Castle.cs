using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Castle : MonoBehaviour
{
    public Transform spawnPoint;
    public Transform droneCaster;
    public GameObject allyPrefab;
    public GameObject dronePrefab;
    public int droneCost = 120;
    public TextMeshProUGUI droneCostT;
    AudioManager audioManager;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    public void Start()
    {
        droneCostT.text = "$" + droneCost;
    }

    public void SpawnAlly()
    {
        audioManager.PlaySFX(audioManager.allySpawn);
        Instantiate(allyPrefab, spawnPoint.position, spawnPoint.rotation);
    }

    public void SpawnDrone()
    {
        if (PlayerStats.Money >= droneCost)
        {
            audioManager.PlaySFX(audioManager.droneSpawn);
            PlayerStats.Money -= droneCost;
            Instantiate(dronePrefab, droneCaster.position, droneCaster.rotation);
        }
        else
        {
            audioManager.PlaySFX(audioManager.click);
            Debug.Log("Need more gold to spawn drone.");
        }
    }


}
