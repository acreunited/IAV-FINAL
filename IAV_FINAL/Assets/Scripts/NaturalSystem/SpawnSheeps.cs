using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSheeps : MonoBehaviour {

    public Transform player;
    //public Material materialSheep;
    private int waitTime = 20;
    private bool canSpawn;
    public GameObject goSheep;

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

        GameObject sheep = Instantiate(goSheep);
        sheep.transform.position = new Vector3(player.position.x + Random.Range(-10, 10), player.position.y+5, player.position.z + Random.Range(-10, 10));
        sheep.transform.parent = this.transform;
        sheep.tag = "Sheep";


    }

    IEnumerator Wait() {
        yield return new WaitForSeconds(waitTime);
        canSpawn = true;

    }
}
