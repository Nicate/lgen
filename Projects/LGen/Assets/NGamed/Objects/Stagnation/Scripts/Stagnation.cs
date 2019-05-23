using UnityEngine;

public class Stagnation : Evolution {
	protected override LSystem[] evolveSystems(LSystem[] systems) {
		LSystem[] evolvedSystems = new LSystem[systems.Length];

		for(int index = 0; index < systems.Length; index += 1) {
			evolvedSystems[index] = systems[index].copy();
		}

		return evolvedSystems;
	}
}
