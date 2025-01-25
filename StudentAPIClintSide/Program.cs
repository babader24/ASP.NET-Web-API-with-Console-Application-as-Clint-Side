using System;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using static Program;

class Program
{
	static readonly HttpClient httpClient = new HttpClient();

	static async Task Main(string[] args)
	{
		httpClient.BaseAddress = new Uri("http://localhost:5268/api/MyStudentAPI/");

		 await GetAllStudent();

		 await GetPassedStudent();

		 await GetAvarageGrade();

		 await GetStudentById(4);

		var newStudent = new Student { Name = "Mazen Abdullah", Age = 20, Grade = 85 };
		await AddNewStudent(newStudent);

		await GetAllStudent();

		await DeleteStudent(1);

		await GetAllStudent();
	}

	static async Task GetAllStudent()
	{
		try
		{
			Console.WriteLine("\n_____________________________");
			Console.WriteLine("\nFetching all students...\n");
			var response = await httpClient.GetAsync("All");

			if (response.IsSuccessStatusCode)
			{
				var students = await response.Content.ReadFromJsonAsync<List<Student>>();
				if (students != null && students.Count > 0)
				{
					foreach (var student in students)
					{
						Console.WriteLine($"ID: {student.StudentId}, Name: {student.Name}, Age: {student.Age}, Grade: {student.Grade}");
					}
				}
			}
			else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
			{
				Console.WriteLine("No students found.");
			}
		}
		catch (Exception ex)
		{
			Console.WriteLine($"An error occurred: {ex.Message}");
		}
	}

	static async Task GetPassedStudent()
	{
		try
		{
			Console.WriteLine("\n__________________________________________");
			Console.WriteLine("\nFetching Passed Students...\n");
			var response = await httpClient.GetAsync("Passed");

			if (response.IsSuccessStatusCode)
			{
				var passedStudents = await response.Content.ReadFromJsonAsync<List<Student>>();
				if (passedStudents != null && passedStudents.Count > 0)
				{
					foreach (var student in passedStudents)
					{
						Console.WriteLine($"ID: {student.StudentId}, Name: {student.Name}, Age: {student.Age}, Grade: {student.Grade}");
					}
				}
			}
			else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
			{
				Console.WriteLine("No passed students found.");
			}
		}
		catch (Exception ex)
		{
			Console.WriteLine($"An error occurred: {ex.Message}");
		}
	}

	static async Task GetAvarageGrade()
	{
		try
		{
			Console.WriteLine("\n_____________________________");
			Console.WriteLine("\nFetching average grade...\n");
			var response = await httpClient.GetAsync("AverageGrade");

			if (response.IsSuccessStatusCode)
			{
				var averageGrade = await response.Content.ReadFromJsonAsync<double>();
				Console.WriteLine($"Average Grade: {averageGrade}");
			}
			else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
			{
				Console.WriteLine("No students found.");
			}
		}
		catch (Exception ex)
		{
			Console.WriteLine($"An error occurred: {ex.Message}");
		}
	}

	static async Task GetStudentById(int id)
	{
		var response = await httpClient.GetAsync($"{id}");

		if (response.IsSuccessStatusCode)
		{
			var student = await response.Content.ReadFromJsonAsync<Student>();
			if (student != null)
			{
				Console.WriteLine($"ID: {student.StudentId}, Name: {student.Name}, Age: {student.Age}, Grade: {student.Grade}");
			}
		}
		else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
		{
			Console.WriteLine($"Bad Request: Not accepted ID {id}");
		}
		else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
		{
			Console.WriteLine($"Not Found: Student with ID {id} not found.");
		}
	}

	static async Task AddNewStudent(Student newStudent)
	{
		try
		{
			Console.WriteLine("\n_____________________________");
			Console.WriteLine("\nAdding a new student...\n");

			var response = await httpClient.PostAsJsonAsync("", newStudent);

			if (response.IsSuccessStatusCode)
			{
				var addedStudent = await response.Content.ReadFromJsonAsync<Student>();
				Console.WriteLine($"Added Student - ID: {addedStudent.StudentId}, Name: {addedStudent.Name}, Age: {addedStudent.Age}, Grade: {addedStudent.Grade}");
			}
			else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
			{
				Console.WriteLine("Bad Request: Invalid student data.");
			}

		}
		catch (Exception ex)
		{
			Console.WriteLine($"An error occurred: {ex.Message}");
		}
	}

	static async Task DeleteStudent(int id)
	{
		try
		{	Console.WriteLine("\n_____________________________");
			Console.WriteLine($"\nDeleteing student with ID: {id}...\n");

			var response = await httpClient.DeleteAsync($"{id}");

			Console.WriteLine(await response.Content.ReadAsStringAsync());

		}
		catch (Exception ex)
		{
			Console.WriteLine($"An error occurred: {ex.Message}");
		}

	}
	public class Student
	{
		public int StudentId { get; set; }
		public string Name { get; set; }
		public int Age { get; set; }
		public int Grade { get; set; }
	}


}
