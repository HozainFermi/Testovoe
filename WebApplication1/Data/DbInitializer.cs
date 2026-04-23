using WebApplication1.Data.Entities;
    
namespace WebApplication1.Data
{
        public static class DbInitializer
        {
            public static void Initialize(AppDbContext context)
            {
                if (context.Departments.Any() || context.Educations.Any())
                {
                    return; 
                }

             
                var educations = new Education[]
                {
                new Education { Id = Guid.NewGuid(), EducationLevel = EducationLevel.Basic_general, CreatedAt = DateTime.UtcNow },
                new Education { Id = Guid.NewGuid(), EducationLevel = EducationLevel.Secondary_general_education, CreatedAt = DateTime.UtcNow },
                new Education { Id = Guid.NewGuid(), EducationLevel = EducationLevel.Specialized_secondary_education, CreatedAt = DateTime.UtcNow },
                new Education { Id = Guid.NewGuid(), EducationLevel = EducationLevel.Bachelors_Degree, CreatedAt = DateTime.UtcNow },
                new Education { Id = Guid.NewGuid(), EducationLevel = EducationLevel.Specialist_Degree, CreatedAt = DateTime.UtcNow },
                new Education { Id = Guid.NewGuid(), EducationLevel = EducationLevel.Masters_Degree, CreatedAt = DateTime.UtcNow },
                new Education { Id = Guid.NewGuid(), EducationLevel = EducationLevel.Candidate_of_Sciences, CreatedAt = DateTime.UtcNow },
                new Education { Id = Guid.NewGuid(), EducationLevel = EducationLevel.Doctor_of_Sciences, CreatedAt = DateTime.UtcNow }
                };
                context.Educations.AddRange(educations);

             
                var departments = new Department[]
                {
                new Department { Id = Guid.NewGuid(), DepartmentName = "IT-отдел", CreatedAt = DateTime.UtcNow },
                new Department { Id = Guid.NewGuid(), DepartmentName = "Бухгалтерия", CreatedAt = DateTime.UtcNow },
                new Department { Id = Guid.NewGuid(), DepartmentName = "Отдел кадров", CreatedAt = DateTime.UtcNow },
                new Department { Id = Guid.NewGuid(), DepartmentName = "Производственный отдел", CreatedAt = DateTime.UtcNow },
                new Department { Id = Guid.NewGuid(), DepartmentName = "Отдел продаж", CreatedAt = DateTime.UtcNow },
                new Department { Id = Guid.NewGuid(), DepartmentName = "Склад", CreatedAt = DateTime.UtcNow },
                new Department { Id = Guid.NewGuid(), DepartmentName = "Юридический отдел", CreatedAt = DateTime.UtcNow },
                new Department { Id = Guid.NewGuid(), DepartmentName = "Отдел маркетинга", CreatedAt = DateTime.UtcNow }
                };
                context.Departments.AddRange(departments);

           
                context.SaveChanges();

         
                var itDept = departments[0];
                var hrDept = departments[2];
                var accounting = departments[1];

                var bachelor = educations.First(e => e.EducationLevel == EducationLevel.Bachelors_Degree);
                var master = educations.First(e => e.EducationLevel == EducationLevel.Masters_Degree);
                var specialized = educations.First(e => e.EducationLevel == EducationLevel.Specialized_secondary_education);

                var employees = new Employee[]
                {
                new Employee
                {
                    Id = Guid.NewGuid(),
                    StuffNumber = 1001,
                    FullName = "Иванов Иван Иванович",
                    BirthDate = new DateTime(1990, 5, 15),
                    HireDate = new DateTime(2020, 1, 10),
                    FiredDate = null,
                    DepartmentId = itDept.Id,
                    EducationId = bachelor.Id,
                    CreatedAt = DateTime.UtcNow
                },
                new Employee
                {
                    Id = Guid.NewGuid(),
                    StuffNumber = 1002,
                    FullName = "Петрова Екатерина Алексеевна",
                    BirthDate = new DateTime(1988, 8, 22),
                    HireDate = new DateTime(2019, 3, 15),
                    FiredDate = null,
                    DepartmentId = accounting.Id,
                    EducationId = master.Id,
                    CreatedAt = DateTime.UtcNow
                },
                new Employee
                {
                    Id = Guid.NewGuid(),
                    StuffNumber = 1003,
                    FullName = "Сидоров Алексей Сергеевич",
                    BirthDate = new DateTime(1995, 12, 3),
                    HireDate = new DateTime(2021, 6, 20),
                    FiredDate = null,
                    DepartmentId = itDept.Id,
                    EducationId = specialized.Id,
                    CreatedAt = DateTime.UtcNow
                },
                new Employee
                {
                    Id = Guid.NewGuid(),
                    StuffNumber = 1004,
                    FullName = "Козлова Мария Дмитриевна",
                    BirthDate = new DateTime(1992, 3, 28),
                    HireDate = new DateTime(2018, 9, 1),
                    FiredDate = null,
                    DepartmentId = hrDept.Id,
                    EducationId = bachelor.Id,
                    CreatedAt = DateTime.UtcNow
                },
                new Employee
                {
                    Id = Guid.NewGuid(),
                    StuffNumber = 1005,
                    FullName = "Соколов Денис Андреевич",
                    BirthDate = new DateTime(1998, 7, 19),
                    HireDate = new DateTime(2022, 2, 14),
                    FiredDate = null,
                    DepartmentId = departments[4].Id, 
                    EducationId = bachelor.Id,
                    CreatedAt = DateTime.UtcNow
                },
                new Employee
                {
                    Id = Guid.NewGuid(),
                    StuffNumber = 1006,
                    FullName = "Морозова Анна Владимировна",
                    BirthDate = new DateTime(1985, 11, 10),
                    HireDate = new DateTime(2015, 5, 25),
                    FiredDate = new DateTime(2023, 12, 31), 
                    DepartmentId = departments[5].Id, 
                    EducationId = specialized.Id,
                    CreatedAt = DateTime.UtcNow
                }
                };
                context.Employees.AddRange(employees);
                context.SaveChanges();

         
                var promotions = new Promotion[]
                {
                new Promotion
                {
                    Id = Guid.NewGuid(),
                    EmployeeId = employees[0].Id,
                    IncreasingPercent = 15.0f,
                    CreatedAt = DateTime.UtcNow
                },
                new Promotion
                {
                    Id = Guid.NewGuid(),
                    EmployeeId = employees[2].Id, 
                    IncreasingPercent = 10.0f,
                    CreatedAt = DateTime.UtcNow
                }
                };
                context.Promotions.AddRange(promotions);
                context.SaveChanges();
            }
        }
}

