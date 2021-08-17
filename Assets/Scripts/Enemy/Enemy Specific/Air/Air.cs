using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Air : Entity
{
    public E3_IdleState idleState { get; private set; }
    public E3_MoveState moveState { get; private set; }
    public E3_PlayerDetectedState playerDetectedState { get; private set; }
    public E3_LookForPlayerState lookForPlayerState { get; private set; }
    public E3_RangedAttackState rangedAttackState { get; private set; }
    public E3_DeadState deadState { get; private set; }
    public E3_StunState stunState { get; private set; }
    public E3_StealthState stealthState { get; private set; }

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
    private D_Stealth stealthStateData;
    [SerializeField]
    private Transform rangedAttackPosition;


    public override void Start()
    {
        base.Start();

        moveState = new E3_MoveState(this, stateMachine, "move", moveStateData, this);
        idleState = new E3_IdleState(this, stateMachine, "idle", idleStateData, this);
        playerDetectedState = new E3_PlayerDetectedState(this, stateMachine, "playerDetected", playerDetectedStateData, this);
        lookForPlayerState = new E3_LookForPlayerState(this, stateMachine, "lookForPlayer", lookForPlayerStateData, this);
        rangedAttackState = new E3_RangedAttackState(this, stateMachine, "rangedAttack", rangedAttackPosition, rangedAttackStateData, this);
        deadState = new E3_DeadState(this, stateMachine, "dead", deadStateData, this);
        stunState = new E3_StunState(this, stateMachine, "stun", stunStateData, this);
        stealthState = new E3_StealthState(this, stateMachine, "hidden", stealthStateData, this);
        stateMachine.Initialize(stealthState);
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
