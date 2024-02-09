﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OgrenciBilgiSistemi.Domain.DataBaseContext;

#nullable disable

namespace OgrenciBilgiSistemi.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20240102181746_mig5")]
    partial class mig5
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("OgrenciBilgiSistemi.Domain.Entities.AssessmentType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<string>("AssessmentTypeName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("assessment_type_name");

                    b.HasKey("Id");

                    b.ToTable("AssessmentTypes");
                });

            modelBuilder.Entity("OgrenciBilgiSistemi.Domain.Entities.City", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<string>("CityName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("city_Name");

                    b.HasKey("Id");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("OgrenciBilgiSistemi.Domain.Entities.Class", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<string>("ClassName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("class_name");

                    b.HasKey("Id");

                    b.ToTable("Classes");
                });

            modelBuilder.Entity("OgrenciBilgiSistemi.Domain.Entities.Course", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<int>("CourseAkts")
                        .HasColumnType("int")
                        .HasColumnName("course_akts");

                    b.Property<string>("CourseCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("course_code");

                    b.Property<string>("CourseCredit")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("course_credit");

                    b.Property<string>("CourseName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("course_name");

                    b.Property<bool>("PracticalCourse")
                        .HasColumnType("bit")
                        .HasColumnName("practical_course");

                    b.Property<bool>("PreparationCourse")
                        .HasColumnType("bit")
                        .HasColumnName("preparation_course ");

                    b.HasKey("Id");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("OgrenciBilgiSistemi.Domain.Entities.CourseAssessment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<string>("AssessmentName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("assesment_name");

                    b.Property<Guid>("AssessmentTypeId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("assessment_type_id");

                    b.Property<string>("AssessmentYear")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("assesment_year");

                    b.Property<Guid>("CourseId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("course_id");

                    b.Property<int>("PassingScore")
                        .HasColumnType("int")
                        .HasColumnName("passing_score");

                    b.Property<Guid>("TeacherId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("teacher_id");

                    b.HasKey("Id");

                    b.HasIndex("AssessmentTypeId");

                    b.HasIndex("CourseId");

                    b.HasIndex("TeacherId");

                    b.ToTable("CourseAssessments");
                });

            modelBuilder.Entity("OgrenciBilgiSistemi.Domain.Entities.CourseSelection", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<Guid>("ClassId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("class_id");

                    b.Property<bool>("IsRegistrationConfirmed")
                        .HasColumnType("bit")
                        .HasColumnName("is_registiration_confirmed");

                    b.Property<Guid>("SemesterCoursesId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("semester_courses_id");

                    b.Property<Guid>("StudentId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("student_id");

                    b.HasKey("Id");

                    b.HasIndex("SemesterCoursesId");

                    b.ToTable("CourseSelections");
                });

            modelBuilder.Entity("OgrenciBilgiSistemi.Domain.Entities.CurrentSemester", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<string>("AssessmentYear")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("assesment_year");

                    b.Property<Guid>("SemesterId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("semesterId");

                    b.HasKey("Id");

                    b.HasIndex("SemesterId");

                    b.ToTable("CurrentSemesters");
                });

            modelBuilder.Entity("OgrenciBilgiSistemi.Domain.Entities.Department", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<Guid>("DepartmentLanguageId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("department_language_id");

                    b.Property<string>("DepartmentName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("department_name");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentLanguageId");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("OgrenciBilgiSistemi.Domain.Entities.DepartmentLanguage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<string>("LanguageName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("language_name");

                    b.HasKey("Id");

                    b.ToTable("DepartmentLanguages");
                });

            modelBuilder.Entity("OgrenciBilgiSistemi.Domain.Entities.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<string>("RoleDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("role_description");

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("role_name");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("OgrenciBilgiSistemi.Domain.Entities.Semester", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<string>("SemesterName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("semester_name");

                    b.HasKey("Id");

                    b.ToTable("Semesters");
                });

            modelBuilder.Entity("OgrenciBilgiSistemi.Domain.Entities.SemesterCourses", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<Guid>("ClassId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("class_id");

                    b.Property<Guid>("CourseId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("course_id");

                    b.Property<Guid>("DepartmentId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("department_id");

                    b.Property<Guid>("SemesterId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("semester_id");

                    b.Property<Guid>("TeacherId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("teacher_id");

                    b.HasKey("Id");

                    b.HasIndex("ClassId");

                    b.HasIndex("CourseId");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("SemesterId");

                    b.HasIndex("TeacherId");

                    b.ToTable("RoleSemesterCoursess");
                });

            modelBuilder.Entity("OgrenciBilgiSistemi.Domain.Entities.Student", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<DateTime>("Birthday")
                        .HasColumnType("datetime2")
                        .HasColumnName("birthday");

                    b.Property<Guid>("CityId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("city_id");

                    b.Property<Guid>("ClassId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("class_id");

                    b.Property<Guid>("DepartmentId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("department_id");

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("email_address");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("name");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("phone_number");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("surname");

                    b.Property<string>("TC")
                        .IsRequired()
                        .HasColumnType("nvarchar(11)")
                        .HasColumnName("tc");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.HasIndex("ClassId");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("UserId");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("OgrenciBilgiSistemi.Domain.Entities.StudentAssessmentGrade", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<int>("AssessmentNote")
                        .HasColumnType("int")
                        .HasColumnName("assesment_note");

                    b.Property<bool>("PassCourse")
                        .HasColumnType("bit")
                        .HasColumnName("pass_course");

                    b.Property<Guid>("StudentId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("student_id");

                    b.HasKey("Id");

                    b.HasIndex("StudentId");

                    b.ToTable("StudentAssessmentGrades");
                });

            modelBuilder.Entity("OgrenciBilgiSistemi.Domain.Entities.StudentCourse", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<Guid>("CourseId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("course_id");

                    b.Property<Guid>("StudentId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("student_id");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.HasIndex("StudentId");

                    b.ToTable("StudentCourses");
                });

            modelBuilder.Entity("OgrenciBilgiSistemi.Domain.Entities.Teacher", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<DateTime>("Birthday")
                        .HasColumnType("datetime2")
                        .HasColumnName("birthday");

                    b.Property<Guid>("CityId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("city_id");

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("email_address");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("name");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("phone_number");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("surname");

                    b.Property<string>("TC")
                        .IsRequired()
                        .HasColumnType("nvarchar(11)")
                        .HasColumnName("tc");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.HasIndex("UserId");

                    b.ToTable("Teachers");
                });

            modelBuilder.Entity("OgrenciBilgiSistemi.Domain.Entities.TeacherCourse", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<Guid>("CourseId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("course_id");

                    b.Property<Guid>("TeacherId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("teacher_id");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.HasIndex("TeacherId");

                    b.ToTable("TeacherCourses");
                });

            modelBuilder.Entity("OgrenciBilgiSistemi.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("password");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("role_id");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(11)")
                        .HasColumnName("user_name");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("OgrenciBilgiSistemi.Domain.Entities.CourseAssessment", b =>
                {
                    b.HasOne("OgrenciBilgiSistemi.Domain.Entities.AssessmentType", "AssessmentType")
                        .WithMany()
                        .HasForeignKey("AssessmentTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OgrenciBilgiSistemi.Domain.Entities.Course", "Courses")
                        .WithMany()
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OgrenciBilgiSistemi.Domain.Entities.Teacher", "Teacher")
                        .WithMany()
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AssessmentType");

                    b.Navigation("Courses");

                    b.Navigation("Teacher");
                });

            modelBuilder.Entity("OgrenciBilgiSistemi.Domain.Entities.CourseSelection", b =>
                {
                    b.HasOne("OgrenciBilgiSistemi.Domain.Entities.SemesterCourses", "SemesterCourses")
                        .WithMany()
                        .HasForeignKey("SemesterCoursesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SemesterCourses");
                });

            modelBuilder.Entity("OgrenciBilgiSistemi.Domain.Entities.CurrentSemester", b =>
                {
                    b.HasOne("OgrenciBilgiSistemi.Domain.Entities.Semester", "Semester")
                        .WithMany()
                        .HasForeignKey("SemesterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Semester");
                });

            modelBuilder.Entity("OgrenciBilgiSistemi.Domain.Entities.Department", b =>
                {
                    b.HasOne("OgrenciBilgiSistemi.Domain.Entities.DepartmentLanguage", "DepartmentLanguage")
                        .WithMany()
                        .HasForeignKey("DepartmentLanguageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DepartmentLanguage");
                });

            modelBuilder.Entity("OgrenciBilgiSistemi.Domain.Entities.SemesterCourses", b =>
                {
                    b.HasOne("OgrenciBilgiSistemi.Domain.Entities.Class", "Class")
                        .WithMany()
                        .HasForeignKey("ClassId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OgrenciBilgiSistemi.Domain.Entities.Course", "Courses")
                        .WithMany()
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OgrenciBilgiSistemi.Domain.Entities.Department", "Department")
                        .WithMany()
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OgrenciBilgiSistemi.Domain.Entities.Semester", "Semester")
                        .WithMany()
                        .HasForeignKey("SemesterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OgrenciBilgiSistemi.Domain.Entities.Teacher", "Teacher")
                        .WithMany()
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Class");

                    b.Navigation("Courses");

                    b.Navigation("Department");

                    b.Navigation("Semester");

                    b.Navigation("Teacher");
                });

            modelBuilder.Entity("OgrenciBilgiSistemi.Domain.Entities.Student", b =>
                {
                    b.HasOne("OgrenciBilgiSistemi.Domain.Entities.City", "City")
                        .WithMany()
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OgrenciBilgiSistemi.Domain.Entities.Class", "Class")
                        .WithMany()
                        .HasForeignKey("ClassId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OgrenciBilgiSistemi.Domain.Entities.Department", "Department")
                        .WithMany()
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OgrenciBilgiSistemi.Domain.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("City");

                    b.Navigation("Class");

                    b.Navigation("Department");

                    b.Navigation("User");
                });

            modelBuilder.Entity("OgrenciBilgiSistemi.Domain.Entities.StudentAssessmentGrade", b =>
                {
                    b.HasOne("OgrenciBilgiSistemi.Domain.Entities.Student", "Student")
                        .WithMany()
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Student");
                });

            modelBuilder.Entity("OgrenciBilgiSistemi.Domain.Entities.StudentCourse", b =>
                {
                    b.HasOne("OgrenciBilgiSistemi.Domain.Entities.Course", "Courses")
                        .WithMany()
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OgrenciBilgiSistemi.Domain.Entities.Student", "Student")
                        .WithMany()
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Courses");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("OgrenciBilgiSistemi.Domain.Entities.Teacher", b =>
                {
                    b.HasOne("OgrenciBilgiSistemi.Domain.Entities.City", "City")
                        .WithMany()
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OgrenciBilgiSistemi.Domain.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("City");

                    b.Navigation("User");
                });

            modelBuilder.Entity("OgrenciBilgiSistemi.Domain.Entities.TeacherCourse", b =>
                {
                    b.HasOne("OgrenciBilgiSistemi.Domain.Entities.Course", "Course")
                        .WithMany()
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OgrenciBilgiSistemi.Domain.Entities.Teacher", "Teacher")
                        .WithMany()
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");

                    b.Navigation("Teacher");
                });

            modelBuilder.Entity("OgrenciBilgiSistemi.Domain.Entities.User", b =>
                {
                    b.HasOne("OgrenciBilgiSistemi.Domain.Entities.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });
#pragma warning restore 612, 618
        }
    }
}
