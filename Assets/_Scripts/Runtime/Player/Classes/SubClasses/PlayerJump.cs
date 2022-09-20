namespace ProdiG.Classes.SubClasses {
	public static class PlayerJump {
		public static void HandleJump(ref PlayerInternals pI) {
			if (pI.jumpToConsume || pI.HasBufferedJump) {
				if (pI.grounded || pI.CanUseCoyote) NormalJump(ref pI);
				else if (pI.jumpToConsume && pI.CanDoubleJump) DoubleJump(ref pI);
			}

			pI.jumpToConsume = false;

			if (!pI.endedJumpEarly && !pI.grounded && !pI.playerInputs.jumpHeld && pI.rb.velocity.y > 0)
				pI.endedJumpEarly = true;
		}

		private static void NormalJump(ref PlayerInternals pI) {
			pI.endedJumpEarly = false;
			pI.bufferedJumpUsable = false;
			pI.coyoteUsable = false;
			pI.doubleJumpUsable = true;
			pI.speed.y = pI.stats.JumpPower;
			pI.playerEvents._jumpedDelegate();
		}

		private static void DoubleJump(ref PlayerInternals pI) {
			pI.endedJumpEarly = false;
			pI.doubleJumpUsable = false;
			pI.speed.y = pI.stats.JumpPower;
			pI.playerEvents._doubleJumpedDelegate();
		}
	}
}