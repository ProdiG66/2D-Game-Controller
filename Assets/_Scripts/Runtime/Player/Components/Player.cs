using ProdiG.Classes;
using ProdiG.Classes.SubClasses;
using ProdiG.SubComponents;
using UnityEngine;

namespace ProdiG.Components {
	[RequireComponent(
		typeof(Rigidbody2D),
		typeof(CapsuleCollider2D)
	)]
	public class Player : PlayerController {
		[SerializeField]
		private PlayerAnimator _animator;

		protected override void Awake() {
			base.Awake();
			_animator.gameObject = gameObject;
			_animator.transform = transform;
			_animator.playerController = this;
			_animator.Awake();
		}

		private void Start() {
			_animator.Start();
		}

		protected override void Update() {
			base.Update();
			_animator.Update();
		}
	}
}