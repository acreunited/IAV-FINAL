using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class Wolf : Agent {

    private int i = 0;
    public GameObject ui;
    private UI ui_s;
    private float thisHP;
    private float loseHPPerTime = 0.02f;
    private float gainHPEat = 50f;
    private bool canDelete;
    private int timeAlive;
    private int timeReproduce = 60; //segundos

    [SerializeField] private AudioSource dieSheepSound;
    [SerializeField] private AudioSource dieWolfSound;

    public override void OnEpisodeBegin() {
        //transform.localPosition = new Vector3(Random.Range(-13f, 13f), 0, Random.Range(-12f, 12f));
    }

    public override void OnActionReceived(ActionBuffers actions) {

        float moveSpeed = 30f;
        float rotateSpeed = 300f;
        float move = actions.ContinuousActions[0];
        float rotate = actions.ContinuousActions[1];
        transform.Rotate(new Vector3(0, rotate * Time.fixedDeltaTime * rotateSpeed, 0));
        transform.localPosition += transform.forward * move * Time.fixedDeltaTime * moveSpeed;

        AddReward(-0.0001f);


    }
    public override void Heuristic(in ActionBuffers actionsOut) {
        ActionSegment<float> ca = actionsOut.ContinuousActions;
        ca[0] = Input.GetAxis("Vertical");
        ca[1] = Input.GetAxis("Horizontal");
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Sheep")) {
            Destroy(other.gameObject);
            this.setThisHP(this.getHP() + gainHPEat);
            AddReward(1f);
            dieSheepSound.Play();
        }
        if (other.gameObject.CompareTag("Wall")) {
            AddReward(-1f);
        }
        if (other.gameObject.CompareTag("Player")) {
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
        if (timeAlive >= 50*timeReproduce) {
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
        GameObject wolf = Instantiate(this.gameObject);
        wolf.transform.position = new Vector3(this.transform.position.x + Random.Range(-5, 5), this.transform.position.y, this.transform.position.z + Random.Range(-5, 5));
        wolf.tag = "Wolf";

        Helper.allWolfs.Add(wolf);
    }

    private float getHP() {
        return this.thisHP;
    }
    private void setThisHP(float thisHP) {
        this.thisHP = (thisHP < 100f) ? thisHP : 100f;
        ui_s.SetHp(this.gameObject, this.getHP(), 100f);
    }

    IEnumerator WaitDestroy() {
        dieWolfSound.Play();
        yield return new WaitForSeconds(0.5f);
        Destroy(this.gameObject);
        canDelete = true;
    }

}
