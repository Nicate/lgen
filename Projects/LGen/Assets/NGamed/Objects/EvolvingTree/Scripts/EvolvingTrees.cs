using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class EvolvingTrees : Trees {
	[Header("Evolution")]
	public int length = 1;

	public float weightExponent = 1.0f;

	public float mutationRate = 0.0f;


	protected override Plant createPlant(Vector3 position, Quaternion rotation, Transform parent) {
		Plant plant = base.createPlant(position, rotation, parent);

		plant.name = "Evolving Tree";

		return plant;
	}

	protected override PlantSystem createSystem() {
		PlantSystem system = base.createSystem();

		string[] outputVariables = system.getVariables();

		List<string> inputVariables = new List<string>(outputVariables);
		inputVariables.Remove("[");
		inputVariables.Remove("]");

		foreach(string inputVariable in inputVariables) {
			StringBuilder builder = new StringBuilder(length);

			int branches = 0;

			for(int index = 0; index < length; index += 1) {
				int selectedIndex = Random.Range(0, outputVariables.Length);

				string outputVariable = outputVariables[selectedIndex];

				if(outputVariable == "[") {
					branches += 1;
				}
				else if(outputVariable == "]") {
					branches -= 1;

					if(branches < 0) {
						builder.Insert(0, "[");

						branches += 1;
					}
				}

				builder.Append(outputVariable);
			}

			while(branches > 0) {
				builder.Append("]");

				branches -= 1;
			}

			system.setProduction(inputVariable, builder.ToString());
		}

		system.setProduction("[", "[");
		system.setProduction("]", "]");

		system.setName("Evolving Tree System");

		return system;
	}


	protected override PlantSystem[] evolveSystems(PlantSystem[] systems, int evolution) {
		List<TreeSystem> treeSystems = castSystems(systems);
		List<float> weights = weighSystems(treeSystems);

		if(debug) {
			debugAverages(treeSystems, evolution);
		}

		for(int index = 0; index < systems.Length; index += 1) {
			int randomIndex = getWeightedRandomIndex(weights);

			TreeSystem randomTreeSystem = treeSystems[randomIndex];

			systems[index] = mutate(randomTreeSystem);
		}

		return systems;
	}

	
	protected List<TreeSystem> castSystems(PlantSystem[] systems) {
		List<TreeSystem> treeSystems = new List<TreeSystem>(systems.Length);

		foreach(PlantSystem system in systems) {
			if(system is TreeSystem) {
				treeSystems.Add(system as TreeSystem);
			}
		}

		return treeSystems;
	}

	protected List<float> weighSystems(List<TreeSystem> systems) {
		List<float> weights = new List<float>(systems.Count);

		float totalWeight = 0.0f;

		foreach(TreeSystem treeSystem in systems) {
			float fitness = getFitness(treeSystem);

			// The exponentiation will cause larger differences in weight.
			totalWeight += Mathf.Pow(fitness, weightExponent);
		}

		float currentWeight = 0.0f;

		foreach(TreeSystem treeSystem in systems) {
			float fitness = getFitness(treeSystem);
			
			// The exponentiation will cause larger differences in weight.
			currentWeight += Mathf.Pow(fitness, weightExponent);

			weights.Add(currentWeight / totalWeight);
		}

		return weights;
	}

	protected int getWeightedRandomIndex(List<float> weights) {
		float value = Random.value;

		int valueIndex = weights.Count - 1;

		for(int index = 0; index < weights.Count; index += 1) {
			float weight = weights[index];

			if(value < weight) {
				valueIndex = index;

				break;
			}
		}

		return valueIndex;
	}

	protected float getFitness(TreeSystem system) {
		return system.getResponsiveness();
	}


	protected TreeSystem mutate(TreeSystem system) {
		// It'll be a messy copy that makes no sense, but it'll be reset before being used and updated after being used.
		TreeSystem copy = system.copy() as TreeSystem;

		string[] outputVariables = system.getVariables();

		List<string> inputVariables = new List<string>(outputVariables);
		inputVariables.Remove("[");
		inputVariables.Remove("]");

		foreach(string inputVariable in inputVariables) {
			string production = system.getProduction(inputVariable);

			StringBuilder builder = new StringBuilder();

			for(int index = 0; index < production.Length; index += 1) {
				string productionVariable = production[index].ToString();

				float mutation = Random.value;

				if(mutation < mutationRate) {
					int selection = Random.Range(0, 3);

					if(selection == 0) {
						// We add a new variable instead of the current non-branching one.
						List<string> variables = new List<string>(outputVariables);

						variables.Remove(productionVariable);

						int outputVariableIndex = Random.Range(0, variables.Count);
						string outputVariable = variables[outputVariableIndex];

						if(productionVariable == "[" || productionVariable == "]") {
							outputVariable = productionVariable;
						}
						else if(outputVariable == "[" || outputVariable == "]") {
							outputVariable = "[]";
						}

						builder.Append(outputVariable);
					}
					else if(selection == 1) {
						// We add the current variable as well as a new variable next to it.
						int outputVariableIndex = Random.Range(0, outputVariables.Length);
						string outputVariable = outputVariables[outputVariableIndex];

						float side = Random.Range(0, 2);

						if(outputVariable == "[" || outputVariable == "]") {
							outputVariable = "[]";
						}

						if(side == 0) {
							builder.Append(outputVariable);
							builder.Append(productionVariable);
						}
						else {
							builder.Append(productionVariable);
							builder.Append(outputVariable);
						}
					}
					else {
						// We don't add any non-branching variables.
						if(productionVariable == "[" || productionVariable == "]") {
							builder.Append(productionVariable);
						}
					}
				}
				else {
					builder.Append(productionVariable);
				}
			}

			copy.setProduction(inputVariable, builder.ToString());
		}

		return copy;
	}


	protected void debugAverages(List<TreeSystem> systems, int evolution) {
		float averageResponsiveness = 0.0f;
		float averageUnresponsiveness = 0.0f;
		float averageEmptiness = 0.0f;

		foreach(TreeSystem system in systems) {
			averageResponsiveness += system.getResponsiveness() / systems.Count;
			averageUnresponsiveness += system.getUnresponsiveness() / systems.Count;
			averageEmptiness += system.getEmptiness() / systems.Count;
		}

		Debug.LogFormat("#{0:0000} | R: {1:0.000000} | U: {2:0.000000} | E: {3:0.000000}", evolution, averageResponsiveness, averageUnresponsiveness, averageEmptiness);
	}
}
