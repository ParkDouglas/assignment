using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoints : MonoBehaviour
{
    public List <GameObject> powerUps= new List <GameObject>();
    private bool randomizer;

    // Start is called before the first frame update
    void Start()
    {
        if(powerUps.Count > 0)
        {
           
            

                int index = Random.Range(0, powerUps.Count);
            Instantiate(powerUps[index], transform.position, transform.rotation);

            
        }
        
       
        
    }

}
