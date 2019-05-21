using UnityEngine;

public class Limb : MonoBehaviour {
	protected int depth = 0;
	protected int branch = 0;


	public int getDepth() {
		return depth;
	}

	public int getBranch() {
		return branch;
	}


	public virtual bool grow(int depth, int branch) {
		this.depth = depth;
		this.branch = branch;

		return false;
	}
}
