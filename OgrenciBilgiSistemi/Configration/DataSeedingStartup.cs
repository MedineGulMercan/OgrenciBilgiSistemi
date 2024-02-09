using Microsoft.EntityFrameworkCore;
using OgrenciBilgiSistemi.Domain.DataBaseContext;
using OgrenciBilgiSistemi.Domain.Entities;

namespace OgrenciBilgiSistemi.Configration
{
    public static class DataSeedingStartup
    {
        public async static Task Seed(IApplicationBuilder app)
        {
            //inject yapmak için scop oluşturuyoruz
            var scope = app.ApplicationServices.CreateScope();
            //context nesnesini scope ile çekiyoruz
            var context = scope.ServiceProvider.GetService<Context>();

            //migration'ları sistem her çalıştığımda çalıştırır (NOT: sadece çalışmamışları)
            //çalışmadığını ise db içerisinde otomatik oluşan __EFMigrationsHistory tablosunda kayıtlı değilse migration çalışmamış demektir

            try
            {
                await context.Database.MigrateAsync();
            }
            catch (Exception e)
            {
                throw;
            }

            if (!context.Teachers.Any())
            {
                await SeedTeacher(context);
            }
            if (!context.Roles.Any())
            {
                await SeedRole(context);
            } 
            if (!context.Semesters.Any())
            {
                await SeedSemester(context);
            }
            if (!context.CurrentSemesters.Any())
            {
                await SeedCurrentSemester(context);
            }
            if (!context.LetterGrades.Any())
            {
                await SeedLetterGrade(context);
            }
            await context.SaveChangesAsync();
        }
        private async static Task SeedSemester(Context context)
        {
            await context.Semesters.AddAsync(
                new Semester
                {
                    Id = Guid.Parse("068421df-1be1-4143-51d3-08dc0bbccd2d"),
                    SemesterName = "Bahar"
                });
        }
        private async static Task SeedCurrentSemester(Context context)
        {
            await context.CurrentSemesters.AddAsync(new CurrentSemester
            {
                Id= Guid.Parse("b7f80b28-5859-4408-51d4-08dc0bbccd2d"),
                SemesterId= Guid.Parse("068421df-1be1-4143-51d3-08dc0bbccd2d"),
                AssessmentYear = "2024",
                IsActive = true
            });
        }
        private async static Task SeedRole(Context context)
        {
            await context.Roles.AddAsync(
                new Role
                {
                    RoleName = "Ogrenci",
                    RoleDescription = "Öğrenci",
                });
        }
        private async static Task SeedTeacher(Context context)
        {
            await context.Teachers.AddRangeAsync(new List<Teacher>()
            {
                new Teacher
                {
                    Birthday= Convert.ToDateTime("11.11.2001"),
                    Name="Melike",
                    Surname="Mercan",
                    PhoneNumber="05489658745",
                    TC="1234678925",
                        EmailAddress="melike@gmail.com",

                    City=new City
                    {
                        CityName="Erzurum",
                    },
                    User = new User
                    {
                        Password="123",
                        UserName="12312312312",
                        Role = new Role
                        {
                            RoleName = "Ogretmen",
                            RoleDescription="Öğretmen"
                        },

                    },
                },
                new Teacher
                {
                    Birthday= Convert.ToDateTime("12.08.2002"),
                    Name="Medine Gül",
                    Surname="Sivrikaya",
                    PhoneNumber="05489658745",
                    TC="11346789256",
                        EmailAddress="medinesivrikaya@gmail.com",

                    City=new City
                    {
                        CityName = "Giresun",
                    },
                    User = new User
                    {
                        Password="123",
                        UserName="147258369",
                        Role = new Role
                        {
                            RoleName = "Admin",
                            RoleDescription = "Bölüm Başkanı"
                        }
                    },
                }
            });
        }
        private async static Task SeedLetterGrade(Context context)
        {
            await context.LetterGrades.AddRangeAsync(
            new List<LetterGrade>()
            {
                new LetterGrade
                { 
                    Id = Guid.Parse("9b0f2b7f-c11c-47ad-ad0e-c25772172370"),
                    Grade = 4,
                    Letter = "AA",
                    OrderBy = 1
                },
                new LetterGrade
                {
                    Id = Guid.Parse("4e87e01d-b2bf-443e-af62-9d823cf59234"),
                    Grade = 3.50m,
                    Letter = "BA",
                    OrderBy = 2
                },
                new LetterGrade
                {
                    Id = Guid.Parse("f84e7b5b-ee15-40f8-8251-6a883c452049"),
                    Grade = 3,
                    Letter = "BB",
                    OrderBy = 3
                },
                new LetterGrade
                {
                    Id = Guid.Parse("631465cf-bf6e-4df9-95d1-cc88f2e9e829"),
                    Grade = 2.5m,
                    Letter = "CB",
                    OrderBy = 4
                },
                new LetterGrade
                {
                    Id = Guid.Parse("e70f1c68-d67f-4ad2-98c2-e2194e8634ac"),
                    Grade = 2,
                    Letter = "CC",
                    OrderBy = 5
                },
                new LetterGrade
                {
                    Id = Guid.Parse("c5d7b8f9-ee79-4a34-a2d3-78d6e89ae297"),
                    Grade = 1.5m,
                    Letter = "DC",
                    OrderBy = 6
                },
                new LetterGrade
                {
                    Id = Guid.Parse("3079e807-9016-4f16-a27c-10e3afeb0fcd"),
                    Grade = 1,
                    Letter = "DD",
                    OrderBy = 7
                },
                new LetterGrade
                {
                    Id = Guid.Parse("73b0e160-ec6e-405e-a2f9-89dbc7b733f4"),
                    Grade = 0.5m,
                    Letter = "FF",
                    OrderBy = 9
                }
            });
        }
        //private async static Task SeedStudent(Context context)
        //{
        //    await context.Students.AddRangeAsync(new List<Student>()
        //    {
        //        new Student
        //        {
        //            Birthday = Convert.ToDateTime("15.08.2002"),
        //            Name = "Medine Gül",
        //            Surname = "Mercan",
        //            PhoneNumber = "05538697125",
        //            TC = "13346789251",
        //            Number = "1",
        //                EmailAddress="medine@gmail.com",

        //            City = new City
        //            {
        //                CityName = "İstanbul",
        //            },
        //            Class = new Class
        //            {
        //                ClassName = "1"
        //            },
        //            User = new User
        //            {
        //                Password="123",
        //                UserName="159357258",
        //                Role = new Role
        //                {
        //                    RoleName = "Ogrenci",
        //                    RoleDescription = "Öğrenci"
        //                }
        //            },
        //        }
        //    });
        //}
    }
}