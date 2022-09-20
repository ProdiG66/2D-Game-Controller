using ProdiG.Enums;
using ProdiG.Interfaces;
using UnityEngine;

namespace ProdiG.Components {
	//WIP
	public class Bouncer : MonoBehaviour {
		[SerializeField]
		private float _bounceForce = 70;

		private void OnCollisionStay2D(Collision2D other) {
			if (other.collider.TryGetComponent(out IPlayerController controller))
				controller.ApplyVelocity(transform.up * _bounceForce, PlayerForce.Burst);
		}
	}
}