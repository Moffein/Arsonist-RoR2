﻿using System;
using EntityStates;

namespace ArsonistMod.SkillStates.EmoteStates
{
    public class EmoteStrut: BaseEmoteState
    {
        public override void OnEnter()
        {
            base.OnEnter();

            //Play the animation instantly, no scaling, nothing.
        }

        public override void OnExit()
        {
            base.OnExit();
            base.PlayAnimation("Fullbody, Override", "BufferEmpty");
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            //Do not exit state automatically.
        }

        public override void PlayEmoteAnim()
        {
            base.PlayAnimation("FullBody, Override", "Emote2");
        }

        public override InterruptPriority GetMinimumInterruptPriority()
        {
            return InterruptPriority.Skill;
        }
    }
}
