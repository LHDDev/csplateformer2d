

using Godot;
using Heimgaerd.Core.Audio;
using Heimgaerd.StateMachine;

namespace Heimgaerd.StateMachine.States.AttackStates
{
    class DashAttack : StateBase
    {
        private int comboCurrentId;

        public override void EnterState()
        {
            actor.CurrentComboID = 0;
            actor.CurrentDashComboID = 0;
            actor.ActorAnimation.Play(FormatAnimationName("mc-dash-attack"));
            actor.UpdateSP(10);
            actor.AudioController.PlaySFX(AudioTypes.SFX_SWORD_SWOOSH_PATH);
        }
        public override void Do()
        {
            actor.Velocity.x = 150 * actor.FacingDirection;
            if (!actor.ActorAnimation.IsPlaying())
            {
                actor.CurrentDashComboID++;
                finiteStateMachine.ChangeState<GroundTransition>();
            }
        }

        private string FormatAnimationName(string animationName)
        {
            string formatedAnimationName = $"{animationName}-{actor.CurrentDashComboID}";
            if (actor.ActorSprite.FlipH)
            {
                formatedAnimationName = $"{formatedAnimationName}-right";
            }
            GD.Print($"{formatedAnimationName}");
            return formatedAnimationName;
        }

        public override bool CanHook()
        {
            return false;
        }
    }
}
