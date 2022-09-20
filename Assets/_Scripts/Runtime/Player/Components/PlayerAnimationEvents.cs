using System.Collections;
using System.Collections.Generic;
using ProdiG.Classes;
using UnityEngine;

public class PlayerAnimationEvents : MonoBehaviour {
	public PlayerAnimator playerAnimator { get; set; }

	public void PlayFootstep() {
		playerAnimator.stepIndex = (playerAnimator.stepIndex + 1) % playerAnimator.footstepClips.Length;
		playerAnimator.PlaySound(playerAnimator.footstepClips[playerAnimator.stepIndex], 0.01f);
	}
}