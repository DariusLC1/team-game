using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkPoint : MonoBehaviour
{
<<<<<<< Updated upstream
    
=======
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.instance.playerSpawnPos.transform.position = transform.position;
        }
    }
>>>>>>> Stashed changes
}
