using UnityEngine;

namespace ProdiG.Classes.SubClasses {
	public static class PlayerHorizontal {
		public static void HandleHorizontal(ref PlayerInternals pI) {
			if (pI.dashing) return;

			if (pI.playerInputs.move.x != 0) {
				if (pI.Crouching && pI.grounded) {
					float crouchPoint = Mathf.InverseLerp(0, pI.stats.CrouchSlowdownFrames,
						pI.fixedFrame - pI.frameStartedCrouching);
					float diminishedMaxSpeed =
						pI.stats.MaxSpeed * Mathf.Lerp(1, pI.stats.CrouchSpeedPenalty, crouchPoint);

					pI.speed.x = Mathf.MoveTowards(pI.speed.x, diminishedMaxSpeed * pI.playerInputs.move.x,
						pI.stats.GroundDeceleration * Time.fixedDeltaTime);
				}
				else {
					float inputX = pI.playerInputs.move.x;
					pI.speed.x = Mathf.MoveTowards(pI.speed.x, inputX * pI.stats.MaxSpeed,
						pI.currentWallJumpMoveMultiplier * pI.stats.Acceleration * Time.fixedDeltaTime);
				}
			}
			else {
				pI.speed.x = Mathf.MoveTowards(pI.speed.x, 0,
					(pI.grounded ? pI.stats.GroundDeceleration : pI.stats.AirDeceleration) * Time.fixedDeltaTime);
			}
		}
	}
}