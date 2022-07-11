using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{

    public Material materialFood;
    public Camera cam;
    bool canCreate;
    public GameObject food;

    [SerializeField] private AudioSource foodSound;

    // Start is called before the first frame update
    void Start()
    {
        canCreate = true;

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.F) && canCreate) {
            canCreate = false;
            RaycastHit hit;
            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, 10)) {

                string chunkName = hit.collider.gameObject.name;
                float chunkx = hit.collider.gameObject.transform.position.x;
                float chunky = hit.collider.gameObject.transform.position.y;
                float chunkz = hit.collider.gameObject.transform.position.z;

                Vector3 hitBlock = hit.point + hit.normal / 2f;

                Chunk c;
                if (World.chunkDict.TryGetValue(chunkName, out c)) {
                    GameObject sFood = Instantiate(food);
                    sFood.SetActive(true);
                    sFood.transform.position = new Vector3((int)(Mathf.Round(hitBlock.x)), (int)(Mathf.Round(hitBlock.y)), (int)(Mathf.Round(hitBlock.z)));
                    sFood.transform.parent = this.transform;
                    sFood.tag = "Food";
                    
                    foodSound.Play();
                }
            }
            this.StartCoroutine(TimeToCreate());
        }
    }

    IEnumerator TimeToCreate() {
        yield return new WaitForSeconds(1);
        canCreate = true;
    }
}
