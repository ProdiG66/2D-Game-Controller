using System;
using System.Collections;
using ProdiG.Classes;
using ProdiG.Classes.SubClasses;
using ProdiG.Enums;
using UnityEngine;

namespace ProdiG.SubComponents {
	[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
	public class PlayerController : MonoBehaviour {
		[SerializeField]
		protected ScriptableStats _stats;

		[SerializeField]
		protected PlayerInternals _playerInternals;

		#region External

		public PlayerEvents playerEvents => _playerInternals.playerEvents;
		public ScriptableStats PlayerStats => _playerInternals.stats;
		public bool Crouching => _playerInternals.Crouching;
		public Vector2 Input => _playerInternals.playerInputs.move;
		public Vector2 Speed => _playerInternals.speed;
		public Vector2 GroundNormal => _playerInternals.groundNormal;

		//WIP External Forces
		public virtual void ApplyVelocity(Vector2 vel, PlayerForce forceType) {
			if (forceType == PlayerForce.Burst) _playerInternals.speed += vel;
			else _playerInternals.currentExternalVelocity += vel;
		}

		#endregion

		protected virtual void Awake() {
			_playerInternals = new PlayerInternals();
			_playerInternals.playerEvents = new PlayerEvents();
			_playerInternals.playerInputs = new PlayerInputs();
			_playerInternals.playerInputs.Awake();
			_playerInternals.stats = _stats;
			_playerInternals.transform = transform;
			_playerInternals.rb = GetComponent<Rigidbody2D>();
			_playerInternals.cols = GetComponents<CapsuleCollider2D>();
			_playerInternals.col = _playerInternals.cols[0];
			_playerInternals.standingColliderBounds = _playerInternals.cols[0].bounds;
			_playerInternals.standingColliderBounds.center = _playerInternals.cols[0].offset;
			Physics2D.queriesStartInColliders = false;
			_playerInternals.cachedTriggerSetting = Physics2D.queriesHitTriggers;
			PlayerCrouch.SetCrouching(false, _playerInternals);
		}

		protected virtual void Update() {
			GatherInput();
		}

		protected virtual void FixedUpdate() {
			_playerInternals.fixedFrame++;
			CheckCollisions();
			PlayerCollision.HandleCollisions(ref _playerInternals);
			PlayerCrouch.HandleCrouching(_playerInternals);
			PlayerJump.HandleJump(ref _playerInternals);
			PlayerDash.HandleDash(_playerInternals);
			PlayerAttack.HandleAttacking(_playerInternals);
			PlayerHorizontal.HandleHorizontal(ref _playerInternals);
			PlayerVertical.HandleVertical(ref _playerInternals);
			ApplyVelocity();
		}

		#region Misc

		private void OnEnable() {
			_playerInternals.playerInputs.OnEnable();
		}

		private void OnDisable() {
			_playerInternals.playerInputs.OnDisable();
		}

		private void CheckCollisions() {
			Physics2D.queriesHitTriggers = false;
			Vector2 origin = (Vector2)transform.position + _playerInternals.col.offset;
			_playerInternals.groundHitCount = Physics2D.CapsuleCastNonAlloc(origin, _playerInternals.col.size,
				_playerInternals.col.direction, 0, Vector2.down,
				_playerInternals.groundHits, _playerInternals.stats.GrounderDistance,
				~_playerInternals.stats.PlayerLayer);
			_playerInternals.ceilingHitCount = Physics2D.CapsuleCastNonAlloc(origin, _playerInternals.col.size,
				_playerInternals.col.direction, 0, Vector2.up,
				_playerInternals.ceilingHits, _playerInternals.stats.GrounderDistance,
				~_playerInternals.stats.PlayerLayer);
		}

		private void GatherInput() {
			_playerInternals.playerInputs.Update();
			if (_playerInternals.playerInputs.jumpDown) {
				_playerInternals.jumpToConsume = true;
				_playerInternals.frameJumpWasPressed = _playerInternals.fixedFrame;
			}

			if (_playerInternals.playerInputs.dashDown && _playerInternals.stats.AllowDash)
				_playerInternals.dashToConsume = true;
			if (_playerInternals.playerInputs.attackDown && _playerInternals.stats.AllowAttacks)
				_playerInternals.attackToConsume = true;
		}

		private void ApplyVelocity() {
			if (!_playerInternals.hasControl) return;
			_playerInternals.rb.velocity = _playerInternals.speed + _playerInternals.currentExternalVelocity;
			_playerInternals.currentExternalVelocity = Vector2.MoveTowards(_playerInternals.currentExternalVelocity,
				Vector2.zero,
				_playerInternals.stats.ExternalVelocityDecay * Time.fixedDeltaTime);
		}

		#endregion

		#region Ledges

		#endregion
	}
}