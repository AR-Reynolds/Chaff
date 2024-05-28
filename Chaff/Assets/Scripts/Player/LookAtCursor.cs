using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCursor : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] Transform rotation;
    [SerializeField] public GameObject gun;

    [SerializeField] LayerMask layer;
    [SerializeField] float lookDistance;

    private void Update()
    {
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(mouseRay, out hit, layer))
        {
            Vector3 pos = hit.point - player.transform.position;
            if (pos.magnitude < lookDistance)
            {
                if (gun != null)
                {
                    gun.transform.LookAt(hit.point);
                }
                return;
            }
            rotation.LookAt(hit.point);
            Quaternion lookat = new Quaternion(0, rotation.transform.rotation.y, 0, rotation.transform.rotation.w);

            player.transform.rotation = lookat;
            if(gun != null)
            {
                gun.transform.LookAt(hit.point);
            }
        }
    }
}


