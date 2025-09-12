using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cam_script : MonoBehaviour
{
    public Transform player;
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = new Vector3(transform.position.x , transform.position.y , player.transform.position.z - 11f);
    }
}
