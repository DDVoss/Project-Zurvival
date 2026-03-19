using UnityEngine;
using UnityEngine.AI;

public class ZombieContext
{
    public bool IsHit { get; set; }
    public float ChaseRange { get; }
    public float AttackRange { get; }
    public float ChaseSpeed { get; }
    
    public NavMeshAgent Agent { get; }
    public Transform Target { get; }
   
    
    
    public ZombieContext(float chaseRange, float attackRange, float chaseSpeed, NavMeshAgent agent, Transform target)
    {
        ChaseRange = chaseRange;
        AttackRange = attackRange;
        ChaseSpeed = chaseSpeed;
        
        Agent = agent;
        Target = target;
        
    }
}
