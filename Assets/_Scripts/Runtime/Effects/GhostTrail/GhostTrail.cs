using UnityEngine;
using DG.Tweening;

namespace ProdiG.Components {
	public class GhostTrail : MonoBehaviour {
		[SerializeField]
		private Transform _ghostsParent;

		[SerializeField]
		private Color _trailColor;

		[SerializeField]
		private Color _fadeColor;

		[SerializeField]
		private float _ghostInterval;

		[SerializeField]
		private float _fadeTime;

		[SerializeField]
		private Transform _player;

		[SerializeField]
		private SpriteRenderer _playerRenderer;

		public void ShowGhost() {
			Sequence s = DOTween.Sequence();
			for (int i = 0; i < _ghostsParent.childCount; i++) {
				Transform currentGhost = _ghostsParent.GetChild(i);
				SpriteRenderer spriteRenderer = currentGhost.GetComponent<SpriteRenderer>();
				s.AppendCallback(() => currentGhost.position = _player.position);
				s.AppendCallback(() => spriteRenderer.flipX = _playerRenderer.flipX);
				s.AppendCallback(() => spriteRenderer.sprite = _playerRenderer.sprite);
				s.Append(spriteRenderer.material.DOColor(_trailColor, 0));
				s.AppendCallback(() => FadeSprite(currentGhost));
				s.AppendInterval(_ghostInterval);
			}
		}

		public void FadeSprite(Transform current) {
			SpriteRenderer spriteRenderer = current.GetComponent<SpriteRenderer>();
			spriteRenderer.material.DOKill();
			spriteRenderer.material.DOColor(_fadeColor, _fadeTime);
		}
	}
}