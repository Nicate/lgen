using UnityEngine;

public class Trunk : Limb {
	public GameObject baseReference;
	public GameObject capReference;

	public float length = 1.0f;
	public float radius = 1.0f;

	public float attenuation = 1.0f;
	public float depthiness = 1.0f;
	public float branchiness = 1.0f;

	public float minimumRadius = 1.0f;


	public override bool grow(int depth, int branch) {
		base.grow(depth, branch);

		float trunkLength = length;
		float trunkRadius = radius * Mathf.Pow(attenuation, -(depthiness * depth + branchiness * branch));

		if(trunkRadius < minimumRadius) {
			trunkRadius = minimumRadius;
		}

		transform.localPosition = new Vector3(0.0f, length, 0.0f);

		baseReference.transform.localScale = new Vector3(trunkRadius, trunkLength, trunkRadius);
		capReference.transform.localScale = new Vector3(trunkRadius, trunkRadius, trunkRadius);

		return true;
	}
}
