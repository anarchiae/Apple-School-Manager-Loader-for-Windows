using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pomme_School_Manager
{
    class ApplePerson
    {
        private string personId;
        private string personNumber;
        private string firstName;
        private string middleName;
        private string lastName;
        private string emailAddress;
        private string sisUsername;
        private string locationId;


        //Assesseurs
        public string PersonId
        {
            get { return personId; }
            set { personId = value; }
        }

        public string PersonNumber
        {
            get { return personNumber; }
            set { personNumber = value; }
        }

        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }

        public string MiddleName
        {
            get { return middleName; }
            set { middleName = value; }
        }

        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }

        public string EmailAddress
        {
            get { return emailAddress; }
            set { emailAddress = value; }
        }

        public string SisUsername
        {
            get { return sisUsername; }
            set { sisUsername = value; }
        }

        public string LocationId
        {
            get { return locationId; }
        }


        /**
         * Constructeur
         */
        public ApplePerson(string nPersonId, string nPersonNumber, string nFirstName, string nMiddleName, string nLastName, string nEmailAddress, string nSisUsername, string nLocationId)
        {
            this.personId = nPersonId;
            this.personNumber = nPersonNumber;
            this.firstName = nFirstName;
            this.middleName = nMiddleName;
            this.lastName = nLastName;
            this.emailAddress = nEmailAddress;
            this.sisUsername = nSisUsername;
            this.locationId = nLocationId;
        }
    }
}
