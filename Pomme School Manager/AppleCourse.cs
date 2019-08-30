using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pomme_School_Manager
{
    class AppleCourse
    {
        private string courseId;
        private string courseNumber;
        private string courseName;
        private string locationId;

        //Assesseurs
        public string CourseId
        {
            get { return courseId; }
            set { courseId = value; }
        }

        public string CourseNumber
        {
            get { return courseNumber; }
            set { courseNumber = value; }
        }

        public string CourseName
        {
            get { return courseName; }
            set { courseName = value; }
        }

        public string LocationId
        {
            get { return locationId; }
            set { locationId = value; }
        }

        /**
         * Constructeur
         */
        public AppleCourse(string nCourseId, string nCourseNumber, string nCourseName, string nLocationId)
        {
            this.courseId = nCourseId;
            this.courseNumber = nCourseNumber;
            this.courseName = nCourseName;
            this.locationId = nLocationId;
        }
    }
}
