using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CQRS_University_System.Application.DTOs.Students;
using CQRS_University_System.Domain.Entities;

namespace CQRS_University_System.Application.Mappers
{
    public static class StudentMappers
    {
        public static List<StudentDTO> ToStudentDTOs(this List<Student> students) 
        {
            List<StudentDTO> dTOs = new List<StudentDTO>();
            foreach (var student in students) 
            {
                dTOs.Add(new StudentDTO 
                {
                    Id = student.Id,
                    Name = student.Name,    
                    CGPA = student.CGPA,
                    City = student.City,
                    DateOfBirth = student.DateOfBirth,  
                    FinishedHours = student.FinishedHours,
                    Gender = student.Gender
                });
            }
            return dTOs;
        }
    }
}
