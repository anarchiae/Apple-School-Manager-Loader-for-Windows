using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pomme_School_Manager
{
    class AppleRoster
    {
        private string rosterId;
        private string classId;
        private string studentId;

        //Assesseurs
        public string RosterId
        {
            get { return rosterId; }
            set { rosterId = value; }
        }

        public string ClassId
        {
            get { return classId; }
            set { classId = value; }
        }

        public string StudentId
        {
            get { return studentId; }
            set { studentId = value; }
        }

        /**
         * Constructeur
         */
        public AppleRoster(string nRosterId, string nClassId, string nStudentId)
        {
            this.rosterId = nRosterId;
            this.classId = nClassId;
            this.studentId = nStudentId;
        }
    }
}
