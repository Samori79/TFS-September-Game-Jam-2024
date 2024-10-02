using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStun : MonoBehaviour
{

    public float stunDuration = 0.2f;
    


    // Start is called before the first frame update
     void OnCollisionEnter(Collision collision)
    {
        Debug.Log("On Collision Enter Triggered");

        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerDriftMovement pm = collision.gameObject.GetComponent<PlayerDriftMovement>();
            if(pm != null)
            {
                pm.StunPlayer(stunDuration);
            }
        }
        Destroy(gameObject);
    }

    
}
