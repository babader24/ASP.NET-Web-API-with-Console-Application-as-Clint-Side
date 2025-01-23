using Student_API_Project.Models;

namespace Student_API_Project.Data_Simolation
{
	public class StudentsData
	{
		public static readonly List<Student> studentsList = new List<Student>
		{
			new Student{StudentId = 1,Name= "Ahmed Babader",Age = 22, Grade = 90},
			new Student{StudentId = 2,Name= "Osama Mohammed",Age = 21, Grade = 45},
			new Student{StudentId = 3,Name= "Albasi Monaser",Age = 90, Grade =59},
			new Student{StudentId = 4,Name= "Ammar shamsan",Age = 40, Grade = 99}
		};
	}
}
