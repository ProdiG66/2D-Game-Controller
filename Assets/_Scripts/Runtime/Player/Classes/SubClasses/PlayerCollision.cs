using ProdiG.SubComponents;
using UnityEngine;

namespace ProdiG.Classes.SubClasses {
	public static class PlayerCollision {
		public static void HandleCollisions(ref PlayerInternals pI) {
			if (pI.speed.y > 0 && pI.ceilingHitCount > 0) pI.speed.y = 0;
			if (!pI.grounded && pI.groundHitCount > 0) {
				pI.grounded = true;
				pI.ResetDash(pI);
				pI.ResetJump(pI);
				pI.playerEvents._groundedChangedDelegate(true, Mathf.Abs(pI.speed.y));
			}
			else if (pI.grounded && pI.groundHitCount == 0) {
				pI.grounded = false;
				pI.frameLeftGrounded = pI.fixedFrame;
				pI.playerEvents._groundedChangedDelegate(false, 0);
			}
		}
	}
}