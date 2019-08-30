using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pomme_School_Manager
{
    class AppleClasse
    {
        private string classId;
        private string classNumber;
        private string courseId;
        private List<string> instructors;
         

        //Assesseurs
        public string ClassId
        {
            get { return classId; }
            set { classId = value; }
        }

        public string ClassNumber
        {
            get { return classNumber; }
            set { classNumber = value; }
        }

        public string CourseId
        {
            get { return courseId; }
            set { courseId = value; }
        }

        public List<string> Instructors
        {
            get { return instructors; }
        }


        /**
         * Constructeur
         */
        public AppleClasse(string nClassId, string nClassNumber, string nCourseId)
        {
            this.classId = nClassId;
            this.classNumber = nClassNumber;
            this.courseId = nCourseId;
            instructors = new List<string>();
        }

        /**
         * Permet d'ajouter des instructors
         */
        public void AddInstructor(string instructorId)
        {
            instructors.Add(instructorId);
        }
    }
}
