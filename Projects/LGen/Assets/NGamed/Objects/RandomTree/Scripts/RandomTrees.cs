using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class RandomTrees : Trees {
	[Header("Random")]
	public int minimumLength = 1;
	public int maximumLength = 1;
	
	[Space]
	public float defaultProductionWeight = 1.0f;
	public ProductionWeight[] productionWeights = new ProductionWeight[0];

	[Serializable]
	public struct ProductionWeight {
		public string variable;
	
		public float weight;
	}


	protected override Plant createPlant(Vector3 position, Quaternion rotation, Transform parent) {
		Plant plant = base.createPlant(position, rotation, parent);

		plant.name = "Random Tree";

		return plant;
	}

	protected override PlantSystem createSystem() {
		PlantSystem system = base.createSystem();

		string[] variables = system.getVariables();

		Dictionary<string, float> weights = new Dictionary<string, float>(variables.Length);

		foreach(string variable in variables) {
			weights.Add(variable, defaultProductionWeight);
		}

		foreach(ProductionWeight productionWeight in productionWeights) {
			weights[productionWeight.variable] = productionWeight.weight;
		}

		float totalWeight = 0.0f;

		foreach(string variable in variables) {
			totalWeight += weights[variable];
		}

		foreach(string variable in variables) {
			weights[variable] = weights[variable] / totalWeight;
		}

		for(int index = 1; index < variables.Length; index += 1) {
			string previous = variables[index - 1];
			string variable = variables[index];

			weights[variable] = weights[previous] + weights[variable];
		}

		foreach(string inputVariable in variables) {
			int length = UnityEngine.Random.Range(minimumLength, maximumLength + 1);

			StringBuilder builder = new StringBuilder(length);

			for(int index = 0; index < length; index += 1) {
				float value = UnityEngine.Random.value;

				string outputVariable = variables[variables.Length - 1];

				foreach(string variable in variables) {
					if(value < weights[variable]) {
						outputVariable = variable;

						break;
					}
				}

				builder.Append(outputVariable);
			}

			system.setProduction(inputVariable, builder.ToString());
		}

		system.setName("Random Tree");

		return system;
	}


	protected override PlantSystem[] evolveSystems(PlantSystem[] systems, int evolution) {
		PlantSystem[] evolvedSystems = base.evolveSystems(systems, evolution);

		for(int index = 0; index < systems.Length; index += 1) {
			evolvedSystems[index] = createSystem();
		}

		return evolvedSystems;
	}
}
