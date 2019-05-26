using System.IO;
using UnityEngine;

public class Trees : Plants {
	[Header("Trees")]
	public TreePlant treePrefab;

	[Header("Debugging")]
	public KeyCode exportKey = KeyCode.X;
	public string exportDirectory = "";


	protected override void Update() {
		base.Update();

		if(debug) {
			if(Input.GetKeyDown(exportKey)) {
				foreach(Plant plant in getPlants()) {
					TreePlant tree = plant as TreePlant;

					tree.getSatellite().export(Path.Combine(exportDirectory, tree.name + ".png"));
				}
			}
		}
	}


	protected override Plant createPlant(Vector3 position, Quaternion rotation, Transform parent) {
		Plant plant = Instantiate(treePrefab, position, rotation, parent);

		plant.name = "Tree";

		return plant;
	}

	protected override PlantSystem createSystem() {
		TreeSystem system = new TreeSystem();

		system.addVariable("T");
		system.addVariable("R");
		system.addVariable("K");
		system.addVariable("B");
		system.addVariable("L");
		system.addVariable("[");
		system.addVariable("]");

		system.setAxiom("T");

		// Start off with only identity production rules.

		system.setName("Tree");

		return system;
	}


	protected override PlantSystem[] evolveSystems(PlantSystem[] systems, int evolution) {
		return systems;
	}
}
