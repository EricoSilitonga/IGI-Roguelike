using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E2_PlayerDetected : PlayerDetectedState
{
    private Api enemy;
    public E2_PlayerDetected(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_PlayerDetected stateData, Api enemy) : base(entity, stateMachine, animBoolName, stateData)
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
        SoundManager.PlaySound("apiSound");
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (performLongRangeAction)
        {
            stateMachine.ChangeState(enemy.rangedAttackState);
        }
        else if (!isPlayerInMaxAgroRange)
        {
            stateMachine.ChangeState(enemy.lookForPlayerState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
