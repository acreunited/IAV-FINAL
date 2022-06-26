using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class Wolf : Agent {

    private int i = 0;

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



}
