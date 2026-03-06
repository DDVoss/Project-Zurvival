using System;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.AI;

public class ZombieStateMachine : StateManager<ZombieStateMachine.EZombieState>
{

    public enum EZombieState
    {
        Patrol,
        Chase,
        Attack
    }

    private ZombieContext _context;
    
    [SerializeField] private float chaseRange;
    [SerializeField] private float attackRange;
    [SerializeField] private float chaseSpeed;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Transform target;

    private void Awake()
    {
        ValidateVariables();
        _context = new ZombieContext(chaseRange, attackRange, chaseSpeed, agent, target);
        InitializeStates();
        
    }

    private void ValidateVariables()
    {
        Assert.Greater(chaseRange, 0f, "chaseRange must be greater than 0.");
        Assert.Greater(attackRange, 0f, "attackRange must be greater than 0.");
        Assert.Greater(chaseSpeed, 0f, "chaseSpeed must be greater than 0.");
        Assert.IsNotNull(agent, "NavMeshAgent is not assigned in the inspector.");
        Assert.IsNotNull(target, "Target Transform is not assigned in the inspector.");
    }


    private void InitializeStates()
    {
        States.Add(EZombieState.Patrol, new ZombiePatrolState(_context, EZombieState.Patrol));
        States.Add(EZombieState.Chase, new ZombieChaseState(_context, EZombieState.Chase));
        States.Add(EZombieState.Attack, new ZombieAttackState(_context, EZombieState.Attack));
        CurrentState = States[EZombieState.Patrol];
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
