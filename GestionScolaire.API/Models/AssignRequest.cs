using System.ComponentModel.DataAnnotations;

public class AssignRequest
{
	[Range(1, int.MaxValue)] // We define the range here 
	public int IdEnseignant { get; set; }

	[Range(1, int.MaxValue)] // Same thing here
	public int IdGroupe { get; set; }
}
