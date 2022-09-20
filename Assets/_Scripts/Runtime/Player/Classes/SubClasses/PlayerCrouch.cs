using ProdiG.SubComponents;
using UnityEngine;

namespace ProdiG.Classes.SubClasses {
	public static class PlayerCrouch {
		public static void HandleCrouching(PlayerInternals pI) {
			if (pI.Crouching != pI.CrouchPressed) SetCrouching(!pI.Crouching, pI);
		}

		public static void SetCrouching(bool active, PlayerInternals pI) {
			if (pI.Crouching && !CanStandUp(pI)) return;

			pI.Crouching = active;
			pI.col = pI.cols[active ? 1 : 0];
			pI.cols[0].enabled = !active;
			pI.cols[1].enabled = active;

			if (pI.Crouching) pI.frameStartedCrouching = pI.fixedFrame;
		}

		public static bool CanStandUp(PlayerInternals pI) {
			Vector2 pos = pI.rb.position + (Vector2)pI.standingColliderBounds.center +
						new Vector2(0, pI.standingColliderBounds.extents.y);
			Vector2 size = new(pI.standingColliderBounds.size.x, pI.stats.CrouchBufferCheck);

			Physics2D.queriesHitTriggers = false;
			int hits = Physics2D.OverlapBoxNonAlloc(pos, size, 0, pI.crouchHits, ~pI.stats.PlayerLayer);
			Physics2D.queriesHitTriggers = pI.cachedTriggerSetting;

			return hits == 0;
		}
	}
}