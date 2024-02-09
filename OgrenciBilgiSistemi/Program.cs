using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using OgrenciBilgiSistemi.Configration;
using OgrenciBilgiSistemi.Domain.DataBaseContext;
using OgrenciBilgiSistemi.Domain.IRepositories;
using OgrenciBilgiSistemi.Domain.Repositories;
using OgrenciBilgiSistemi.Helper;
using System.Runtime.InteropServices.Marshalling;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


//Login olmadan controllera eriþmeyi engelliyor.
builder.Services.AddMvc(config =>
{
    var policy = new AuthorizationPolicyBuilder()
    .RequireAuthenticatedUser().Build();
    config.Filters.Add(new AuthorizeFilter(policy));
});

builder.Services.AddMvc();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(x =>
{
    x.Cookie.Name = "UserLoginCookie";
    x.LoginPath = "/Login/SignIn";
    x.AccessDeniedPath = "/Login/AccessDenied";
});
builder.Services.AddAutoMapperModule();
builder.Services.AddDatabaseModule();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<ITeacherRepository, TeacherRepository>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<ICityRepository, CityRepository>();
builder.Services.AddScoped<IClassRepository, ClassRepository>();
builder.Services.AddScoped<ICourseRepository, CourseRepository>();
builder.Services.AddScoped<ICourseAssessmentRepository, CourseAssessmentRepository>();
builder.Services.AddScoped<IDepartmentLanguageRepository, DepartmentLanguageRepository>();
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
builder.Services.AddScoped<ISemesterCoursesRepository, SemesterCoursesRepository>();
builder.Services.AddScoped<ISemesterRepository, SemesterRepository>();
builder.Services.AddScoped<ITeacherCourseRepository, TeacherCourseRepository>();
builder.Services.AddScoped<ISemesterRepository, SemesterRepository>();
builder.Services.AddScoped<IAssessmentTypeRepository, AssessmentTypeRepository>();
builder.Services.AddScoped<ICurrentSemesterRepository, CurrentSemesterRepository>();
builder.Services.AddScoped<ICourseSelectionRepository, CourseSelectionRepository>();
builder.Services.AddScoped<IExamGradeRepository, ExamGradeRepository>();
builder.Services.AddScoped<IHelper, Helper>();
builder.Services.AddScoped<ICourseLetterScoreRepository, CourseLetterScoreRepository>();
builder.Services.AddScoped<ILetterGradeRepository, LetterGradeRepository>();

var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

DataSeedingStartup.Seed(app);

app.UseHttpsRedirection();
app.UseStaticFiles();
//sisteme Authentication olmak için eklendi.
app.UseAuthentication();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Login}/{action=SignIn}/{id?}");
    endpoints.MapControllerRoute(
           name: "areas",
           pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
         );
});

app.Run();
