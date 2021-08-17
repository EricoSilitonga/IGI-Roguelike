using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Api : Entity
{
    public E2_IdleState idleState { get; private set; }
    public E2_MoveState moveState { get; private set; }
    public E2_PlayerDetected playerDetectedState { get; private set; }
    public E2_LookForPlayerState lookForPlayerState { get; private set; }
    public E2_RangedAttackState rangedAttackState { get; private set; }
    public E2_DeadState deadState { get; private set; }
    public E2_StunState stunState { get; private set; }

    [SerializeField]
    private D_IdleState idleStateData;
    [SerializeField]
    private D_MoveState moveStateData;
    [SerializeField]
    private D_PlayerDetected playerDetectedStateData;
    [SerializeField]
    private D_LookForPlayer lookForPlayerStateData;
    [SerializeField]
    private D_RangedAttack rangedAttackStateData;
    [SerializeField]
    private D_DeadState deadStateData;
    [SerializeField]
    private D_StunState stunStateData;
    [SerializeField]
    private Transform rangedAttackPosition;

    public override void Start()
    {
        base.Start();

        moveState = new E2_MoveState(this, stateMachine, "move",moveStateData, this);
        idleState = new E2_IdleState(this, stateMachine, "idle", idleStateData, this);
        playerDetectedState = new E2_PlayerDetected(this, stateMachine, "playerDetected", playerDetectedStateData, this);
        lookForPlayerState = new E2_LookForPlayerState(this, stateMachine, "lookForPlayer", lookForPlayerStateData, this);
        rangedAttackState = new E2_RangedAttackState(this, stateMachine, "rangedAttack",rangedAttackPosition, rangedAttackStateData, this);
        deadState = new E2_DeadState(this, stateMachine, "dead", deadStateData, this);
        stunState = new E2_StunState(this, stateMachine, "stun", stunStateData, this);
        stateMachine.Initialize(moveState);
    }

    public override void Damage(AttackDetails attackDetails)
    {
        base.Damage(attackDetails);
        if (isDead)
        {
            stateMachine.ChangeState(deadState);
        }
        else if (isStunned && stateMachine.currentState != stunState)
        {
            stateMachine.ChangeState(stunState);
        }
        else if (CheckPlayerInMinAgroRange())
        {
            stateMachine.ChangeState(rangedAttackState);
        }
    }

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
    }
}
