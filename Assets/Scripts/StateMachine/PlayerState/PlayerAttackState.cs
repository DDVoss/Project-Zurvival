using NUnit.Framework.Constraints;
using UnityEngine;

public class PlayerAttackState : PlayerState
{
    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 0.5f; // Time between shots in seconds
    public bool isFullAuto = false; // false = semi-auto (pistol), true = full-auto (SMG)
    
    private float _nextFireTime = 0f;
    private bool _hasFiredThisTriggerPull;

    private ParticleSystem muzzleFlash;
    
    public PlayerAttackState(PlayerContext context, PlayerStateMachine.EPlayerState estate) : base(context, estate)
    {
    }

    public override void EnterState()
    {
        Debug.Log("Entering attack State");
        // Reset the trigger pull state when entering the attack state
        _hasFiredThisTriggerPull = false;
        
        // Assuming the muzzle flash is a child of the MuzzlePointTransform
        // Hardcoding the muzzle flash retrieval for now
        muzzleFlash = Context.MuzzlePointTransform.GetComponentInChildren<ParticleSystem>();
    }

    public override void ExitState()
    {
        Debug.Log("Exiting attack State");
    }

    public override void UpdateState()
    {
        ApplyGravity();
 
        bool attackPressed = Context.PlayerLocomotionInput.AttackInput;

        if (isFullAuto)
        {
            if (attackPressed && Time.time >= _nextFireTime)
            {
                Shoot();
                _nextFireTime = Time.time + fireRate;
            }
        }
        else // Semi-auto logic
        {
            if (attackPressed && !_hasFiredThisTriggerPull && Time.time >= _nextFireTime)
            {
                Shoot();
                _nextFireTime = Time.time + fireRate;
                _hasFiredThisTriggerPull = true;
            }
            else if (!attackPressed)
            {
                _hasFiredThisTriggerPull = false; // Reset for next trigger pull
            }
        }

    }

    public override PlayerStateMachine.EPlayerState GetNextState()
    {
        if (!Context.PlayerLocomotionInput.AimInput)
        {
            return PlayerStateMachine.EPlayerState.Locomotion;
        }
        if (!Context.PlayerLocomotionInput.AttackInput)
        {
            return PlayerStateMachine.EPlayerState.Aiming;
        }
        return StateKey; // stay in this state by default
    }

    public override void OnTriggerEnter(Collider other) { }

    public override void OnTriggerStay(Collider other) { }

    public override void OnTriggerExit(Collider other) { }

    public void Shoot()
    {
        muzzleFlash.Play();
 
        RaycastHit hit;
        if ( Physics.Raycast(Context.MuzzlePointTransform.position, Context.MuzzlePointTransform.forward, out hit, range))
        {
            IDamageable damageable = hit.transform.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(damage);
                Debug.DrawRay(Context.MuzzlePointTransform.position, Context.MuzzlePointTransform.forward * 10f, Color.red, 1f);

            }
        }
    }
}