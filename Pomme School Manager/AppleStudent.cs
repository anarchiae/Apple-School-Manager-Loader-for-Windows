using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pomme_School_Manager
{
    class AppleStudent : ApplePerson
    {
        private string gradeLevel;
        private string passwordPolicy;

        //Assesseurs
        public string GradeLevel
        {
            get { return gradeLevel; }
            set { gradeLevel = value; }
        }

        public string PasswordPolicy
        {
            get { return passwordPolicy; }
            set { passwordPolicy = value; }
        }


        /**
         * Constructeur
         */
        public AppleStudent(string nPersonId, string nPersonNumber, string nFirstName, string nMiddleName, string nLastName, string nGradeLevel, string nEmailAddress, string nSisUsername, string nPasswordPolicy, string nLocationId)
            : base(nPersonId, nPersonNumber, nFirstName, nMiddleName, nLastName, nEmailAddress, nSisUsername, nLocationId)
        {
            this.gradeLevel = nGradeLevel;
            this.passwordPolicy = nPasswordPolicy;
        }
    }
}
