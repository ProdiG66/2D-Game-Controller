using ProdiG.Enums;
using ProdiG.Interfaces;
using UnityEngine;

namespace ProdiG.Components {
	//WIP
	public class Explosion : MonoBehaviour {
		[SerializeField]
		private float _growSpeed = 1;

		[SerializeField]
		private float _growSize = 1;

		[SerializeField]
		private float _explosionForce = 50;

		private void Update() {
			float scale = (Mathf.Sin(Time.time * _growSpeed) + 2) * _growSize;
			transform.localScale = new Vector3(scale, scale, scale);
		}

		private void OnTriggerEnter2D(Collider2D other) {
			if (other.TryGetComponent(out IPlayerController controller)) {
				Vector2 dir = (Vector2)other.transform.position + other.offset - (Vector2)transform.position;
				controller.ApplyVelocity(dir.normalized * _explosionForce, PlayerForce.Decay);
			}
		}
	}
}