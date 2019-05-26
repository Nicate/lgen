using System.Collections.Generic;
using UnityEngine;

public abstract class Plant : MonoBehaviour {
	private PlantSystem system;
	
	private Parent parent;
	private Stack<Parent> parents;

	private int branch {
		get => parents.Count - 1;
	}

	private struct Parent {
		public Transform transform;
		public int depth;

		public Parent(Transform transform, int depth) {
			this.transform = transform;
			this.depth = depth;
		}
	}


	protected virtual void Awake() {
		parents = new Stack<Parent>();
	}


	protected PlantSystem getSystem() {
		// Note that the system is only available during interpretation or evaluation.
		return system;
	}


	public void interpret(PlantSystem system) {
		this.system = system;

		reset();
		build();
		complete();
	}


	protected virtual void reset() {
		foreach(Transform child in transform) {
			if(child.GetComponents<Limb>().Length > 0) {
				Destroy(child.gameObject);
			}
		}
		
		parent = new Parent(transform, 0);

		parents.Clear();
		parents.Push(parent);
	}

	protected virtual void build() {
		foreach(string variable in system) {
			interpret(variable);
		}
	}

	protected virtual void complete() {
		// No default implementation.
	}


	protected Limb growLimb(Limb limbPrefab) {
		Limb limb = Instantiate(limbPrefab, parent.transform);

		limb.name = limbPrefab.name + " [" + parent.depth + "]";

		bool isParent = limb.grow(parent.depth, branch);

		if(isParent) {
			parent = new Parent(limb.transform, parent.depth + 1);
		}

		return limb;
	}


	protected void startBranch() {
		parents.Push(parent);
	}

	protected void stopBranch() {
		if(branch >= 0) {
			parent = parents.Pop();
		}
	}


	protected abstract void interpret(string variable);

	public abstract void evaluate();
}
