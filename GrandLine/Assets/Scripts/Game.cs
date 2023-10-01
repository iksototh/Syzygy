using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public GameObject Player;

    void Awake()
    {
        var player = Instantiate(Player, new Vector3Int(-2, 1), Quaternion.identity);
        var playerCamera = GameObject.FindGameObjectWithTag("PlayerCamera");
        var cinematicScript = playerCamera.GetComponent<CinemachineVirtualCamera>();
        cinematicScript.Follow = player.transform;
        cinematicScript.LookAt = player.transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
