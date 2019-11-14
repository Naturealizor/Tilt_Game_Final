using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followPlayer : MonoBehaviour
{
    public Transform player;

    Vector3 startingPosition;
    // float followDistance = ;

    // Start is called before the first frame update
    void Start()
    {
        startingPosition = this.transform.position;
        // followDistance = player.transform.position.z - this.transform.position.z;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        this.transform.position = new Vector3(player.position.x, startingPosition.y - 5, player.position.z - 5);

        // this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y,
        //  this.transform.position.z - followDistance);
    }
}
