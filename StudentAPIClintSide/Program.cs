
using System;
using System.Net.Http.Json;
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


	}

	static async Task GetAllStudent()
	{
		try
		{
			Console.WriteLine("\n__________________________________________");
			Console.WriteLine("\nFetching All Students...\n");
			var Students = await httpClient.GetFromJsonAsync<List<Student>>("All");

			if (Students != null)
			{
				foreach(var student in Students)
				{
					Console.WriteLine($"ID: {student.StudentId} - Name: {student.Name} - Age: {student.Age} - Grade: {student.Grade}");
				}
			}
		}
		catch (Exception ex)
		{
			Console.WriteLine($"An Error Accuered: {ex.Message}");
		}
	}

	static async Task GetPassedStudent()
	{
		try
		{
			Console.WriteLine("\n__________________________________________");
			Console.WriteLine("\nFetching Passed Students...\n");
			var Students = await httpClient.GetFromJsonAsync<List<Student>>("Passed");

			if (Students != null)
			{
				foreach (var student in Students)
				{
					Console.WriteLine($"ID: {student.StudentId} - Name: {student.Name} - Age: {student.Age} - Grade: {student.Grade}");
				}
			}
		}
		catch (Exception ex)
		{
			Console.WriteLine($"An Error Accuered: {ex.Message}");
		}
	}

	static async Task GetAvarageGrade()
	{
		try
		{
			Console.WriteLine("\n__________________________________________");
			Console.WriteLine("\nFetching Students AvarageGrade ...\n");
			var AvarageGrade = await httpClient.GetFromJsonAsync<double>("AvarageGrade");

			Console.WriteLine($"Students Avarage Grade: {AvarageGrade}");
		}
		catch (Exception ex)
		{
			Console.WriteLine($"An Error Accuered: {ex.Message}");
		}
	}

	static async Task GetStudentById(int id)
	{
		try
		{
			Console.WriteLine("\n__________________________________________");
			Console.WriteLine("\nFetching Student By ID ...\n");
			var student = await httpClient.GetFromJsonAsync<Student>($"{id}");

			if(student != null)
				Console.WriteLine($"ID: {student.StudentId} - Name: {student.Name} - Age: {student.Age} - Grade: {student.Grade}");
		}
		catch (Exception ex)
		{
			Console.WriteLine($"An Error Accuered: {ex.Message}");
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
