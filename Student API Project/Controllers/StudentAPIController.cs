using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Student_API_Project.Data_Simolation;
using Student_API_Project.Models;

namespace Student_API_Project.Controllers
{
	[Route("api/MyStudentAPI")]
	[ApiController]
	public class StudentAPIController : ControllerBase
	{
		[HttpGet("All" , Name= "GetAllStudents")]

		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public ActionResult<IEnumerable<Student>> GatAllStudent()
		{
			if(StudentsData.studentsList.Count == 0)
			{
				return NotFound("No Students Found");
			}

			return Ok(StudentsData.studentsList);
		}


		[HttpGet("Passed", Name = "GetPassedStudents")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public ActionResult<IEnumerable<Student>> GetPassedStudents()
		{
			var pagedStudents = StudentsData.studentsList.Where(student => student.Grade > 60);
			return Ok(pagedStudents);
		}


		[HttpGet("AvarageGrade", Name = "GetAvarageGrade")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public ActionResult<IEnumerable<Student>> GetAvarageGrade()
		{
			var pagedStudents = StudentsData.studentsList.Average(student => student.Grade);
			return Ok(pagedStudents);
		}


		[HttpGet("{id}", Name = "GetStudentById")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]

		public ActionResult<Student> GetStudentById(int id)
		{
			if(id < 0)
			{
				return BadRequest($"Not Accepted ID {id}");
			}

			var student = StudentsData.studentsList.FirstOrDefault(student => student.StudentId == id);
			if(student == null)
			{
				return NotFound($"Student With ID {id}, Not Found");
			}
			return Ok(student);

		}

		[HttpPost(Name = "AddStudent")]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public ActionResult<Student> AddStudent(Student student)
		{
			if (student == null || string.IsNullOrEmpty(student.Name) || student.Age < 0 || student.Grade == 0)
			{
				return BadRequest("Invalid Student Data");
			}

			student.StudentId = StudentsData.studentsList.Count > 0 ? StudentsData.studentsList.Count + 1 : 1;
			StudentsData.studentsList.Add(student);

			return CreatedAtRoute("GetStudentById", new {id = student.StudentId},student);
		}

		[HttpDelete("{id}", Name = "DeleteStudent")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]

		public ActionResult<Student> DeleteStudent(int id)
		{
			if(id < 1)
				return BadRequest($"the ID: {id} , Not Accepted");

			var student = StudentsData.studentsList.FirstOrDefault(x => x.StudentId == id);
			if(student == null)
				return NotFound($"Student with Id: {id} Not Found");

			StudentsData.studentsList.Remove(student);

			return Ok($"Student With ID: {id} has been deleted");
		}

	}
}
