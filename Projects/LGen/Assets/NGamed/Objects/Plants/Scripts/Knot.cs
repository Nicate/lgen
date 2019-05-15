using UnityEngine;

public class Knot : Part {
	public override bool grow(int depth, int branch) {
		base.grow(depth, branch);

		transform.localRotation = Quaternion.Euler(60.0f, 0.0f, 0.0f);

		return true;
	}
}
