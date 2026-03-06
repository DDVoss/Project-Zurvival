using UnityEngine;
using UnityEngine.AI;

public class ZombieContext
{ 
    private float _chaseRange;
    private float _attackRange;
    private float _chaseSpeed;
    
    private NavMeshAgent _agent;
    private Transform _target;
    
    
    public ZombieContext(float chaseRange, float attackRange, float chaseSpeed, NavMeshAgent agent, Transform target)
    {
        _chaseRange = chaseRange;
        _attackRange = attackRange;
        _chaseSpeed = chaseSpeed;
        
        _agent = agent;
        _target = target;
        
    }
    
    // readonly properties
    public float ChaseRange => _chaseRange;
    public float AttackRange => _attackRange;
    public float ChaseSpeed => _chaseSpeed;
    public NavMeshAgent Agent => _agent;
    public Transform Target => _target;
}
