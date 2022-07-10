using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnSheeps : MonoBehaviour {

    public Transform player;
    //public Material materialSheep;
    private int waitSpawnTime = 100;
    private int waitDeleteDistanceTime = 10;
    private bool canSpawn;
    public GameObject goSheep;
    //List<GameObject> allSheeps;
    private float distDestroy = 500f;
    public Text sheeps;


    // Start is called before the first frame update
    void Start() {
        canSpawn = true;
       // allSheeps = new List<GameObject>();

       // Spawn(10);
    }

    // Update is called once per frame
    void Update() {
        if (canSpawn) {
            canSpawn = false;
            Spawn(35);
            
        }

        this.StartCoroutine( checkDist() );
        sheeps.text = "" + Helper.allSheeps.Count;

    }

    

    public void Spawn(int n) {
        //this.StartCoroutine(Wait());

        for (int i = 0; i < n; i++) {
            GameObject sheep = Instantiate(goSheep);
            sheep.SetActive(true);
            sheep.transform.position = new Vector3(player.position.x + Random.Range(-30, 30), player.position.y + 20, player.position.z + Random.Range(-30, 30));
            sheep.transform.parent = this.transform;
            sheep.tag = "Sheep";
            Helper.allSheeps.Add(sheep);
        }

    }

    private void deleteIfDist() {

        GameObject[] arrSheeps = Helper.allSheeps.ToArray();
        List<GameObject> updateSheeps = new List<GameObject>();

        for (int i = 0; i < arrSheeps.Length; i++) {
            if (arrSheeps[i]!=null) {
                float dist = Vector3.Distance(player.position, arrSheeps[i].transform.position);
                if (dist > distDestroy) {
                    Destroy(arrSheeps[i]);
                    arrSheeps[i] = null;
                }
                else {
                    updateSheeps.Add(arrSheeps[i]);
                }
            }
            
        }
        setAllSheeps(updateSheeps);
    }

    private void setAllSheeps(List<GameObject> allSheeps) {
        Helper.allSheeps = allSheeps;
    }

    IEnumerator checkDist() {
        yield return new WaitForSeconds(waitDeleteDistanceTime);
        deleteIfDist();
    }

    IEnumerator Wait() {
        yield return new WaitForSeconds(waitSpawnTime);
        //canSpawn = true;

    }

   
}
