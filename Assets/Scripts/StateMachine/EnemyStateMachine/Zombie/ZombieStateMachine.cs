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
        Attack,
        Hit
    }

    private ZombieContext _context;

    private float health;
    [SerializeField] private float chaseRange;
    [SerializeField] private float attackRange;
    [SerializeField] private float chaseSpeed;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Transform target;

    private ZombieHealth _zombieHealth;
    

    private void Awake()
    {
        ValidateVariables();
        _context = new ZombieContext(chaseRange, attackRange, chaseSpeed, agent, target);
        _zombieHealth = GetComponent<ZombieHealth>();
        _zombieHealth.Initialize(_context);
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
        // Populate the dictionary with enum-to-state mappings
        States.Add(EZombieState.Patrol, new ZombiePatrolState(_context, EZombieState.Patrol));
        States.Add(EZombieState.Chase, new ZombieChaseState(_context, EZombieState.Chase));
        States.Add(EZombieState.Attack, new ZombieAttackState(_context, EZombieState.Attack));
        States.Add(EZombieState.Hit, new ZombieHitState(_context, EZombieState.Hit));
        
        // Start in the Patrol state
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
