using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnWolfs : MonoBehaviour {

    public Transform player;
    //public Material materialWolf;
    private int waitTime = 50;
    private bool canSpawn;
    public GameObject goWolf;
    List<GameObject> allWolfs;
    private float distDestroy = 500f;
    private int waitDeleteDistanceTime = 10;

    // Start is called before the first frame update
    void Start() {
        canSpawn = true;
        allWolfs = new List<GameObject>();
    }

    // Update is called once per frame
    void Update() {
        if (canSpawn) {
            canSpawn = false;
            Spawn();
        }

        this.StartCoroutine(checkDist());

    }

    private void Spawn() {
        this.StartCoroutine(Wait());
        //GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        //cube.transform.position = new Vector3(blockx, blocky, blockz);
        GameObject wolf = Instantiate(goWolf);
        wolf.SetActive(true);
        wolf.transform.position = new Vector3(player.position.x + Random.Range(-10, 10), player.position.y+5, player.position.z + Random.Range(-10, 10));
        wolf.transform.parent = this.transform;
        wolf.tag = "Wolf";
        //wolf.GetComponent<MeshRenderer>().material = materialWolf;

        allWolfs.Add(wolf);

    }

    private void deleteIfDist() {

        GameObject[] arrWolfs = allWolfs.ToArray();
        List<GameObject> updateWolfs = new List<GameObject>();

        for (int i = 0; i < arrWolfs.Length; i++) {
            float dist = Vector3.Distance(player.position, arrWolfs[i].transform.position);
            if (dist > distDestroy) {
                Destroy(arrWolfs[i]);
                arrWolfs[i] = null;
            }
            else {
                updateWolfs.Add(arrWolfs[i]);
            }
        }
        setAllWolfs(updateWolfs);
    }

    private void setAllWolfs(List<GameObject> allWolfs) {
        this.allWolfs = allWolfs;
    }

    IEnumerator checkDist() {
        yield return new WaitForSeconds(waitDeleteDistanceTime);
        deleteIfDist();
    }

    IEnumerator Wait() {
        yield return new WaitForSeconds(waitTime);
        canSpawn = true;

    }
}
