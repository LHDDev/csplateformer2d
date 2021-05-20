using Godot;
using System;

namespace TestCs.StateMachine.States
{
    class AttackState : StateBase
    {
        private int comboNextId;
        private int comboCurrentId;

        public async override void _Ready()
        {
            base._Ready();
            await ToSignal(actor, "ready");
            actor.ActorAnimation.Connect("animation_finished", this, nameof(CheckCombo));
        }

        public override void EnterState()
        {
            comboCurrentId = 0;
            comboNextId = 0;
            actor.ActorAnimation.Play(FormatAnimationName("mc-attack"));
            actor.UpdateSP(10);
        }

        public override void Do()
        {
        }

        private void CheckCombo(string finishedAnimationName)
        {
            if(comboCurrentId == comboNextId)
            {
                finiteStateMachine.ChangeState<IdleState>();
                return;
            }

            comboCurrentId = comboNextId;
            PlayNextAttack();
        }

        private void PlayNextAttack()
        {
            actor.ActorAnimation.Play(FormatAnimationName("mc-attack"));
            actor.UpdateSP(10);

        }

        private string FormatAnimationName(string animationName)
        {
            string formatedAnimationName = $"{animationName}-{comboCurrentId}";
            if (actor.ActorSprite.FlipH)
            {
                formatedAnimationName = $"{formatedAnimationName}-right";
            }
            return formatedAnimationName;
        }
    }
}
