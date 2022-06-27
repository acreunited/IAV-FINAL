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
    private float loseHPPerTime = 0.05f;
    private float gainHPEat = 50f;

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
    }

    private void Start() {
        ui_s = ui.GetComponent<UI>();
        ui_s.RegisterHP(this.gameObject);
        thisHP = 100f;
    }

    private void Update() {

        if (this.getHP() <= 0f) {
            Destroy(this.gameObject);
        }
        else {
            this.setThisHP(this.getHP() - loseHPPerTime);
        }

    }

    private float getHP() {
        return this.thisHP;
    }
    private void setThisHP(float thisHP) {
        this.thisHP = (thisHP < 100f) ? thisHP : 100f;
        ui_s.SetHp(this.gameObject, this.getHP(), 100f);
    }

}
