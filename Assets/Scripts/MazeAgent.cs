using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class MazeAgent : Agent
{
    [SerializeField] private Transform targetTransform;
    [SerializeField] private Material winMaterial;
    [SerializeField] private Material loseMaterial;
    [SerializeField] private MeshRenderer platformMeshRenderer;


    public override void OnEpisodeBegin()
    {
        transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
        //transform.position = Vector3.zero;
    }

    // Get state (overservation) from the environment
    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.localPosition);
        sensor.AddObservation(targetTransform.localPosition);
    }

    // For actions
    public override void OnActionReceived(ActionBuffers actions)
    {
        int moveX = actions.DiscreteActions[0] - 1;
        int moveZ = actions.DiscreteActions[1] - 1;

        float speed = 3f;
        transform.localPosition += new Vector3(moveX, 0, moveZ) * Time.deltaTime * speed;
    }

    // Give the reward as feedback
    private void OnTriggerEnter(Collider other)
    {
        // positive reward
        if(other.TryGetComponent<Goal>(out Goal goal))
        {
            SetReward(1f);
            platformMeshRenderer.material = winMaterial;
            EndEpisode();
        }

        // negative reward
        // Wall
        if (other.TryGetComponent<Wall>(out Wall wall))
        {
            SetReward(-1f);
            platformMeshRenderer.material = loseMaterial;
            EndEpisode();
        }

        // Trap
        if (other.TryGetComponent<Trap>(out Trap trap))
        {
            SetReward(-10f);
            platformMeshRenderer.material = loseMaterial;
            EndEpisode();
        }
    }
}
