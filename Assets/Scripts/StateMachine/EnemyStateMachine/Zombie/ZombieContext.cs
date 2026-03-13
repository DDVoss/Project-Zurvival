using UnityEngine;
using UnityEngine.AI;

public class ZombieContext
{
    public bool IsHit { get; set; }
    public float Health { get; set; }
    public float ChaseRange { get; }
    public float AttackRange { get; }
    public float ChaseSpeed { get; }
    
    public NavMeshAgent Agent { get; }
    public Transform Target { get; }
   
    
    
    public ZombieContext(float health, float chaseRange, float attackRange, float chaseSpeed, NavMeshAgent agent, Transform target)
    {
        Health = health;
        ChaseRange = chaseRange;
        AttackRange = attackRange;
        ChaseSpeed = chaseSpeed;
        
        Agent = agent;
        Target = target;
        
    }
}
