using UnityEngine;

public class Twist : Part {
	public override bool grow(int depth, int branch) {
		base.grow(depth, branch);

		transform.localRotation = Quaternion.Euler(0.0f, 120.0f, 0.0f);

		return true;
	}
}
