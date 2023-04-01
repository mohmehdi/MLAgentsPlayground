using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class Move3DAgent : Agent
{
    [SerializeField] private float moveSpeed =1f; 
    [SerializeField] private float rotateSpeed =1f;
    [SerializeField] private Transform targetTransform; 
    [SerializeField] private Material winMat;
    [SerializeField] private Material loseMat ;
    [SerializeField] private MeshRenderer flooreMesh;
    private Transform _transform = null;
    private Rigidbody _agentRB = null;

    private void Start() 
    {
        _transform = transform;
        _agentRB = GetComponent<Rigidbody>();
    }
    public override void OnEpisodeBegin()
    {
        _transform.localPosition = new Vector3(Random.Range(-9,9),Random.Range(-9,9),Random.Range(-20,0));
        // _transform.localPosition = Vector3.zero;
        _transform.localEulerAngles = Vector3.zero;
        _agentRB.velocity = Vector3.zero;
        _agentRB.angularVelocity = Vector3.zero;
        targetTransform.localPosition=new Vector3(Random.Range(-9,9),Random.Range(-9,9),Random.Range(5,30));
    }
    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(_transform.localPosition);
        sensor.AddObservation(_transform.localEulerAngles);
        sensor.AddObservation(targetTransform.localPosition);
    }
    public override void OnActionReceived(ActionBuffers actions)
    {
        float moveZ = actions.DiscreteActions[0]-1;
        float rotX = actions.DiscreteActions[1]-1;
        float rotZ = actions.DiscreteActions[2]-1;
        _agentRB.AddForce(_transform.forward*moveZ*moveSpeed,ForceMode.VelocityChange);
        _agentRB.AddRelativeTorque(new Vector3(rotX,rotZ,0)*rotateSpeed); 
        float dot = Vector3.Dot(_transform.forward,targetTransform.forward);
        float distance = (_transform.position-targetTransform.position).sqrMagnitude;
        AddReward(dot);
        AddReward(-1);
    }
    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var actions = actionsOut.DiscreteActions;
        actions[0] = 1;
        actions[1] = 1;
        actions[2] = 1;
        if(Input.GetKey(KeyCode.W))
            actions[0] = 2;
        if(Input.GetKey(KeyCode.S))
            actions[0] = 0;
        if(Input.GetKey(KeyCode.DownArrow))
            actions[1] = 2;
        if(Input.GetKey(KeyCode.UpArrow))
            actions[1] = 0;
        if(Input.GetKey(KeyCode.RightArrow))
            actions[2] = 2;
        if(Input.GetKey(KeyCode.LeftArrow))
            actions[2] = 0;
    }
    private void OnTriggerEnter(Collider other) {
        if(other.TryGetComponent<WallTrigger>(out WallTrigger wall)){
            SetReward(-50f);
            flooreMesh.material = loseMat;
            EndEpisode();
        }
        if(other.TryGetComponent<TargetTrigger>(out TargetTrigger target)){
            SetReward(+50f);
            flooreMesh.material = winMat;
            EndEpisode();
        }
    }

}
