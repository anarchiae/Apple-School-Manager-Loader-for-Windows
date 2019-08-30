using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Pomme_School_Manager
{
    class ASMTools
    {
        /**
         *Crée les chemins des fichiers nécessaires
         *Vérifie si les fichiers existents
         */
        public static String createPath(String basePath, String fileName)
        {
            String newPath = null;

            if (File.Exists(basePath + '\\' + fileName))
            {
                newPath = basePath + '\\' + fileName;
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Le fichier " + fileName + " est introuvable. Vérifiez qu'il se trouve bien dans le même répertoire que le fichier sélectionné et recommencez.", "Fichier manquant", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
            }

            return newPath;
        }


        /**
         * Lit un fichier CSV donné est le converti en ArrayList de String[]
         * A partir du ArrayList sorti, il faut utiliser les données pour créer un objet (student, staff, roster, etc...)
         */
        public static ArrayList parseCSVFiles(String fileName, char separator)
        {
            String[] aLine;
            ArrayList parsedValues = new ArrayList();

            //Lecture du fichier CSV
            try
            {
                StreamReader sr = new StreamReader(fileName);
                while (!sr.EndOfStream)
                {
                    aLine = sr.ReadLine().Split(separator);
                    Console.WriteLine(aLine.Length.ToString());
                    parsedValues.Add(aLine);
                }
            }
            catch (IOException)
            {
                System.Windows.Forms.MessageBox.Show("Ce fichier ne peut pas être importé car il semble être utilisé par une autre application", "IOException", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

            return parsedValues;
        }
    }
}
