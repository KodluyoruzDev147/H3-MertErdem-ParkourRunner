using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.PlayerControl
{
    public abstract class State
    {
        protected PlayerController player;
        protected Animator animator;

        public abstract void OnUpdate();

        public virtual void OnStateEnter() { }
        public virtual void OnStateExit() { }

        public State(PlayerController player)
        {
            this.player = player;
            animator = player.animator;
        }
    }

    public class IdleState : State
    {
        public IdleState(PlayerController player) : base(player)
        {

        }

        public override void OnUpdate()
        {

        }
    }

    public class RunState : State
    {
        public RunState(PlayerController player) : base(player)
        {

        }

        public override void OnStateEnter()
        {
            animator.SetTrigger("Run");
        }

        public override void OnUpdate()
        {
            player.Move();
        }
    }

    public class DanceState : State
    {
        public DanceState(PlayerController player) : base(player)
        {

        }

        public override void OnStateEnter()
        {
            animator.SetTrigger("Dance");
        }

        public override void OnUpdate()
        {

        }
    }
}
