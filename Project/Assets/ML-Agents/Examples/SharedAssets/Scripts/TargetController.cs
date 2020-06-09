using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using Unity.MLAgents;

public class TargetController : MonoBehaviour
{
    [Header("Target To Walk Towards")] [Space(10)]
    public Transform target; //Target the agent will walk towards.
    public float targetSpawnRadius; //The radius in which a target can be randomly spawned.
    public bool detectTargets; //Should this agent detect targets
    public bool respawnTargetWhenTouched; //Should the target respawn to a different position when touched

    private Vector3 m_startingPos; //the starting position of the target

    private Agent m_agentTouching; //the agent currently touching the target
    // Start is called before the first frame update
    void OnEnable()
    {
        m_startingPos = target.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    /// <summary>
    /// Moves target to a random position within specified radius.
    /// </summary>
    public void MoveTargetToRandomPosition()
    {
        var newTargetPos = m_startingPos + (Random.insideUnitSphere * targetSpawnRadius);
        newTargetPos.y = 5;
        target.position = newTargetPos;
    }

    private void OnCollisionEnter(Collision other)
    {
        m_agentTouching = other.transform.GetComponentInParent<Agent>();
        if (m_agentTouching)
        {
//            m_agentTouching.
        }
        throw new NotImplementedException();
    }
}
