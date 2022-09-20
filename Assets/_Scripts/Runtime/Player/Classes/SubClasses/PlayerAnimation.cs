using UnityEngine;

namespace ProdiG.Classes.SubClasses {
	public static class PlayerAnimation {
		public static readonly int Idle = Animator.StringToHash("Idle");
		public static readonly int Walk = Animator.StringToHash("Walk");
		public static readonly int Jump = Animator.StringToHash("Jump");
		public static readonly int Fall = Animator.StringToHash("Fall");
		public static readonly int Land = Animator.StringToHash("Land");
		public static readonly int Attack = Animator.StringToHash("Attack");
		public static readonly int Crouch = Animator.StringToHash("Crouch");

		public static void HandleAnimations(PlayerAnimator pA) {
			int state = GetState();
			ResetFlags();
			if (state == pA.currentState) return;

			pA.animator.Play(state, 0);
			pA.currentState = state;

			int GetState() {
				if (Time.time < pA.lockedTill) return pA.currentState;

				if (pA.attacked) return LockState(Attack, pA.attackAnimTime);

				if (pA.playerController.Crouching) return Crouch;
				if (pA.landed) return LockState(Land, pA.landAnimDuration);
				if (pA.jumpTriggered) return Jump;

				if (pA.grounded) return pA.playerController.Input.x == 0 ? Idle : Walk;
				return pA.playerController.Speed.y > 0 ? Jump : Fall;

				int LockState(int s, float t) {
					pA.lockedTill = Time.time + t;
					return s;
				}
			}

			void ResetFlags() {
				pA.jumpTriggered = false;
				pA.landed = false;
				pA.attacked = false;
			}
		}

		public static void UnlockAnimationLock(PlayerAnimator pA) {
			pA.lockedTill = 0f;
		}
	}
}