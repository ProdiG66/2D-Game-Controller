using System;
using UnityEngine;

namespace ProdiG.Structs {
	//Prototype Input
	[Serializable]
	public struct JoystickInput {
		public bool isPC;
		public float x;
		public float y;
		private Vector2 joystick => new(x, y);
		public Vector2 normalized => joystick.normalized;
		public float magnitude => joystick.magnitude;
		public bool isUsing => x != 0 || y != 0;

		public void Reset() {
			x = y = 0;
		}
	}
}