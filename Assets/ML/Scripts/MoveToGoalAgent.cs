using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class MoveToGoalAgent : Agent
{
    [SerializeField] private float moveSpeed =1f; 
    [SerializeField] private Transform targetTransform; 
    [SerializeField] private Material winMat;
    [SerializeField] private Material loseMat ;
    [SerializeField] private MeshRenderer flooreMesh;
    private Transform _transform = null;
    private void Start() 
    {
        _transform = transform;
    }
    public override void OnEpisodeBegin()
    {
        _transform.localPosition = Vector3.zero;
    }
    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(_transform.localPosition);
        sensor.AddObservation(targetTransform.localPosition);
    }
    public override void OnActionReceived(ActionBuffers actions)
    {
        float moveX = actions.ContinuousActions[0];
        float moveZ = actions.ContinuousActions[1];

        _transform.localPosition+= new Vector3(moveX,0,moveZ)*Time.deltaTime * moveSpeed;
    }
    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var actions = actionsOut.ContinuousActions;
        actions[0] = Input.GetAxisRaw("Horizontal");
        actions[1] = Input.GetAxisRaw("Vertical");
    }
    private void OnTriggerEnter(Collider other) {
        if(other.TryGetComponent<WallTrigger>(out WallTrigger wall)){
            SetReward(-1f);
            flooreMesh.material = loseMat;
            EndEpisode();
        }
        if(other.TryGetComponent<TargetTrigger>(out TargetTrigger target)){
            SetReward(+1f);
            flooreMesh.material = winMat ;
            EndEpisode();
        }
    }

}
