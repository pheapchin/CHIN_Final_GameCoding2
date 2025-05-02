using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformCamera1 : MonoBehaviour
{
    public Transform player;
    public Vector3 offset = new Vector3(0, 2f, -10f);
    public float followSpeed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (player == null) return;

        Vector3 targetPos = player.position + offset;

        //smoothly move cam towards cam
        transform.position = Vector3.Lerp(transform.position, targetPos, followSpeed * Time.deltaTime);
    }
}
