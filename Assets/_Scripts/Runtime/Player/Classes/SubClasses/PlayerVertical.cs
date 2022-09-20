using UnityEngine;

namespace ProdiG.Classes.SubClasses {
	public static class PlayerVertical {
		public static void HandleVertical(ref PlayerInternals pI) {
			if (pI.dashing) return;
			if (pI.grounded && pI.speed.y <= 0f) {
				pI.speed.y = pI.stats.GroundingForce;
				Physics2D.queriesHitTriggers = false;
				RaycastHit2D hit = Physics2D.Raycast(pI.transform.position, Vector2.down, pI.stats.GrounderDistance * 2,
					~pI.stats.PlayerLayer);
				Physics2D.queriesHitTriggers = pI.cachedTriggerSetting;
				if (hit.collider != null) {
					pI.groundNormal = hit.normal;
					if (!Mathf.Approximately(pI.groundNormal.y, 1f)) {
						pI.speed.y = pI.speed.x * -pI.groundNormal.x / pI.groundNormal.y;
						if (pI.speed.x != 0) pI.speed.y += pI.stats.GroundingForce;
					}
				}
				else {
					pI.groundNormal = Vector2.zero;
				}

				return;
			}

			float fallSpeed = pI.stats.FallAcceleration;
			if (pI.endedJumpEarly && pI.speed.y > 0) fallSpeed *= pI.stats.JumpEndEarlyGravityModifier;
			pI.speed.y = Mathf.MoveTowards(pI.speed.y, -pI.stats.MaxFallSpeed, fallSpeed * Time.fixedDeltaTime);
		}
	}
}