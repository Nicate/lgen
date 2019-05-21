using UnityEngine;

public class Ring : Limb {
	public float twist = 0.0f;


	public override bool grow(int depth, int branch) {
		base.grow(depth, branch);

		transform.localRotation = Quaternion.Euler(0.0f, twist, 0.0f);

		return true;
	}
}
