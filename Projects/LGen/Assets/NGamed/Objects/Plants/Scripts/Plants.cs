using UnityEngine;

public class Plants : MonoBehaviour {
	private void Start() {
		LSystem system = new LSystem();

		system.addVariable("A");
		system.addVariable("B");

		system.setAxiom("A");

		system.setProduction("A", "AB");
		system.setProduction("B", "A");

		Debug.Log(system.generateSequence(4));
	}
}
