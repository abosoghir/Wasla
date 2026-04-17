using EduBrain.Common.Enums;

namespace EduBrain.Common.Grading;

public class GradingService : IGradingService
{
    /// <summary>
    /// Article (15) A - Grading System based on percentage
    /// </summary>
    public Grade CalculateGradeFromPercentage(decimal percentage)
    {
        return percentage switch
        {
            >= 95 => Grade.APlus,   // A⁺ - Excellent
            >= 90 => Grade.A,       // A - Excellent
            >= 85 => Grade.AMinus,  // A⁻ - Excellent
            >= 80 => Grade.BPlus,   // B⁺ - Very Good
            >= 75 => Grade.B,       // B - Very Good
            >= 70 => Grade.CPlus,   // C⁺ - Good
            >= 65 => Grade.C,       // C - Good
            >= 60 => Grade.DPlus,    // D⁺ - Pass
            >= 55 => Grade.D,       // D - Conditional Pass
            >= 50 => Grade.DMinus,  // D⁻ - Conditional Pass
            _ => Grade.F            // F - Fail
        };
    }

    /// <summary>
    /// Article (15) A - Grade Points Table
    /// </summary>
    public decimal GetGradePoints(Grade grade)
    {
        return grade switch
        {
            Grade.APlus => 4.0m,
            Grade.A => 3.7m,
            Grade.AMinus => 3.3m,
            Grade.BPlus => 3.0m,
            Grade.B => 2.8m,
            Grade.CPlus => 2.6m,
            Grade.C => 2.3m,
            Grade.DPlus => 2.0m,
            Grade.D => 1.7m,
            Grade.DMinus => 1.4m,
            Grade.F => 0.0m,
            _ => 0.0m
        };
    }

    /// <summary>
    /// Article (15) A - Descriptive Grades
    /// </summary>
    public string GetDescriptiveGrade(Grade grade)
    {
        return grade switch
        {
            Grade.APlus => "Excellent",
            Grade.A => "Excellent",
            Grade.AMinus => "Excellent",
            Grade.BPlus => "Very Good",
            Grade.B => "Very Good",
            Grade.CPlus => "Good",
            Grade.C => "Good",
            Grade.DPlus => "Pass",
            Grade.D => "Conditional Pass",
            Grade.DMinus => "Conditional Pass",
            Grade.F => "Fail",
            _ => "Unknown"
        };
    }

    /// <summary>
    /// Article (15) C - Semester GPA Calculation
    /// Formula: Sum(points × credit hours) / Sum(credit hours)
    /// Result rounded to 2 decimal places
    /// </summary>
    public decimal CalculateSemesterGPA(IEnumerable<EnrollmentGradeInfo> enrollments)
    {
        var enrollmentList = enrollments.ToList();

        if (enrollmentList.Count == 0)
            return 0.0m;

        decimal totalPoints = 0;
        int totalCreditHours = 0;

        foreach (var enrollment in enrollmentList)
        {
            totalPoints += enrollment.GradePoints * enrollment.CreditHours;
            totalCreditHours += enrollment.CreditHours;
        }

        if (totalCreditHours == 0)
            return 0.0m;

        var gpa = totalPoints / totalCreditHours;
        return Math.Round(gpa, 2, MidpointRounding.AwayFromZero);
    }

    /// <summary>
    /// Article (15) C - Cumulative GPA Calculation
    /// Formula: Sum(points × credit hours for all courses) / Sum(all credit hours)
    /// Result rounded to 2 decimal places
    /// </summary>
    public decimal CalculateCumulativeGPA(IEnumerable<EnrollmentGradeInfo> allEnrollments)
    {
        // Uses the same formula as semester GPA but across all semesters
        return CalculateSemesterGPA(allEnrollments);
    }

    /// <summary>
    /// Article (15) D - General Grade Calculation based on Cumulative GPA
    /// </summary>
    public (Grade Grade, string DescriptiveGrade) DetermineGeneralGrade(decimal cumulativeGPA)
    {
        // Article (15) B - Must have at least 1.4 to pass
        if (cumulativeGPA < 1.4m)
            return (Grade.F, "Very Poor");

        return cumulativeGPA switch
        {
            >= 3.8m => (Grade.APlus, "Excellent"),
            >= 3.3m => (Grade.A, "Excellent"),
            >= 3.0m => (Grade.BPlus, "Very Good"),
            >= 2.8m => (Grade.B, "Very Good"),
            >= 2.6m => (Grade.CPlus, "Good"),
            >= 2.3m => (Grade.C, "Good"),
            >= 2.0m => (Grade.DPlus, "Pass"),
            >= 1.4m => (Grade.D, "Poor"),
            _ => (Grade.F, "Very Poor")
        };
    }

    /// <summary>
    /// Article (15) B - Student passes if they obtain at least 1.4 (D⁻)
    /// </summary>
    public bool IsPassingGrade(Grade grade)
    {
        return grade != Grade.F && GetGradePoints(grade) >= 1.4m;
    }

    /// <summary>
    /// Article (15) B - D⁻ students must have cumulative GPA > 2.0
    /// If GPA ≤ 2.0, they are placed on academic probation
    /// </summary>
    public bool NeedsAcademicProbation(Grade courseGrade, decimal cumulativeGPA)
    {
        // Only applies to D⁻ grades
        if (courseGrade != Grade.DMinus)
            return false;

        // Must have cumulative GPA > 2.0, otherwise academic probation
        return cumulativeGPA <= 2.0m;
    }

    /// <summary>
    /// Article (15) E - Honors Requirements:
    /// - Cumulative GPA ≥ 2.8
    /// - No failed courses
    /// - Study period ≤ 4 academic years
    /// </summary>
    public bool QualifiesForHonors(decimal cumulativeGPA, bool hasFailures, int studyYears)
    {
        return cumulativeGPA >= 2.8m
               && !hasFailures
               && studyYears <= 4;
    }
}
