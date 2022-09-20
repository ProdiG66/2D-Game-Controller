using ProdiG.SubComponents;

namespace ProdiG.Classes.SubClasses {
	public static class PlayerAttack {
		public static void HandleAttacking(PlayerInternals pI) {
			if (!pI.attackToConsume) return;

			if (pI.fixedFrame > pI.frameLastAttacked + pI.stats.AttackFrameCooldown) {
				pI.frameLastAttacked = pI.fixedFrame;
				pI.playerEvents._attackedDelegate();
			}

			pI.attackToConsume = false;
		}
	}
}