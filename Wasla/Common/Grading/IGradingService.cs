using EduBrain.Common.Enums;

namespace EduBrain.Common.Grading;

public interface IGradingService
{
    /// <summary>
    /// Calculates the letter grade from a percentage score based on regulations
    /// </summary>
    Grade CalculateGradeFromPercentage(decimal percentage);

    /// <summary>
    /// Gets the grade points for a specific grade (A+ = 4.0, A = 3.7, etc.)
    /// </summary>
    decimal GetGradePoints(Grade grade);

    /// <summary>
    /// Gets the descriptive grade for a letter grade
    /// </summary>
    string GetDescriptiveGrade(Grade grade);

    /// <summary>
    /// Calculates Semester GPA from enrollments
    /// Formula: Sum(points × credit hours) / Sum(credit hours)
    /// </summary>
    decimal CalculateSemesterGPA(IEnumerable<EnrollmentGradeInfo> enrollments);

    /// <summary>
    /// Calculates Cumulative GPA from all completed enrollments
    /// Formula: Sum(points × credit hours for all courses) / Sum(all credit hours)
    /// </summary>
    decimal CalculateCumulativeGPA(IEnumerable<EnrollmentGradeInfo> allEnrollments);

    /// <summary>
    /// Determines the general grade from cumulative GPA
    /// </summary>
    (Grade Grade, string DescriptiveGrade) DetermineGeneralGrade(decimal cumulativeGPA);

    /// <summary>
    /// Checks if a grade is a passing grade (≥ D⁻ which is 1.4 points)
    /// </summary>
    bool IsPassingGrade(Grade grade);

    /// <summary>
    /// Checks if D⁻ student needs academic probation (cumulative GPA ≤ 2.0)
    /// </summary>
    bool NeedsAcademicProbation(Grade courseGrade, decimal cumulativeGPA);

    /// <summary>
    /// Checks if student qualifies for honors (GPA ≥ 2.8, no failures, completed in ≤ 4 years)
    /// </summary>
    bool QualifiesForHonors(decimal cumulativeGPA, bool hasFailures, int studyYears);
}

/// <summary>
/// Information needed for GPA calculations
/// </summary>
public record EnrollmentGradeInfo
{
    public required decimal GradePoints { get; init; }
    public required int CreditHours { get; init; }
    public required Grade LetterGrade { get; init; }
}
