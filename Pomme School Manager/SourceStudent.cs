using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pomme_School_Manager
{
    class SourceStudent : SourcePerson
    {
        private string classe;

        //Assesseurs
        public string Classe
        {
            get { return classe; }
            set {classe = value; }
        }

        /**
         * Constructeur
         */
        public SourceStudent(string nLastName, string nFirstName, string nClasse, string nEmailAddress, string nId)
           : base(nLastName, nFirstName, nEmailAddress, nId)
        {
            this.classe = nClasse;
        }
            
    }
}
