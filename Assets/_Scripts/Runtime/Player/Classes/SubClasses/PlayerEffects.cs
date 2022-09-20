using UnityEngine;

namespace ProdiG.Classes.SubClasses {
	public static class PlayerEffects {
		public static void HandleGroundEffects(PlayerAnimator pA) {
			float speedPoint = Mathf.InverseLerp(0, pA.playerController.PlayerStats.MaxSpeed,
				Mathf.Abs(pA.playerController.Speed.x));
			pA.moveParticles.transform.localScale = Vector3.MoveTowards(pA.moveParticles.transform.localScale,
				Vector3.one * speedPoint, 2 * Time.deltaTime);
			pA.transform.up = Vector2.SmoothDamp(pA.transform.up,
				pA.grounded ? pA.playerController.GroundNormal : Vector2.up,
				ref pA.tiltVelocity, pA.tiltChangeSpeed);
		}

		public static void SetParticleColor(Vector2 detectionDir, ParticleSystem system, PlayerAnimator pA) {
			int hitCount = Physics2D.RaycastNonAlloc(pA.transform.position, detectionDir, pA.groundHits, 2);
			for (int i = 0; i < hitCount; i++) {
				RaycastHit2D hit = pA.groundHits[i];
				if (!hit.collider || hit.collider.isTrigger ||
					!hit.transform.TryGetComponent(out SpriteRenderer r)) continue;
				Color color = r.color;
				pA.currentGradient = new ParticleSystem.MinMaxGradient(color * 0.9f, color * 1.2f);
				SetColor(system, pA);
				return;
			}
		}

		public static void SetColor(ParticleSystem ps, PlayerAnimator pA) {
			ParticleSystem.MainModule main = ps.main;
			main.startColor = pA.currentGradient;
		}
	}
}