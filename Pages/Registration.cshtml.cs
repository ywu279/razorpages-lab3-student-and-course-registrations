using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using AcademicManagement;
using Microsoft.AspNetCore.Http;

namespace Lab3.Pages
{
    public class RegistrationModel : PageModel
    {
        [BindProperty]
        public string SelectedStudentId { get; set; } = "-1";

        [BindProperty]
        public List<SelectListItem> CourseSelections { get; set; }
        
/*        [BindProperty]
        public double Grade { get; set; }*/

        [BindProperty]
        public List<AcademicRecord> AcademicRecordsOfSelectedStudent { get; set; }
        //public List<Course> SelectedCourses { get; set; }

        public string Msg { get; set; } = "";
      

        public void OnGet()
        {

        }

        public void OnPost()
        {
            
        }

        public void OnPostStudentSelected()
        {
            AcademicRecordsOfSelectedStudent = DataAccess.GetAcademicRecordsByStudentId(SelectedStudentId);

            if (SelectedStudentId == "-1")
            {
                Msg = "You must select a student!";
                AcademicRecordsOfSelectedStudent = null;
                CourseSelections = null;
            }
            else
            {
                if (AcademicRecordsOfSelectedStudent.Count() == 0)
                {
                    Msg = "The student has not registered any course. Select course(s) to register.";
                }
                else
                {
                    Msg = "The student has registered for the following courses. You can enter or edit the grades";
                }

            }
        }

        
        public void OnPostRegister()
        {
            //List<Course> SelectedCourses = new List<Course>();
            
            foreach (SelectListItem item in CourseSelections)
            {
                if (item.Selected)
                {
                    //SelectedCourses.Add(DataAccess.GetAllCourses().First(c => c.CourseCode == item.Value));

                    AcademicRecord academicRecord = new AcademicRecord(SelectedStudentId, item.Value);

                    DataAccess.AddAcademicRecord(academicRecord);

                    AcademicRecordsOfSelectedStudent.Add(academicRecord);
                }
            }

            if(CourseSelections.Count() == 0)
            {
                Msg = "You must select at least one course!";
            }
            else
            {
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
                foreach (AcademicRecord a in DataAccess.GetAcademicRecordsByStudentId(SelectedStudentId))
                {
                    if (ar.CourseCode == a.CourseCode)
                    {
                        a.Grade = ar.Grade;
                    }
                }
            }
        }


    }
}
