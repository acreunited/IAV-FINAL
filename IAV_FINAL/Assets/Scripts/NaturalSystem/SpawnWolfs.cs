using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnWolfs : MonoBehaviour {

    public Transform player;
    //public Material materialWolf;
    private int waitTime = 100;
    private bool canSpawn;
    public GameObject goWolf;
    //List<GameObject> allWolfs;
    private float distDestroy = 500f;
    private int waitDeleteDistanceTime = 10;
    public Text wolfs;

    // Start is called before the first frame update
    void Start() {
        canSpawn = true;
       // allWolfs = new List<GameObject>();

       
    }

    // Update is called once per frame
    void Update() {
        if (canSpawn) {
            canSpawn = false;
            Spawn(20);
        }
        
        this.StartCoroutine(checkDist());
        wolfs.text = "" + Helper.allWolfs.Count;

    }

    private void Spawn(int n) {
        //this.StartCoroutine(Wait());
        //GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        //cube.transform.position = new Vector3(blockx, blocky, blockz);

        for (int i = 0; i < n; i++) {
            GameObject wolf = Instantiate(goWolf);
            wolf.SetActive(true);
            wolf.transform.position = new Vector3(player.position.x + Random.Range(-25, 25), player.position.y+5, player.position.z + Random.Range(-25, 25));
           
            wolf.transform.parent = this.transform;
            wolf.tag = "Wolf";

            Helper.allWolfs.Add(wolf);
        }

        

    }

    private void deleteIfDist() {

        if (Helper.allWolfs.ToArray().Length > 0) {
            GameObject[] arrWolfs = Helper.allWolfs.ToArray();
            List<GameObject> updateWolfs = new List<GameObject>();

            for (int i = 0; i < arrWolfs.Length; i++) {
                if (arrWolfs[i] != null) {
                    float dist = Vector3.Distance(player.position, arrWolfs[i].transform.position);
                    if (dist > distDestroy) {
                        Destroy(arrWolfs[i]);
                        arrWolfs[i] = null;
                    }
                    else {
                        updateWolfs.Add(arrWolfs[i]);
                    }
                }
                
            }
            setAllWolfs(updateWolfs);
        }
        
    }

    private void setAllWolfs(List<GameObject> allWolfs) {
        Helper.allWolfs = allWolfs;
    }

    IEnumerator checkDist() {
        yield return new WaitForSeconds(waitDeleteDistanceTime);
        deleteIfDist();
 
    }

    IEnumerator Wait() {
        yield return new WaitForSeconds(waitTime);
       // canSpawn = true;

    }
}
