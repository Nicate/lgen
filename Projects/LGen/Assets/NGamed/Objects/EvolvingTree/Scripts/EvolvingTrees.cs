using System.Text;
using UnityEngine;

public class EvolvingTrees : Trees {
	[Header("Evolution")]
	public int length = 1;


	protected override Plant createPlant(Vector3 position, Quaternion rotation, Transform parent) {
		Plant plant = base.createPlant(position, rotation, parent);

		plant.name = "Evolving Tree";

		return plant;
	}

	protected override PlantSystem createSystem() {
		PlantSystem system = base.createSystem();

		string[] variables = system.getVariables();

		foreach(string inputVariable in variables) {
			StringBuilder builder = new StringBuilder(length);

			for(int index = 0; index < length; index += 1) {
				int selectedIndex = Random.Range(0, variables.Length);

				string outputVariable = variables[selectedIndex];

				builder.Append(outputVariable);
			}

			system.setProduction(inputVariable, builder.ToString());
		}

		system.setName("Evolving Tree");

		return system;
	}


	protected override PlantSystem[] evolveSystems(PlantSystem[] systems, int evolution) {
		// TODO Implement the actual evolution.
		return systems;
	}
}
