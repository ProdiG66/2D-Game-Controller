using System;
using DG.Tweening;
using ProdiG.Classes.SubClasses;
using ProdiG.Components;
using ProdiG.Interfaces;
using ProdiG.SubComponents;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ProdiG.Classes {
	[Serializable]
	public class PlayerAnimator : IMonoBehaviourEvents {
		private Animator _animator;
		private SpriteRenderer _renderer;

		[SerializeField]
		private AudioSource _source;

		private Camera _cam;

		[SerializeField]
		private GhostTrail _ghostTrail;

		[SerializeField]
		private BoomEffect _boomEffect;

		public GameObject gameObject { get; set; }
		public Transform transform { get; set; }
		public PlayerController playerController { get; set; }

		public int stepIndex {
			get => _stepIndex;
			set => _stepIndex = value;
		}

		public AudioClip[] footstepClips {
			get => _footstepClips;
			set => _footstepClips = value;
		}

		public void Awake() {
			_playerAnimator = this;
			animator = gameObject.GetComponentInChildren<Animator>();
			_renderer = gameObject.GetComponentInChildren<SpriteRenderer>();
			gameObject.GetComponentInChildren<PlayerAnimationEvents>().playerAnimator = this;
			_cam = Camera.main;
		}

		public void Start() {
			playerController.playerEvents.GroundedChanged += OnGroundedChanged;
			playerController.playerEvents.DashingChanged += OnDashingChanged;
			playerController.playerEvents.Jumped += OnJumped;
			playerController.playerEvents.DoubleJumped += OnDoubleJumped;
			playerController.playerEvents.Attacked += OnAttacked;
		}

		public void Update() {
			HandleSpriteFlipping();
			PlayerEffects.HandleGroundEffects(_playerAnimator);
			PlayerEffects.SetParticleColor(Vector2.down, moveParticles, _playerAnimator);
			PlayerAnimation.HandleAnimations(_playerAnimator);
		}

		private void HandleSpriteFlipping() {
			if (Mathf.Abs(playerController.Input.x) > 0.1f) _renderer.flipX = playerController.Input.x < 0;
		}

		#region Serialized Fields

		[SerializeField]
		private ParticleSystem _moveParticles;

		[SerializeField]
		private float _tiltChangeSpeed = .05f;

		[SerializeField]
		private AudioClip[] _footstepClips;

		[SerializeField]
		private AudioClip _dashClip;

		[SerializeField]
		private ParticleSystem _dashParticles,
								_dashRingParticles;

		[SerializeField]
		private Transform _dashRingTransform;

		[SerializeField]
		private float _minImpactForce = 20;

		[SerializeField]
		private float _landAnimDuration = 0.1f;

		[SerializeField]
		private AudioClip _landClip,
						_jumpClip,
						_doubleJumpClip;

		[SerializeField]
		private ParticleSystem _jumpParticles,
								_launchParticles,
								_doubleJumpParticles,
								_landParticles;

		[SerializeField]
		private Transform _jumpParticlesParent;

		[SerializeField]
		private float _attackAnimTime = 0.25f;

		[SerializeField]
		private AudioClip _attackClip;

		#endregion

		#region Internal

		private PlayerAnimator _playerAnimator;
		private ParticleSystem.MinMaxGradient _currentGradient;

		[HideInInspector]
		public Vector2 tiltVelocity;

		private int _stepIndex = 0;

		private bool _jumpTriggered;
		private bool _landed;
		private bool _grounded;
		private bool _attacked;
		private float _lockedTill;
		private readonly RaycastHit2D[] _groundHits = new RaycastHit2D[2];

		#endregion

		#region Cached Properties

		public int currentState { get; set; }

		public Animator animator {
			get => _animator;
			set => _animator = value;
		}

		public float lockedTill {
			get => _lockedTill;
			set => _lockedTill = value;
		}

		public bool attacked {
			get => _attacked;
			set => _attacked = value;
		}

		public bool grounded {
			get => _grounded;
			set => _grounded = value;
		}

		public bool landed {
			get => _landed;
			set => _landed = value;
		}

		public bool jumpTriggered {
			get => _jumpTriggered;
			set => _jumpTriggered = value;
		}

		public float attackAnimTime {
			get => _attackAnimTime;
			set => _attackAnimTime = value;
		}

		public float landAnimDuration {
			get => _landAnimDuration;
			set => _landAnimDuration = value;
		}

		public float tiltChangeSpeed {
			get => _tiltChangeSpeed;
			set => _tiltChangeSpeed = value;
		}

		public ParticleSystem moveParticles {
			get => _moveParticles;
			set => _moveParticles = value;
		}

		public ParticleSystem.MinMaxGradient currentGradient {
			get => _currentGradient;
			set => _currentGradient = value;
		}

		public RaycastHit2D[] groundHits => _groundHits;

		#endregion

		#region PlayerActions

		private void OnGroundedChanged(bool grounded, float impactForce) {
			this.grounded = grounded;

			if (impactForce >= _minImpactForce) {
				float p = Mathf.InverseLerp(0, _minImpactForce, impactForce);
				landed = true;
				_landParticles.transform.localScale = p * Vector3.one;
				_landParticles.Play();
				PlayerEffects.SetColor(_landParticles, _playerAnimator);
				PlaySound(_landClip, p * 0.1f);
			}

			if (this.grounded) moveParticles.Play();
			else moveParticles.Stop();
		}

		private void OnDashingChanged(bool dashing, Vector2 dir) {
			if (dashing) {
				_dashRingTransform.up = dir;
				_dashRingParticles.Play();
				_dashParticles.Play();
				PlaySound(_dashClip, 0.1f);
				_cam.transform.DOComplete();
				_cam.transform.DOShakePosition(.2f, .5f, 14, 90, false, true);
				_ghostTrail.ShowGhost();
				_boomEffect.ripplemat.SetFloat("TimeStep", 0);
				Vector3 playerScreenPos = playerController.transform.position;
				playerScreenPos.y += 2.5f;
				_boomEffect.Boom(playerScreenPos, 1, -0.5f, 0.11f);
			}
			else {
				_dashParticles.Stop();
			}
		}

		private void OnJumped() {
			jumpTriggered = true;
			PlaySound(_jumpClip, 0.05f, Random.Range(0.98f, 1.02f));

			_jumpParticlesParent.localRotation = Quaternion.Euler(0, 0, 60f);

			PlayerEffects.SetColor(_jumpParticles, _playerAnimator);
			PlayerEffects.SetColor(_launchParticles, _playerAnimator);
			_jumpParticles.Play();
		}

		private void OnDoubleJumped() {
			PlaySound(_doubleJumpClip, 0.1f);
			_doubleJumpParticles.Play();
		}

		private void OnAttacked() {
			attacked = true;
			PlaySound(_attackClip, 0.1f, Random.Range(0.97f, 1.03f));
		}

		#endregion

		#region Audio

		public void PlaySound(AudioClip clip, float volume = 1, float pitch = 1) {
			_source.pitch = pitch;
			_source.PlayOneShot(clip, volume);
		}

		#endregion
	}
}