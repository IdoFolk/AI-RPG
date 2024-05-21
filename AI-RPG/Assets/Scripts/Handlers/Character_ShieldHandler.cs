using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_ShieldHandler : MonoBehaviour
{

    [SerializeField]
    Weapon_Shield equippedShield;

    PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        playerController = PlayerController.instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
