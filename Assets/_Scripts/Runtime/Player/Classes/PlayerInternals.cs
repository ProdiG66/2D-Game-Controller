using System;
using UnityEngine;

namespace ProdiG.Classes {
	[Serializable]
	public class PlayerInternals {
		public void ResetJump(PlayerInternals pI) {
			pI.coyoteUsable = true;
			pI.bufferedJumpUsable = true;
			pI.doubleJumpUsable = false;
			pI.endedJumpEarly = false;
		}

		public void ResetDash(PlayerInternals pI) {
			pI.canDash = true;
		}

		public bool Crouching { get; set; }
		public bool CrouchPressed => playerInputs.move.y <= stats.CrouchInputThreshold;
		public bool CanUseCoyote => coyoteUsable && !grounded && fixedFrame < frameLeftGrounded + stats.CoyoteFrames;
		public bool HasBufferedJump => bufferedJumpUsable && fixedFrame < frameJumpWasPressed + stats.JumpBufferFrames;
		public bool CanDoubleJump => doubleJumpUsable && stats.AllowDoubleJump;

		public void TakeAwayControl(bool resetVelocity = true) {
			if (resetVelocity) rb.velocity = Vector2.zero;
			hasControl = false;
		}

		public void ReturnControl() {
			speed = Vector2.zero;
			hasControl = true;
		}

		public Transform transform;
		public ScriptableStats stats { get; set; }
		public PlayerEvents playerEvents { get; set; }
		private Rigidbody2D _rb;
		private PlayerInputs _playerInputs;
		private CapsuleCollider2D[] _cols;
		private CapsuleCollider2D _col;

		public Bounds standingColliderBounds =
			new(new Vector3(0, 0.75f),
				Vector3.one);

		private bool _cachedTriggerSetting;

		public Vector2 speed;
		private Vector2 _currentExternalVelocity;
		private int _fixedFrame;
		private bool _hasControl = true;

		private RaycastHit2D[] _groundHits = new RaycastHit2D[2];
		private RaycastHit2D[] _ceilingHits = new RaycastHit2D[2];
		private int _groundHitCount;
		private int _ceilingHitCount;
		private int _frameLeftGrounded = int.MinValue;
		private bool _grounded;

		private int _frameLeftLadder = int.MinValue;
		private readonly Collider2D[] _crouchHits = new Collider2D[5];
		private int _frameStartedCrouching;

		private bool _jumpToConsume;
		private bool _endedJumpEarly;
		private bool _coyoteUsable;
		private bool _doubleJumpUsable;
		private bool _bufferedJumpUsable;
		private int _frameJumpWasPressed = int.MinValue;

		private bool _canDash;
		private Vector2 _dashVel;
		private bool _dashing;
		private int _startedDashing;

		private bool _attackToConsume;
		private int _frameLastAttacked = int.MinValue;
		private Vector2 _groundNormal;

		private float _currentWallJumpMoveMultiplier = 1f;
		private bool _dashToConsume;

		public Rigidbody2D rb {
			get => _rb;
			set => _rb = value;
		}

		public PlayerInputs playerInputs {
			get => _playerInputs;
			set => _playerInputs = value;
		}

		public CapsuleCollider2D[] cols {
			get => _cols;
			set => _cols = value;
		}

		public CapsuleCollider2D col {
			get => _col;
			set => _col = value;
		}

		public bool cachedTriggerSetting {
			get => _cachedTriggerSetting;
			set => _cachedTriggerSetting = value;
		}

		public Vector2 currentExternalVelocity {
			get => _currentExternalVelocity;
			set => _currentExternalVelocity = value;
		}

		public int fixedFrame {
			get => _fixedFrame;
			set => _fixedFrame = value;
		}

		public bool hasControl {
			get => _hasControl;
			set => _hasControl = value;
		}

		public RaycastHit2D[] groundHits => _groundHits;

		public RaycastHit2D[] ceilingHits => _ceilingHits;

		public int groundHitCount {
			get => _groundHitCount;
			set => _groundHitCount = value;
		}

		public int ceilingHitCount {
			get => _ceilingHitCount;
			set => _ceilingHitCount = value;
		}

		public int frameLeftGrounded {
			get => _frameLeftGrounded;
			set => _frameLeftGrounded = value;
		}

		public bool grounded {
			get => _grounded;
			set => _grounded = value;
		}

		public int frameLeftLadder {
			get => _frameLeftLadder;
			set => _frameLeftLadder = value;
		}

		public Collider2D[] crouchHits => _crouchHits;

		public int frameStartedCrouching {
			get => _frameStartedCrouching;
			set => _frameStartedCrouching = value;
		}

		public bool jumpToConsume {
			get => _jumpToConsume;
			set => _jumpToConsume = value;
		}

		public bool endedJumpEarly {
			get => _endedJumpEarly;
			set => _endedJumpEarly = value;
		}

		public bool coyoteUsable {
			get => _coyoteUsable;
			set => _coyoteUsable = value;
		}

		public bool doubleJumpUsable {
			get => _doubleJumpUsable;
			set => _doubleJumpUsable = value;
		}

		public bool bufferedJumpUsable {
			get => _bufferedJumpUsable;
			set => _bufferedJumpUsable = value;
		}

		public int frameJumpWasPressed {
			get => _frameJumpWasPressed;
			set => _frameJumpWasPressed = value;
		}

		public bool canDash {
			get => _canDash;
			set => _canDash = value;
		}

		public Vector2 dashVel {
			get => _dashVel;
			set => _dashVel = value;
		}

		public bool dashing {
			get => _dashing;
			set => _dashing = value;
		}

		public int startedDashing {
			get => _startedDashing;
			set => _startedDashing = value;
		}

		public bool attackToConsume {
			get => _attackToConsume;
			set => _attackToConsume = value;
		}

		public int frameLastAttacked {
			get => _frameLastAttacked;
			set => _frameLastAttacked = value;
		}

		public Vector2 groundNormal {
			get => _groundNormal;
			set => _groundNormal = value;
		}

		public float currentWallJumpMoveMultiplier {
			get => _currentWallJumpMoveMultiplier;
			set => _currentWallJumpMoveMultiplier = value;
		}

		public bool dashToConsume {
			get => _dashToConsume;
			set => _dashToConsume = value;
		}
	}
}