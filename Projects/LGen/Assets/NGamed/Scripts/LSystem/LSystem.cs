using System.Collections;
using System.Collections.Generic;
using System.Text;

public class LSystem : IEnumerable<string> {
	private List<string> variables;
	private string axiom;
	private Dictionary<string, string> productions;

	private string sequence;
	private int generation;


	public LSystem() {
		variables = new List<string>();
		axiom = "";
		productions = new Dictionary<string, string>();

		sequence = "";
		generation = -1;
	}

	public LSystem(LSystem system) : this() {
		foreach(string variable in system.getVariables()) {
			addVariable(variable);
		}

		axiom = system.getAxiom();

		foreach(string variable in variables) {
			setProduction(variable, system.getProduction(variable));
		}
	}


	public LSystem copy() {
		return new LSystem(this);
	}


	public string[] getVariables() {
		return variables.ToArray();
	}

	public bool hasVariable(string variable) {
		return variables.Contains(variable);
	}

	public void addVariable(string variable) {
		if(variable != null && variable.Length == 1) {
			variables.Add(variable);
			productions.Add(variable, variable);
		}
	}

	public void removeVariable(string variable) {
		if(hasVariable(variable)) {
			variables.Remove(variable);
			productions.Remove(variable);
		}
	}


	public string getAxiom() {
		return axiom;
	}
	
	public void setAxiom(string axiom) {
		if(axiom != null && axiom.Length > 0) {
			this.axiom = axiom;
		}
	}


	public string getProduction(string variable) {
		if(hasVariable(variable)) {
			return productions[variable];
		}
		else {
			return "";
		}
	}

	public void setProduction(string variable, string production) {
		if(hasVariable(variable) && production != null && production.Length > 0) {
			productions[variable] = production;
		}
	}


	public string getSequence() {
		return sequence;
	}

	public IEnumerator<string> GetEnumerator() {
		foreach(char character in sequence) {
			yield return character.ToString();
		}
	}

	IEnumerator IEnumerable.GetEnumerator() {
		return GetEnumerator();
	}


	public bool hasGenerations() {
		return generation >= 0;
	}

	public int getGeneration() {
		return generation;
	}


	public void reset() {
		sequence = axiom;

		generation = 0;
	}

	public void generate() {
		if(hasGenerations()) {
			StringBuilder builder = new StringBuilder();

			foreach(string variable in this) {
				if(hasVariable(variable)) {
					builder.Append(getProduction(variable));
				}
			}

			sequence = builder.ToString();

			generation += 1;
		}
	}

	public void generate(int generations) {
		for(int count = 0; count < generations; count += 1) {
			generate();
		}
	}

	public string generateSequence(int generations) {
		reset();

		generate(generations);

		return sequence;
	}
}
