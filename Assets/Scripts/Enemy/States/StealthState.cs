using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealthState : State
{
    protected D_Stealth stateData;
    protected bool isDetectingPlayer;
    public StealthState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_Stealth stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isDetectingPlayer = entity.CheckPlayerInDestealthRange();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }


}
