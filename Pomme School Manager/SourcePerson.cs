using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pomme_School_Manager
{
    class SourcePerson
    {
        private string name;
        private string firstName;
        private string mail;
        private string username;

        //Assesseurs
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }

        public string Mail
        {
            get { return mail; }
            set { mail = value; }
        }

        public string Username
        {
            get { return username; }
            set { username = value; }
        }


        /*
         * Constructeur
         */
        public SourcePerson(string nName, string nFirstName, string nMail, string nUsername)
        {
            this.name = nName;
            this.firstName = nFirstName;
            this.mail = nMail;
            this.username = nUsername;
        }


    }
}
