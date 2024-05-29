using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    [SerializeField] private KeyCode dashButton;
    [SerializeField] private float dashForce;
    [SerializeField] private GameObject player;


    private bool dashEnabled = true;

    private void Update()
    {
        if(dashEnabled && Input.GetKeyDown(dashButton))
        {
            Dash(Vector3.forward);
        }
    }

    private void Dash(Vector3 direction)
    {
        player.transform.position += direction * 2772;
    }

}
