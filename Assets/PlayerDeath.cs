using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDeath : MonoBehaviour
{
    Health player;
    bool Dead = false;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.health <= 0 && Dead == false)
        {
            GetComponent<PlayerInput>().enabled = false;
            GetComponent<Animator>().SetTrigger("Die");
            Dead = true;
        }
    }
}
