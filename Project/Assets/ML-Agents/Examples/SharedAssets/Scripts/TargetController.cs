using UnityEngine;
using Random = UnityEngine.Random;
using Unity.MLAgents;
using UnityEngine.Events;

public class TargetController : MonoBehaviour
{
    [Header("Target To Walk Towards")] [Space(10)]
    public Transform target; //Target the agent will walk towards.
    public float targetSpawnRadius; //The radius in which a target can be randomly spawned.
    public bool detectTargets; //Should this agent detect targets
    public bool respawnTargetWhenTouched; //Should the target respawn to a different position when touched
    private Vector3 m_startingPos; //the starting position of the target
    private Agent m_agentTouching; //the agent currently touching the target

    public string tagToDetect;
    [System.Serializable]
    public class TriggerEvent : UnityEvent<Collider> {}
    [Header("Trigger Callbacks")]
    public bool triggerIsTouching;
    public TriggerEvent onTriggerEnterEvent = new TriggerEvent ();
    public TriggerEvent onTriggerStayEvent = new TriggerEvent ();
    public TriggerEvent onTriggerExitEvent = new TriggerEvent ();
    
    [System.Serializable]
    public class CollisionEvent : UnityEvent<Collision> {}
    [Header("Collision Callbacks")]
    public bool colliderIsTouching;
    public CollisionEvent onCollisionEnterEvent = new CollisionEvent ();
    public CollisionEvent onCollisionStayEvent = new CollisionEvent ();
    public CollisionEvent onCollisionExitEvent = new CollisionEvent ();
    
    // Start is called before the first frame update
    void OnEnable()
    {
        m_startingPos = target.position;
        if (respawnTargetWhenTouched)
        {
            MoveTargetToRandomPosition();
        }
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
    
    private void OnCollisionEnter(Collision col)
    {
        if (col.transform.CompareTag(tagToDetect))
        {
            colliderIsTouching = true;
            onCollisionEnterEvent.Invoke(col);
        }
    }
    
    private void OnCollisionStay(Collision col)
    {
        if (col.transform.CompareTag(tagToDetect))
        {
            colliderIsTouching = true;
            onCollisionStayEvent.Invoke(col);
        }
    }
    
    private void OnCollisionExit(Collision col)
    {
        if (col.transform.CompareTag(tagToDetect))
        {
            colliderIsTouching = false;
            onCollisionExitEvent.Invoke(col);
            if (respawnTargetWhenTouched)
            {
                MoveTargetToRandomPosition();
            }
        }
    }
    
    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag(tagToDetect))
        {
            triggerIsTouching = true;
            onTriggerEnterEvent.Invoke(col);
        }
    }
    
    private void OnTriggerStay(Collider col)
    {
        if (col.CompareTag(tagToDetect))
        {
            triggerIsTouching = true;
            onTriggerStayEvent.Invoke(col);
        }
    }
    
    private void OnTriggerExit(Collider col)
    {
        if (col.CompareTag(tagToDetect))
        {
            triggerIsTouching = false;
            onTriggerExitEvent.Invoke(col);
        }
    }
}
