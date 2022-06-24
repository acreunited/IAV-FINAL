using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnWolfs : MonoBehaviour
{

    public Transform player;
    public Material materialWolf;
    private int waitTime = 5;
    private bool canSpawn;

    // Start is called before the first frame update
    void Start()
    {
        canSpawn = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (canSpawn) {
            canSpawn = false;
            Spawn();
        }
       
        
    }

    private void Spawn() {
        this.StartCoroutine(Wait());
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        //cube.transform.position = new Vector3(blockx, blocky, blockz);
     
        cube.transform.position = new Vector3(player.position.x + Random.Range(-30, 30), player.position.y, player.position.z + Random.Range(-30, 30));
        cube.transform.parent = this.transform;
        cube.tag = "Wolf";
        cube.AddComponent<Rigidbody>();
        cube.GetComponent<MeshRenderer>().material = materialWolf;
        cube.AddComponent<Wolf>();

        
    }

    IEnumerator Wait() {
        yield return new WaitForSeconds( waitTime );
        canSpawn = true;

    }
}
