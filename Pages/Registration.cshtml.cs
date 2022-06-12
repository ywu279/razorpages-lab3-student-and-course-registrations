using AcademicManagement;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace Lab3.Pages
{
    public class CourseSelection
    {
        public Course TheCourse { get; set; }
        public bool Selected { get; set; }
    }


    public class RegistrationModel : PageModel
    {
        [BindProperty]
        public string SelectedStudentId { get; set; } = "-1";

        /*[BindProperty]
        public List<SelectListItem> CourseSelections { get; set; }*/

        [BindProperty]
        public List<CourseSelection> CourseSelections { get; set; }

        [BindProperty]
        public List<AcademicRecord> AcademicRecordsOfSelectedStudent { get; set; }

        public string Msg { get; set; } = "";
      

        public void OnGet(string orderby)
        {
            if(orderby != null)
            {
                HttpContext.Session.SetString("orderby", orderby);

                SelectedStudentId = HttpContext.Session.GetString("SelectedStudentId");

                if (SelectedStudentId != null && SelectedStudentId != "-1")
                {
                    AcademicRecordsOfSelectedStudent = DataAccess.GetAcademicRecordsByStudentId(SelectedStudentId);
                    if(AcademicRecordsOfSelectedStudent.Count > 0)
                    {
                        SortAcademicRecordsOfSelectedStudent();
                    }
                    else
                    {
                        BuildCourseSelections();
                    }
                }
            }
        }

        public void OnPost()
        {
            
        }

        public void OnPostStudentSelected()
        {
            if (SelectedStudentId == "-1")
            {
                Msg = "You must select a student!";
                AcademicRecordsOfSelectedStudent = null;
                CourseSelections = null;

                HttpContext.Session.Remove("SelectedStudentId");
            }
            else
            {
                HttpContext.Session.SetString("SelectedStudentId", SelectedStudentId);

                AcademicRecordsOfSelectedStudent = DataAccess.GetAcademicRecordsByStudentId(SelectedStudentId);

                if (AcademicRecordsOfSelectedStudent.Count() > 0)
                {
                    Msg = "The student has registered for the following courses.";
                    SortAcademicRecordsOfSelectedStudent();
                }
                else
                {
                    Msg = "The student has not registered any course. Select course(s) to register.";
                    BuildCourseSelections();
                }

            }
        }

        
        public void OnPostRegister()
        {
            foreach (CourseSelection c in CourseSelections)
            {
                if (c.Selected)
                {
                    AcademicRecord academicRecord = new AcademicRecord(SelectedStudentId, c.TheCourse.CourseCode);

                    DataAccess.AddAcademicRecord(academicRecord);

                    AcademicRecordsOfSelectedStudent.Add(academicRecord);
                }
            }

            if(AcademicRecordsOfSelectedStudent.Count() > 0)
            {
                Msg = "The student has registered for the following courses. You can enter or edit the grades";

                SortAcademicRecordsOfSelectedStudent();
            }
            else
            {
                Msg = "You must select at least one course!";

                BuildCourseSelections();
            }

            
        }

        public void OnPostSubmitGrades()
        {
            //to match the selected CourseCode to GetAcademicRecordsByStudentId().CourseCode, then apply the Grade that way:

            foreach(AcademicRecord ar in AcademicRecordsOfSelectedStudent)
            {
                //DataAccess.GetAcademicRecordsByStudentId(SelectedStudentId).First(a => a.CourseCode == ar.CourseCode);
                foreach (AcademicRecord a in DataAccess.GetAcademicRecordsByStudentId(SelectedStudentId))
                {
                    if (ar.CourseCode == a.CourseCode)
                    {
                        a.Grade = ar.Grade;
                    }
                }
            }
        }

        private void BuildCourseSelections()
        {
            CourseSelections = new List<CourseSelection>();
            foreach (Course c in DataAccess.GetAllCourses())
            {
                CourseSelections.Add(new CourseSelection() { TheCourse = c, Selected = false });
            }


            string orderby = HttpContext.Session.GetString("orderby");
            if (orderby == "code")
            {
                //CourseComparer codeComparer = new CourseComparer("code");
                //CourseSelections.Sort(codeComparer);

                CourseSelections.Sort((a, b) => a.TheCourse.CourseCode.CompareTo(b.TheCourse.CourseCode));
            }
            else if (orderby == "title")
            {
                CourseSelections.Sort((a, b) => a.TheCourse.CourseTitle.CompareTo(b.TheCourse.CourseTitle));
            }
        }


        private void SortAcademicRecordsOfSelectedStudent()
        {
            string orderby = HttpContext.Session.GetString("orderby");
            if (orderby == "code")
            {
                AcademicRecordsOfSelectedStudent.Sort((a,b) => a.CourseCode.CompareTo(b.CourseCode));
            }
            else if (orderby == "title")
            {
                AcademicRecordsOfSelectedStudent.Sort(this.ARCourseTitleCompare);
            }
            else if(orderby == "grade")
            {
                AcademicRecordsOfSelectedStudent.Sort((a, b) => a.Grade.CompareTo(b.Grade));
            }
        }

        public int ARCourseTitleCompare(AcademicRecord x, AcademicRecord y)
        {
            Course xCourse = DataAccess.GetAllCourses().First(c => c.CourseCode == x.CourseCode);
            Course yCourse = DataAccess.GetAllCourses().First(c => c.CourseCode == y.CourseCode);

            return xCourse.CourseTitle.CompareTo(yCourse.CourseTitle);
        }

    }



    #region CourseComparer
    /*public class CourseComparer : IComparer<CourseSelection>
    {
        private string CompareBy { get; set; }

        //constructor
        public CourseComparer(string compareBy)
        {
            if (compareBy == "code" || compareBy == "title" || compareBy == "grade")
            {
                CompareBy = compareBy;
            }
            else
            {
                throw new Exception("Unsupported comparing criteria!");
            }
        }

        public int Compare(CourseSelection c1, CourseSelection c2)
        {
            if (c1 == null && c2 == null) return 0;
            else if (c1 == null && c2 != null) return -1;
            else if (c1 != null && c2 == null) return 1;
            else
            {
                if (CompareBy == "code")
                {
                    return c1.TheCourse.CourseCode.CompareTo(c2.TheCourse.CourseCode);
                }
                else if (CompareBy == "title")
                {
                    return c1.TheCourse.CourseTitle.CompareTo(c2.TheCourse.CourseTitle);
                }
                else throw new Exception("Unsupported comparing criteria!");
            }

        }
    }*/
    #endregion
}
