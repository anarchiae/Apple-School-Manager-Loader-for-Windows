using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pomme_School_Manager
{
    class SourceClasse
    {
        private string classId;
        private List<string> instructors;


        //Assesseurs
        public string ClassId
        {
            get { return classId; }
            set { classId = value; }
        }

        public List<string> Instructors
        {
            get { return instructors; }
        }

        /**
         * Constructeur
         */
        public SourceClasse(string nClassId)
        {
            this.classId = nClassId;
            instructors = new List<string>();
        }

        /*
         * Ajoute un professeur à la liste des professeurs
         */
        public void addInstructor(string instructorId)
        {
            instructors.Add(instructorId);
        }
    }
}
