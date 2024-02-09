﻿namespace OgrenciBilgiSistemi.Dto.CourseAssessment
{
    public class CourseAssessmentCreateDto
    {

        public string AssessmentName { get; set; }
        public int PassingScore { get; set;}
        public int ImpactCourseGrade { get; set;}
        public Guid CourseId { get; set; }
        public Guid AssessmentTypeId { get; set; }
    }
}
