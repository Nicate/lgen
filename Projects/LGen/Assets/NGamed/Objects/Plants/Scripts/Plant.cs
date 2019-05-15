using UnityEngine;

public abstract class Plant : MonoBehaviour {
	protected LSystem system;


	public virtual void interpret(LSystem system) {
		this.system = system;

		reset();
		build();
		complete();
	}


	protected virtual void reset() {
		foreach(Transform child in transform) {
			if(child.GetComponents<Part>().Length > 0) {
				Destroy(child.gameObject);
			}
		}
	}

	protected virtual void build() {
		foreach(string variable in system) {
			interpret(variable);
		}
	}

	protected virtual void complete() {
		// No default implementation.
	}


	protected abstract void interpret(string variable);
}
