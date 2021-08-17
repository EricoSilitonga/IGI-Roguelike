using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E3_StealthState : StealthState
{
    private Air enemy;
    public E3_StealthState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_Stealth stateData, Air enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void DoChecks()
    {
        base.DoChecks();
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
        if (isDetectingPlayer)
        {
            stateMachine.ChangeState(enemy.idleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
