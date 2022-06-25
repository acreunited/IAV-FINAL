using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnWolfs : MonoBehaviour {

    public Transform player;
    //public Material materialWolf;
    private int waitTime = 20;
    private bool canSpawn;
    public GameObject goWolf;

    // Start is called before the first frame update
    void Start() {
        canSpawn = true;
    }

    // Update is called once per frame
    void Update() {
        if (canSpawn) {
            canSpawn = false;
            Spawn();
        }


    }

    private void Spawn() {
        this.StartCoroutine(Wait());
        //GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        //cube.transform.position = new Vector3(blockx, blocky, blockz);
        GameObject wolf = Instantiate(goWolf);

        // cube.AddComponent<Rigidbody>();

        wolf.transform.position = new Vector3(player.position.x + Random.Range(-20, 20), player.position.y+5, player.position.z + Random.Range(-20, 20));
        wolf.transform.parent = this.transform;
        wolf.tag = "Wolf";
        //wolf.GetComponent<MeshRenderer>().material = materialWolf;



    }

    IEnumerator Wait() {
        yield return new WaitForSeconds(waitTime);
        canSpawn = true;

    }
}
