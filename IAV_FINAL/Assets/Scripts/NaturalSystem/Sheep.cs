using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class Sheep : Agent {

    private int i = 0;
    public GameObject ui;
    private UI ui_s;
    private float thisHP;
    private float loseHPPerTime = 0.02f;
    private float gainHPEat = 50f;
    private bool canDelete;
    private int timeAlive;
    private int timeReproduce = 300; //segundos
    public Transform player;

    [SerializeField] private AudioSource dieSheepSound;
    [SerializeField] private AudioSource eatSound;

    public override void OnActionReceived(ActionBuffers actions) {

        float moveSpeed = 50f;
        float rotateSpeed = 500f;
        float move = actions.ContinuousActions[0];
        float rotate = actions.ContinuousActions[1];
        transform.Rotate(new Vector3(0, rotate * Time.fixedDeltaTime * rotateSpeed, 0));
        transform.localPosition += transform.forward * move * Time.fixedDeltaTime * moveSpeed;

        //Debug.Log(Vector3.Distance(player.position, transform.localPosition));
        if (Vector3.Distance(player.position, transform.localPosition) > 30f) {
            AddReward(-0.1f);
        }
       /* else {
            AddReward(0.1f);
        }*/

        AddReward(-0.0001f);


    }
    public override void Heuristic(in ActionBuffers actionsOut) {
        ActionSegment<float> ca = actionsOut.ContinuousActions;
        ca[0] = Input.GetAxis("Vertical");
        ca[1] = Input.GetAxis("Horizontal");
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Food")) {
            eatSound.Play();
            Destroy(other.gameObject);
            this.setThisHP(this.getHP() + gainHPEat);
            
            AddReward(1.5f);
        }
        if (other.gameObject.CompareTag("Wall")) {
            AddReward(-1f);
        }
       

        EndEpisode();
    }


    // Update is called once per frame
    private void FixedUpdate() {
        if (i++ % 5 == 0) {
            RequestDecision();
        }

        timeAlive++;
        if (timeAlive >= 50 * timeReproduce) {
            timeAlive = 0;
            duplicate();
        }
    }

    private void Start() {
        ui_s = ui.GetComponent<UI>();
        ui_s.RegisterHP(this.gameObject);

        thisHP = 100f;
        canDelete = true;
        timeAlive = 0;
    }
    private void Update() {

        if (this.getHP() <= 0f) {

            if (canDelete) {
                canDelete = false;
                this.StartCoroutine(WaitDestroy());
            }

        }
        else {
            this.setThisHP(this.getHP() - loseHPPerTime);
            
        }

        
       
    }

    private void duplicate() {
        GameObject sheep = Instantiate(this.gameObject);
        sheep.transform.position = new Vector3(this.transform.position.x + Random.Range(-5, 5), this.transform.position.y, this.transform.position.z + Random.Range(-5, 5));
        sheep.tag = "Sheep"; 
        Helper.allSheeps.Add(sheep);
    }

    private float getHP() {
        return this.thisHP;
    }
    private void setThisHP(float thisHP) {
        this.thisHP = (thisHP < 100f) ? thisHP : 100f;
        ui_s.SetHp(this.gameObject, this.getHP(), 100f);
    }

    IEnumerator WaitDestroy() {
        dieSheepSound.Play();
        yield return new WaitForSeconds(0.5f);
        Destroy(this.gameObject);
        canDelete = true;
    }

}
