using UnityEngine;

public abstract class Evolution : MonoBehaviour {
	[Header("Evolution")]
	public Plants plantsPrefab;

	public int generations = 0;

	[Space]
	public bool debugEvolution = false;


	private Plants plants;

	private LSystem[] systems;


	protected virtual void Awake() {
		plants = Instantiate(plantsPrefab, transform);

		systems = new LSystem[plants.getNumberOfPlants()];
	}

	protected virtual void Start() {
		plants.growPlants();

		systems = new LSystem[plants.getNumberOfPlants()];

		evolve();
	}
	
	protected virtual void Update() {
		if(debugEvolution) {
			if(Input.GetKeyDown(KeyCode.Return)) {
				evolvePlants();
			}
		}
	}


	public void evolve() {
		for(int generation = 0; generation < generations; generation += 1) {
			evolvePlants();
		}
	}
	
	public virtual void evolvePlants() {
		systems = evolveSystems(systems);

		for(int index = 0; index < plants.getNumberOfPlants(); index += 1) {
			Plant plant = plants[index];
			LSystem system = systems[index];

			// Circumvent plant.updatePlant() because we are essentially replacing the plant without respawning it.
			plant.interpret(system);
			plant.evaluate();
		}
	}

	protected abstract LSystem[] evolveSystems(LSystem[] systems);
}
