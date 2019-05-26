using System.Text;

// "System" already exists somewhere within .NET :P
public class PlantSystem : LSystem {
	private string name;

	private float responsiveness;
	private float unresponsiveness;
	private float emptiness;


	public PlantSystem() {
		name = "";

		responsiveness = 0.0f;
		unresponsiveness = 0.0f;
		emptiness = 1.0f;
	}


	public string getName() {
		return name;
	}

	public void setName(string name) {
		this.name = name;
	}

	
	public float getResponsiveness() {
		return responsiveness;
	}

	public void setResponsiveness(float responsiveness) {
		this.responsiveness = responsiveness;
	}


	public float getUnresponsiveness() {
		return unresponsiveness;
	}

	public void setUnresponsiveness(float unresponsiveness) {
		this.unresponsiveness = unresponsiveness;
	}


	public float getEmptiness() {
		return emptiness;
	}

	public void setEmptiness(float emptiness) {
		this.emptiness = emptiness;
	}


	public PlantSystem copy() {
		PlantSystem system = new PlantSystem();

		string[] variables = getVariables();

		foreach(string variable in variables) {
			system.addVariable(variable);
		}

		system.setAxiom(getAxiom());

		foreach(string variable in variables) {
			system.setProduction(variable, getProduction(variable));
		}

		system.setName(getName());

		system.setResponsiveness(responsiveness);
		system.setUnresponsiveness(unresponsiveness);
		system.setEmptiness(emptiness);

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

		builder.AppendFormat("\nSequence: {0}\n", getSequence());

		builder.AppendFormat("\nReponsiveness: {0}\n", responsiveness);
		builder.AppendFormat("Unresponsiveness: {0}\n", unresponsiveness);
		builder.AppendFormat("Emptiness: {0}", emptiness);

		return builder.ToString();
	}
}
