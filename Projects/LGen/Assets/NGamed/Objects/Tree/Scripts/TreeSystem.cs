using System.Text;

public class TreeSystem : PlantSystem {
	private float responsiveness;
	private float unresponsiveness;
	private float emptiness;


	public TreeSystem() {
		responsiveness = 0.0f;
		unresponsiveness = 0.0f;
		emptiness = 1.0f;
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


	public override PlantSystem copy() {
		TreeSystem system = base.copy() as TreeSystem;
		
		system.setResponsiveness(responsiveness);
		system.setUnresponsiveness(unresponsiveness);
		system.setEmptiness(emptiness);

		return system;
	}


	public override string ToString() {
		StringBuilder builder = new StringBuilder();

		builder.AppendFormat("{0}\n\n", base.ToString());

		builder.AppendFormat("Reponsiveness: {0}\n", responsiveness);
		builder.AppendFormat("Unresponsiveness: {0}\n", unresponsiveness);
		builder.AppendFormat("Emptiness: {0}", emptiness);

		return builder.ToString();
	}
}
