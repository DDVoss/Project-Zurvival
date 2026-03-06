using UnityEngine;

public class ZombieState : BaseState<ZombieStateMachine.EZombieState>
{
    protected ZombieContext Context;

    public ZombieState(ZombieContext context, ZombieStateMachine.EZombieState stateKey) : base(stateKey)
    {
        Context = context;
    }

    public override void EnterState() {}

    public override void ExitState() {}

    public override void UpdateState() {}

    public override ZombieStateMachine.EZombieState GetNextState()
    {
        return StateKey;
    }

    public override void OnTriggerEnter(Collider other) {}

    public override void OnTriggerStay(Collider other) {}

    public override void OnTriggerExit(Collider other) {}
}
