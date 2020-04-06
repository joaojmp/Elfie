using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour
{
    private Vector2 velocity;
    public float smoothX, smoothY;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        smoothX = 0.15f;
        smoothY = 0.15f;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        float posx = Mathf.SmoothDamp(transform.position.x, player.transform.position.x, ref velocity.x, smoothX);
        float posy = Mathf.SmoothDamp(transform.position.y, player.transform.position.y, ref velocity.y, smoothY);

        transform.position = new Vector3(posx,posy,transform.position.z);
    }
}
