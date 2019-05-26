using System;
using System.Text;

// "System" already exists somewhere within .NET :P
public class PlantSystem : LSystem {
	private string name;


	public PlantSystem() {
		name = "";
	}


	public string getName() {
		return name;
	}

	public void setName(string name) {
		this.name = name;
	}


	public virtual PlantSystem copy() {
		PlantSystem system = Activator.CreateInstance(GetType()) as PlantSystem;
		
		string[] variables = getVariables();

		foreach(string variable in variables) {
			system.addVariable(variable);
		}

		system.setAxiom(getAxiom());

		foreach(string variable in variables) {
			system.setProduction(variable, getProduction(variable));
		}

		system.setName(getName());

		return system;
	}


	public override string ToString() {
		StringBuilder builder = new StringBuilder();

		string[] variables = getVariables();
		
		builder.AppendFormat("System: {0}\n", name);
		builder.AppendFormat("Variables: {0}\n", string.Join(", ", variables));
		builder.Append("Productions:\n");

		foreach(string variable in variables) {
			builder.AppendFormat("  {0} -> {1}\n", variable, getProduction(variable));
		}

		builder.AppendFormat("\nSequence: {0}", getSequence());

		return builder.ToString();
	}
}
