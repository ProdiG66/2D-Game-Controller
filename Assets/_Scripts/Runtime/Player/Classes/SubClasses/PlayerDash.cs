using UnityEngine;

namespace ProdiG.Classes.SubClasses {
	public static class PlayerDash {
		public static void HandleDash(PlayerInternals pI) {
			if (pI.dashToConsume && pI.canDash && !pI.Crouching) {
				Vector2 dir = new Vector2(pI.playerInputs.move.x, Mathf.Max(pI.playerInputs.move.y, 0f)).normalized;
				if (dir == Vector2.zero) {
					pI.dashToConsume = false;
					return;
				}

				pI.dashVel = dir * pI.stats.DashVelocity;
				pI.dashing = true;
				pI.canDash = false;
				pI.startedDashing = pI.fixedFrame;
				pI.playerEvents._dashingChangedDelegate(true, dir);
				pI.currentExternalVelocity = Vector2.zero;
			}

			if (pI.dashing) {
				pI.speed = pI.dashVel;
				if (pI.fixedFrame > pI.startedDashing + pI.stats.DashDurationFrames) {
					pI.dashing = false;
					pI.playerEvents._dashingChangedDelegate(false, Vector2.zero);
					if (pI.speed.y > 0) pI.speed.y = 0;
					pI.speed.x *= pI.stats.DashEndHorizontalMultiplier;
					if (pI.grounded) pI.canDash = true;
				}
			}

			pI.dashToConsume = false;
		}
	}
}