using AcademicManagement;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;

using System.Collections.Generic;

namespace Lab3.Pages
{
    #region CourseComparer
/*    public class CourseComparer : IComparer<Course>
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

        public int Compare(Course c1, Course c2)
        {
            if (c1 == null && c2 == null) return 0;
            else if (c1 == null && c2 != null) return -1;
            else if (c1 != null && c2 == null) return 1;
            else
            {
                if (CompareBy == "code")
                {
                    return c1.CourseCode.CompareTo(c2.CourseCode);
                }
                else if (CompareBy == "title")
                {
                    return c1.CourseTitle.CompareTo(c2.CourseTitle);
                }
                else throw new Exception("Unsupported comparing criteria!");
            }

        }
    }*/
    #endregion




    public class CourseSelection
    {
        public Course TheCourse { get; set; }
        public bool Selected { get; set; }
    }


    public class RegistrationModel : PageModel
    {
        [BindProperty]
        public string SelectedStudentId { get; set; } = "-1";

        [BindProperty]
        public List<CourseSelection> CourseSelections { get; set; }

/*        [BindProperty]
        public List<SelectListItem> CourseSelections { get; set; }*/
        
/*        [BindProperty]
        public double Grade { get; set; }*/

        [BindProperty]
        public List<AcademicRecord> AcademicRecordsOfSelectedStudent { get; set; }
        //public List<Course> SelectedCourses { get; set; }

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

                HttpContext.Session.SetString("SelectedStudentId", SelectedStudentId);

                if (AcademicRecordsOfSelectedStudent.Count() == 0)
                {
                    Msg = "The student has not registered any course. Select course(s) to register.";
                    BuildCourseSelections();
                }
                else
                {
                    Msg = "The student has registered for the following courses.";
                    SortAcademicRecordsOfSelectedStudent();
                }

            }
        }

        
        public void OnPostRegister()
        {
            //List<Course> SelectedCourses = new List<Course>();
            
            foreach (CourseSelection c in CourseSelections)
            {
                if (c.Selected)
                {
                    //SelectedCourses.Add(DataAccess.GetAllCourses().First(c => c.CourseCode == item.Value));

                    AcademicRecord academicRecord = new AcademicRecord(SelectedStudentId, c.TheCourse.CourseCode);

                    DataAccess.AddAcademicRecord(academicRecord);

                    AcademicRecordsOfSelectedStudent.Add(academicRecord);
                }
            }

            if(CourseSelections.Count() == 0)
            {
                Msg = "You must select at least one course!";

                BuildCourseSelections();
            }
            else
            {
                SortAcademicRecordsOfSelectedStudent();

                Msg = "The student has registered for the following courses. You can enter or edit the grades";
            }

            
        }

        public void OnPostSubmitGrades()
        {
            //to match the selected CourseCode to GetAcademicRecordsByStudentId().CourseCode, then apply the Grade that way:

            /*List<AcademicRecord> a = DataAccess.GetAcademicRecordsByStudentId(SelectedStudentId);
            for (int i = 0; i < AcademicRecordsOfSelectedStudent.Count(); i++)
            {
                if (AcademicRecordsOfSelectedStudent[i].CourseCode == a[i].CourseCode)  
                {
                    a[i].Grade = AcademicRecordsOfSelectedStudent[i].Grade;
                }
               
            }*/

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
}
