using ProdiG.SubComponents;
using UnityEngine;

namespace ProdiG.Components {
	public class CameraFollow : MonoBehaviour {
		[SerializeField]
		private Transform _player;

		[SerializeField]
		private float _smoothTime = 0.5f;

		[SerializeField]
		private float _minX,
					_maxX;

		private float _yLock;
		private Vector3 _currentVel;

		private void Start() {
			if (_player == null) {
				PlayerController player = FindObjectOfType<PlayerController>();
				if (player != null) _player = player.transform;
			}

			_yLock = transform.position.y;
		}


		private void Update() {
			if (!_player) return;

			Vector3 target = new(Mathf.Clamp(_player.position.x, _minX, _maxX), _yLock, -10);
			transform.position = Vector3.SmoothDamp(transform.position, target, ref _currentVel, _smoothTime);
		}
	}
}