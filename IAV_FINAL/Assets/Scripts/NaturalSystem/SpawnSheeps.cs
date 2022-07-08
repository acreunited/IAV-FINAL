using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnSheeps : MonoBehaviour {

    public Transform player;
    //public Material materialSheep;
    private int waitSpawnTime = 25;
    private int waitDeleteDistanceTime = 10;
    private bool canSpawn;
    public GameObject goSheep;
    List<GameObject> allSheeps;
    private float distDestroy = 500f;
    public Text sheeps;


    // Start is called before the first frame update
    void Start() {
        canSpawn = true;
        allSheeps = new List<GameObject>();
    }

    // Update is called once per frame
    void Update() {
        if (canSpawn) {
            canSpawn = false;
            Spawn();
        }

        this.StartCoroutine( checkDist() );
        sheeps.text = "" + allSheeps.Count;

    }

    private void Spawn() {
        this.StartCoroutine(Wait());

        GameObject sheep = Instantiate(goSheep);
        sheep.SetActive(true);
        sheep.transform.position = new Vector3(player.position.x + Random.Range(-10, 10), player.position.y+5, player.position.z + Random.Range(-10, 10));
        sheep.transform.parent = this.transform;
        sheep.tag = "Sheep";
        allSheeps.Add(sheep);

    }

    private void deleteIfDist() {

        GameObject[] arrSheeps = allSheeps.ToArray();
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
        this.allSheeps = allSheeps;
    }

    IEnumerator checkDist() {
        yield return new WaitForSeconds(waitDeleteDistanceTime);
        deleteIfDist();
    }

    IEnumerator Wait() {
        yield return new WaitForSeconds(waitSpawnTime);
        canSpawn = true;

    }
}
