using UnityEngine;

public class Stem : Part {
	public Part basePart;
	public Part capPart;

	public float length = 1.0f;
	public float radius = 1.0f;

	public float attenuation = 1.0f;
	public float depthiness = 1.0f;
	public float branchiness = 1.0f;

	public float minimumRadius = 1.0f;


	public override bool grow(int depth, int branch) {
		base.grow(depth, branch);

		float stemLength = length;
		float stemRadius = radius * Mathf.Pow(attenuation, -(depthiness * depth + branchiness * branch));

		if(stemRadius < minimumRadius) {
			stemRadius = minimumRadius;
		}

		transform.localPosition = new Vector3(0.0f, length, 0.0f);

		basePart.transform.localScale = new Vector3(stemRadius, stemLength, stemRadius);
		capPart.transform.localScale = new Vector3(stemRadius, stemRadius, stemRadius);

		return true;
	}
}
