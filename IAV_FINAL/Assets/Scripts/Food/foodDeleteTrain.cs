using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class foodDeleteTrain : MonoBehaviour
{
    public Transform player;
    bool canCreate;
    public GameObject food;

    // Start is called before the first frame update
    void Start() {
        canCreate = true;
    }

    // Update is called once per frame
    void Update() {

        if (canCreate) {
            canCreate = false;
            
            GameObject sFood = Instantiate(food);
            sFood.SetActive(true);
            sFood.transform.position = new Vector3(player.position.x + Random.Range(-15, 15), player.position.y+0.5f, player.position.z + Random.Range(-15, 15));
            sFood.transform.parent = this.transform;
            sFood.tag = "Food";
            
            this.StartCoroutine(TimeToCreate());
        }
    }

    IEnumerator TimeToCreate() {
        yield return new WaitForSeconds(3);
        canCreate = true;
    }
}
