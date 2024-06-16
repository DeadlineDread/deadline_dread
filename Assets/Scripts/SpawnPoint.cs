using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    public GameObject PlayerSpawn;

    // Start is called before the first frame update
    void Start()
    {
        DoSpawnPlayer();
    }

    public GameObject DoSpawnPlayer(){
        if(PlayerSpawn != null){
            transform.position = new Vector3(0,0,0);
            return Instantiate(PlayerSpawn, transform.position, Quaternion.identity);
            //return Instantiate(PlayerSpawn, transform.position, Quaternion.identity);

        }
        return null;
    }
}
