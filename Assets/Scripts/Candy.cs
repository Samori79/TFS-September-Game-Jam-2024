using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candy : MonoBehaviour
{

    [SerializeField] public int scoreValue; //The value assigned to the individual pickup

    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //Player Score increase here
            GameManager.Instance.AddScore(scoreValue);

            Destroy(gameObject);

        }
    }

    /*void ApplySideEffect(int scoreValue)
    {
    switch (scoreValue)
    {
    //give the player a small boost of speed
    case 5:
    SpeedBoost();
    break;

    //add time to the timer
    case 10:
    TimeBoost();
    break;


    //make the player immune to stuns/time decreases
    case 15:
    Invulnerable();
    break;
    
    }


    }*/
}
