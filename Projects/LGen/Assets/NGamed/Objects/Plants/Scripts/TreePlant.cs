using System.Collections.Generic;
using UnityEngine;

using Parent = System.Tuple<UnityEngine.Transform, int>;

public class TreePlant : Plant {
	public Part stemPrefab;
	public Part twistPrefab;
	public Part knotPrefab;
	public Part budPrefab;
	public Part leafPrefab;


	private Parent parent;

	private Stack<Parent> parents;

	private int branch {
		get => parents.Count - 1;
	}


	private void Awake() {
		parents = new Stack<Parent>();
	}


	protected override void reset() {
		base.reset();
		
		parent = new Parent(transform, 0);

		parents.Clear();
		parents.Push(parent);
	}


	protected override void interpret(string variable) {
		if(variable == "S") {
			growPart(stemPrefab);
		}
		else if(variable == "T") {
			growPart(twistPrefab);
		}
		else if(variable == "K") {
			growPart(knotPrefab);
		}
		else if(variable == "B") {
			growPart(budPrefab);
		}
		else if(variable == "L") {
			growPart(leafPrefab);
		}
		else if(variable == "[") {
			parents.Push(parent);
		}
		else if(variable == "]") {
			if(branch >= 0) {
				parent = parents.Pop();
			}
		}
	}


	private Part growPart(Part partPrefab) {
		Part part = Instantiate(partPrefab, parent.Item1);

		part.name = partPrefab.name + " [" + parent.Item2 + "]";

		bool isParent = part.grow(parent.Item2, branch);

		if(isParent) {
			parent = new Parent(part.transform, parent.Item2 + 1);
		}

		return part;
	}
}
