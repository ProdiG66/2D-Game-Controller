using System;
using ProdiG.Classes;
using ProdiG.Enums;
using UnityEngine;

namespace ProdiG.Interfaces {
	public interface IPlayerController {
		public ScriptableStats PlayerStats { get; }
		public Vector2 Input { get; }
		public Vector2 Speed { get; }
		public Vector2 GroundNormal { get; }
		public bool Crouching { get; }
		public event Action<bool, float> GroundedChanged;
		public event Action<bool, Vector2> DashingChanged;
		public event Action<bool> Jumped;
		public event Action DoubleJumped;
		public event Action Attacked;
		public void ApplyVelocity(Vector2 vel, PlayerForce forceType);
	}
}