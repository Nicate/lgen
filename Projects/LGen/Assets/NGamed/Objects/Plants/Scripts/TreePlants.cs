using UnityEngine;

public class TreePlants : Plants {
	public TreePlant treePlantPrefab;


	protected override Plant growPlant(Vector3 position, Quaternion rotation, Transform parent) {
		Plant plant = createPlant(position, rotation, parent);
		LSystem system = createSystem();

		system.generateSequence(generations);

		plant.interpret(system);

		return plant;
	}


	private Plant createPlant(Vector3 position, Quaternion rotation, Transform parent) {
		return Instantiate(treePlantPrefab, position, rotation, parent);
	}

	private LSystem createSystem() {
		LSystem system = new LSystem();

		system.addVariable("A");
		system.addVariable("B");

		system.setAxiom("A");

		system.setProduction("A", "AB");
		system.setProduction("B", "A");

		return system;
	}
}
