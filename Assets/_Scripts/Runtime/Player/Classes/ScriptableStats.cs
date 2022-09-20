using UnityEngine;

namespace ProdiG.Classes {
	[CreateAssetMenu(fileName = "ScriptableStat", menuName = "ProdiG/Scriptable Stat", order = 0)]
	public class ScriptableStats : ScriptableObject {
		[Header("MOVEMENT")]
		public float MaxSpeed = 14;

		public float Acceleration = 120;
		public float GroundDeceleration = 60;
		public float AirDeceleration = 30;

		[Range(0f, -10f)]
		public float GroundingForce = -1.5f;

		[Header("CROUCHING")]
		public float CrouchInputThreshold = -0.5f;

		[Range(0f, 1f)]
		public float CrouchSpeedPenalty = 0.5f;

		public int CrouchSlowdownFrames = 50;
		public float CrouchBufferCheck = 0.1f;

		[Header("JUMP")]
		public bool AllowDoubleJump = true;

		public float JumpPower = 36;
		public float MaxFallSpeed = 40;
		public float FallAcceleration = 110;
		public float JumpEndEarlyGravityModifier = 3;
		public int CoyoteFrames = 7;
		public int JumpBufferFrames = 7;

		[Header("DASH")]
		public bool AllowDash = true;

		public float DashVelocity = 50;
		public int DashDurationFrames = 5;
		public float DashEndHorizontalMultiplier = 0.25f;

		[Header("ATTACKS")]
		public bool AllowAttacks = true;

		public int AttackFrameCooldown = 15;

		[Header("COLLISIONS")]
		public LayerMask PlayerLayer;

		public float GrounderDistance = 0.1f;
		public Vector2 WallDetectorSize = new(0.75f, 1f);

		[Header("EXTERNAL")]
		public int ExternalVelocityDecay = 100;
	}
}