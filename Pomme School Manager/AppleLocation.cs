using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pomme_School_Manager
{
    class AppleLocation
    {
        private string locationId;
        private string locationNumber;

        //Assesseurs
        public string LocationId
        {
            get { return locationId; }
            set { locationId = value; }
        }

        public string LocationNumber
        {
            get { return locationNumber; }
            set { locationNumber = value; }
        }

        /***
         * Constructeur
         */
        public AppleLocation(string nLocationId, string nLocationNumber)
        {
            this.locationId = nLocationId;
            this.locationNumber = nLocationNumber;
        }
    }
}
