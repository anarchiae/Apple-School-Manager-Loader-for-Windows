using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace Pomme_School_Manager
{
    public partial class Form1 : Form
    {
        String oldAppleClassesFilePath = null;
        String oldAppleCoursesFilePath = null;
        String oldAppleLocationsFilePath = null;
        String oldAppleRostersFilePath = null;
        String oldAppleStaffFilePath = null;
        String oldAppleStudentsFilePath = null;

        String sourceStaffFilePath = null;
        String sourceStudentsFilePath = null;
        String sourceClassesFilePath = null;

        char oldSeparator;
        char newSeparator;

        int addedStudents = 0;
        int removedStudents = 0;
        int addedStaff = 0;
        int removedStaff = 0;

        List<AppleClasse> oldAppleClasses;
        List<AppleCourse> oldAppleCourses;
        List<AppleLocation> oldAppleLocations;
        List<ApplePerson> oldAppleStaff;
        List<AppleStudent> oldAppleStudents;
        List<AppleRoster> oldAppleRosters;

        List<SourceClasse> sourceClasses;
        List<SourcePerson> sourceStaff;
        List<SourceStudent> sourceStudents;

        List<AppleClasse> newAppleClasses;
        List<AppleCourse> newAppleCourses;
        List<AppleLocation> newAppleLocations;
        List<ApplePerson> newAppleStaff;
        List<AppleStudent> newAppleStudents;
        List<AppleRoster> newAppleRosters;

        public Form1()
        {
            InitializeComponent();
        }

        /**
         * Sélectionne les anciens fichiers
         */
        private void oldFilesButton_Click(object sender, EventArgs e)
        {
            if(folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                String oldFilesFolder = folderBrowserDialog.SelectedPath;
                oldAppleClassesFilePath = ASMTools.createPath(oldFilesFolder, "classes.csv");
                oldAppleCoursesFilePath = ASMTools.createPath(oldFilesFolder, "courses.csv");
                oldAppleLocationsFilePath = ASMTools.createPath(oldFilesFolder, "locations.csv");
                oldAppleRostersFilePath = ASMTools.createPath(oldFilesFolder, "rosters.csv");
                oldAppleStaffFilePath = ASMTools.createPath(oldFilesFolder, "staff.csv");
                oldAppleStudentsFilePath = ASMTools.createPath(oldFilesFolder, "students.csv");
            }
        }

        /**
         * Sélectionne le nouveau fichier staff
         */
        private void newStaffButton_Click(object sender, EventArgs e)
        {
            if(openFileDialog.ShowDialog() == DialogResult.OK)
            {
                sourceStaffFilePath = openFileDialog.FileName;
            }
        }


        /**
         * Sélectionne le nouveau fichier students
         */
        private void newStudentsButton_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                sourceStudentsFilePath = openFileDialog.FileName;
            }
        }

        /**
         * Sélectionne le nouveau fichier classes
         */
        private void newClassesButton_Click(object sender, EventArgs e)
        {
            if(openFileDialog.ShowDialog() == DialogResult.OK)
            {
                sourceClassesFilePath = openFileDialog.FileName;
            }
        }





        /**
         * Charge les fichiers sélectionnés
         */
        private void loadFilesButton_Click(object sender, EventArgs e)
        {
            //Vérifie que tous les fichiers ont bien été sélectionnés avant le début de la conversion
            if (oldAppleClassesFilePath == null || oldAppleCoursesFilePath == null || oldAppleLocationsFilePath == null || oldAppleRostersFilePath == null || oldAppleStaffFilePath == null || oldAppleStudentsFilePath == null || sourceStaffFilePath == null || sourceStudentsFilePath == null || sourceClassesFilePath == null)
            {
                MessageBox.Show("Tous les fichiers n'ont pas été sélectionnés. Vérifiez puis recommencez.", "Fichiers manquants", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                //Vérifie la valeur des séparateurs
                oldSeparator = oldSeparatorTextBox.Text[0];
                newSeparator = newSeparatorTextBox.Text[0];

                //Crée les Apple classes
                ArrayList classes = ASMTools.parseCSVFiles(oldAppleClassesFilePath, oldSeparator);
                oldAppleClasses = new List<AppleClasse>();
                try
                {
                    foreach (string[] aClass in classes)
                    {
                        AppleClasse newClass = new AppleClasse(aClass[0].Replace("\"", ""), aClass[1].Replace("\"", ""), aClass[2].Replace("\"", ""));
                        //Ajoute des professeurs
                        if (aClass.Length > 3)
                        {
                            for (int i = 3; i < aClass.Length; i++)
                            {
                                newClass.AddInstructor(aClass[i].Replace("\"", ""));
                            }
                        }

                        oldAppleClasses.Add(newClass);
                    }

                    statusProgressBar.Increment(16);
                    statusLabel.Text = "Classes ajoutées";
                }
                catch (IndexOutOfRangeException)
                {
                    MessageBox.Show("Impossible de lire le fichier. Vérifiez le séparateur", "IndexOutOfRangeException", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }


                //Crée les Apple courses
                ArrayList courses = ASMTools.parseCSVFiles(oldAppleCoursesFilePath, oldSeparator);
                oldAppleCourses = new List<AppleCourse>();
                try
                {
                    foreach (string[] aCourse in courses)
                    {
                        AppleCourse newCourse = new AppleCourse(aCourse[0].Replace("\"", ""), aCourse[1].Replace("\"", ""), aCourse[2].Replace("\"", ""), aCourse[3].Replace("\"", ""));
                        oldAppleCourses.Add(newCourse);
                    }

                    statusProgressBar.Increment(16);
                    statusLabel.Text = "Cours ajoutés";
                }
                catch (IndexOutOfRangeException)
                {
                    MessageBox.Show("Impossible de lire le fichier. Vérifiez le séparateur", "IndexOutOfRangeException", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                //Crée les Apple locations
                ArrayList locations = ASMTools.parseCSVFiles(oldAppleLocationsFilePath, oldSeparator);
                oldAppleLocations = new List<AppleLocation>();
                try
                {
                    foreach (string[] aLocation in locations)
                    {
                        AppleLocation newLocation = new AppleLocation(aLocation[0].Replace("\"", ""), aLocation[1].Replace("\"", ""));
                        oldAppleLocations.Add(newLocation);
                    }

                    statusProgressBar.Increment(16);
                    statusLabel.Text = "Locations ajoutées";
                }
                catch (IndexOutOfRangeException)
                {
                    MessageBox.Show("Impossible de lire le fichier. Vérifiez le séparateur", "IndexOutOfRangeException", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }


                //Crée les Apple professeurs
                ArrayList staff = ASMTools.parseCSVFiles(oldAppleStaffFilePath, oldSeparator);
                oldAppleStaff = new List<ApplePerson>();
                try
                {
                    foreach (string[] aStaff in staff)
                    {
                        ApplePerson newStaff = new ApplePerson(aStaff[0].Replace("\"", ""), aStaff[1].Replace("\"", ""), aStaff[2].Replace("\"", ""), aStaff[3].Replace("\"", ""), aStaff[4].Replace("\"", ""), aStaff[5].Replace("\"", ""), aStaff[6].Replace("\"", ""), aStaff[7].Replace("\"", ""));
                        oldAppleStaff.Add(newStaff);
                    }

                    statusProgressBar.Increment(16);
                    statusLabel.Text = "Professeurs ajoutés";
                }
                catch (IndexOutOfRangeException)
                {
                    MessageBox.Show("Impossible de lire le fichier. Vérifiez le séparateur", "IndexOutOfRangeException", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }


                //Crée les Apple rosters
                ArrayList rosters = ASMTools.parseCSVFiles(oldAppleRostersFilePath, oldSeparator);
                oldAppleRosters = new List<AppleRoster>();
                try
                {
                    foreach (string[] aRoster in rosters)
                    {
                        AppleRoster newRoster = new AppleRoster(aRoster[0].Replace("\"", ""), aRoster[1].Replace("\"", ""), aRoster[2].Replace("\"", ""));
                        oldAppleRosters.Add(newRoster);
                    }

                    statusProgressBar.Increment(16);
                    statusLabel.Text = "Rosters ajoutés";
                }
                catch (IndexOutOfRangeException)
                {
                    MessageBox.Show("Impossible de lire le fichier. Vérifiez le séparateur", "IndexOutOfRangeException", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }


                //Crée les Apple students
                ArrayList students = ASMTools.parseCSVFiles(oldAppleStudentsFilePath, oldSeparator);
                oldAppleStudents = new List<AppleStudent>();
                try
                {
                    foreach (string[] aStudent in students)
                    {
                        AppleStudent newStudent = new AppleStudent(aStudent[0].Replace("\"", ""), aStudent[1].Replace("\"", ""), aStudent[2].Replace("\"", ""), aStudent[3].Replace("\"", ""), aStudent[4].Replace("\"", ""), aStudent[5].Replace("\"", ""), aStudent[6].Replace("\"", ""), aStudent[7].Replace("\"", ""), aStudent[8].Replace("\"", ""), aStudent[9].Replace("\"", ""));
                        oldAppleStudents.Add(newStudent);
                    }

                    statusProgressBar.Value = 100;
                    statusLabel.Text = "Fichiers d'origine Apple importés";
                }
                catch (IndexOutOfRangeException)
                {
                    MessageBox.Show("Impossible de lire le fichier. Vérifiez le séparateur", "IndexOutOfRangeException", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }




                //Crée les classes sources(Saint Jean)
                classes = ASMTools.parseCSVFiles(sourceClassesFilePath, newSeparator);
                sourceClasses = new List<SourceClasse>();
                try
                {
                    foreach (string[] aClasse in classes)
                    {
                        SourceClasse newClasse = new SourceClasse(aClasse[0].Replace("\"", ""));
                        //Ajoute les professeurs à la classe
                        if (aClasse.Length > 1)
                        {
                            for (int i = 1; i < aClasse.Length; i++)
                            {
                                newClasse.addInstructor(aClasse[i].Replace("\"", ""));
                            }
                        }

                        sourceClasses.Add(newClasse);

                        statusProgressBar.Value = 0;
                        statusProgressBar.Increment(33);
                        statusLabel.Text = "Fichier source des classes importé";
                    }
                }
                catch (IndexOutOfRangeException)
                {
                    MessageBox.Show("Impossible de lire le fichier. Vérifiez le séparateur", "IndexOutOfRangeException", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }


                //Crée les professeurs sources(Saint Jean)
                staff = ASMTools.parseCSVFiles(sourceStaffFilePath, newSeparator);
                sourceStaff = new List<SourcePerson>();
                try
                {
                    foreach (string[] aStaff in staff)
                    {
                        SourcePerson newStaff = new SourcePerson(aStaff[0].Replace("\"", ""), aStaff[1].Replace("\"", ""), aStaff[2].Replace("\"", ""), aStaff[3].Replace("\"", ""));
                        sourceStaff.Add(newStaff);
                    }

                    statusProgressBar.Increment(33);
                    statusLabel.Text = "Fichier source des professeurs importé";
                }
                catch (IndexOutOfRangeException)
                {
                    MessageBox.Show("Impossible de lire le fichier. Vérifiez le séparateur", "IndexOutOfRangeException", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }


                //Crée les étudiants source (Saint Jean)
                students = ASMTools.parseCSVFiles(sourceStudentsFilePath, newSeparator);
                sourceStudents = new List<SourceStudent>();
                try
                {
                    foreach (string[] aStudent in students)
                    {
                        SourceStudent newStudent = new SourceStudent(aStudent[0].Replace("\"", ""), aStudent[1].Replace("\"", ""), aStudent[2].Replace("\"", ""), aStudent[3].Replace("\"", ""), aStudent[4].Replace("\"", ""));
                        sourceStudents.Add(newStudent);
                    }

                    statusProgressBar.Value = 100;
                    statusLabel.Text = "Fichier source des élèves importé";

                }
                catch (IndexOutOfRangeException)
                {
                    MessageBox.Show("Impossible de lire le fichier. Vérifiez le séparateur", "IndexOutOfRangeException", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                //===================================================================
                // GENERATION DES NOUVEAUX FICHIERS
                //===================================================================

                statusProgressBar.Value = 0; //Rénitialise la barre de progression
                statusLabel.Text = "Création des nouveaux fichiers Apple";

                //CREATION DU NOUVEAU FICHIER STAFF APPLE
                //1. N'ajoute que les professeurs présents l'année précédente et cette année
                bool staffHasBeenRemoved;
                int lastRemovedStaffId;
                newAppleStaff = new List<ApplePerson>();
                foreach (ApplePerson anAppleStaff in oldAppleStaff)
                {
                    if (anAppleStaff.FirstName != "first_name")
                    {
                        staffHasBeenRemoved = true;

                        foreach (SourcePerson aSourceStaff in sourceStaff)
                        {
                            if (anAppleStaff.LastName.ToLower() == aSourceStaff.Name.ToLower() && anAppleStaff.EmailAddress.ToLower() == aSourceStaff.Mail.ToLower())
                            {
                                newAppleStaff.Add(anAppleStaff);
                                staffHasBeenRemoved = false;
                            }
                        }

                        if (staffHasBeenRemoved)
                        {
                            lastRemovedStaffId = Convert.ToInt32(anAppleStaff.PersonNumber.Split('-')[1]);
                            if (lastRemovedStaffId > Properties.Settings.Default.highestRemovedStaffId)
                            {
                                Properties.Settings.Default.highestRemovedStaffId = lastRemovedStaffId;
                            }
                            removedStaff++;
                        }
                    }
                }

                removedStaffValueLabel.Text = removedStaff.ToString(); //Affiche le nombre de professeurs supprimés
                statusProgressBar.Increment(10); //Etape terminée incrémente la progresse bar de 10.


                //2. Ajoute les professeurs présents dans le fichier staff source et absents de l'ancien fichier Staff apple
                //2.a Récupère l'identifiant le plus grand
                int personNumber; //Numéro de l'identifiant de la personne
                int personNumberMax = 0; //Plus grand numéro
                foreach (ApplePerson anAppleStaff in newAppleStaff)
                {
                    personNumber = Convert.ToInt32(anAppleStaff.PersonNumber.Split('-')[1]); //Converti le numéro en entier (int)
                    if (personNumber > personNumberMax) 
                    {
                        personNumberMax = personNumber;
                    }
                }

                statusProgressBar.Increment(10); //Première partie de la deuxième étape terminée on incrémente la barre de progression de 10

                //2.b Ajoute les professeurs
                bool notInOldAppleStaff = true;
                //Ici nous recherchons les nouveaux professeurs (présents dans source mais pas dans les fichiers Apple) 
                foreach (SourcePerson aSourceStaff in sourceStaff)
                {
                    if (aSourceStaff.FirstName != "Prénom")
                    {
                        foreach (ApplePerson anApplePerson in oldAppleStaff)
                        {
                            if (aSourceStaff.Name == anApplePerson.LastName && aSourceStaff.Mail == anApplePerson.EmailAddress && aSourceStaff.Username == anApplePerson.SisUsername)
                            {
                                notInOldAppleStaff = false;
                            }
                        }

                        //Si le professeur n'est pas dans l'ancien fichier Apple
                        if (notInOldAppleStaff)
                        {
                            //Crée le nouvel identifiant
                            //Vérifier que l'identifiant n'a pas été utilisé précédemment
                            do
                            {
                                personNumberMax++;
                            }
                            while (personNumberMax <= Properties.Settings.Default.highestRemovedStaffId);

                            ApplePerson newStaff = new ApplePerson("ID-Professeur-" + personNumberMax.ToString(), "PROF-" + personNumberMax.ToString(), aSourceStaff.FirstName, "", aSourceStaff.Name, aSourceStaff.Mail, aSourceStaff.Username, "ID-Location-College-Saint_Jean");
                            newAppleStaff.Add(newStaff);
                            addedStaff++;
                        }

                        notInOldAppleStaff = true; //Réinitialise
                    }

                }

                //Affiche les professeurs dans la staffDataGridView
                foreach (ApplePerson anApplePerson in newAppleStaff)
                {
                    staffDataGridView.Rows.Add(anApplePerson.PersonId, anApplePerson.PersonNumber, anApplePerson.FirstName, anApplePerson.MiddleName, anApplePerson.LastName, anApplePerson.EmailAddress, anApplePerson.SisUsername, anApplePerson.LocationId);
                }

                addedStaffValueLabel.Text = addedStaff.ToString(); //On affiche le nombre de professeurs ajoutés
                statusProgressBar.Increment(10); //Deuxième étape terminée on incrémente la barre de progression de 10




                //CREATION D'UN NOUVEAU FICHIER STUDENT APPLE
                //1. N'ajoute que les élèves présents l'année précédente et cette année
                bool studentHasBeenRemoved;
                int lastRemovedStudentId;
                newAppleStudents = new List<AppleStudent>();
                foreach (AppleStudent anAppleStudent in oldAppleStudents)
                {
                    if (anAppleStudent.FirstName != "first_name")
                    {
                        studentHasBeenRemoved = true;

                        foreach (SourceStudent aSourceStudent in sourceStudents)
                        {
                            if(anAppleStudent.LastName.ToLower() == aSourceStudent.Name.ToLower() && anAppleStudent.EmailAddress.ToLower() == aSourceStudent.Mail.ToLower())
                            {
                                anAppleStudent.GradeLevel = aSourceStudent.Classe; //Bug réglé
                                newAppleStudents.Add(anAppleStudent);
                                studentHasBeenRemoved = false;
                            }
                        }

                        if (studentHasBeenRemoved)
                        {
                            try
                            {
                                lastRemovedStudentId = Convert.ToInt32(anAppleStudent.PersonNumber.Split('-')[1]);
                                if (lastRemovedStudentId > Properties.Settings.Default.highestRemovedStudentId)
                                {
                                    Properties.Settings.Default.highestRemovedStudentId = lastRemovedStudentId;
                                }
                                removedStudents++;
                            }
                            catch(IndexOutOfRangeException)
                            {
                                MessageBox.Show("Veuillez vérifier que le person_id et le person_number de l'élève " + anAppleStudent.FirstName + " " + anAppleStudent.LastName + " est au bon format dans le fichier CSV", "IndexOutOfRangeException", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }

                removedStudentsValueLabel.Text = removedStaff.ToString(); //Affiche le nombre de professeurs supprimés
                statusProgressBar.Increment(10); //Etape terminée incrémente la progresse bar de 10.


                //2. Ajoute les élèves présents dans le fichier students source et absents de l'ancien fichier Students apple
                //2.a Récupère l'identifiant le plus grand
                personNumber = 0;
                personNumberMax = 0;
                foreach (AppleStudent anAppleStudent in newAppleStudents)
                {
                    personNumber = Convert.ToInt32(anAppleStudent.PersonNumber.Split('-')[1]);
                    if (personNumber > personNumberMax)
                    {
                        personNumberMax = personNumber;
                    }
                }

                statusProgressBar.Increment(10);

                //2.b Ajoute les professeurs
                bool notInOldAppleStudents = true;

                //Ici nous recherchons les nouveaux élèves (présents dans source mais pas dans les fichiers Apple) 
                foreach (SourceStudent aSourceStudent in sourceStudents)
                {
                    if (aSourceStudent.Name != "Nom")
                    {
                        foreach (AppleStudent anAppleStudent in oldAppleStudents)
                        {
                            if (aSourceStudent.Name == anAppleStudent.LastName && aSourceStudent.Mail == anAppleStudent.EmailAddress && aSourceStudent.Username == anAppleStudent.SisUsername)
                            {
                                notInOldAppleStudents = false;
                            }
                        }

                        //Si l'élève n'est pas dans l'ancien fichier Apple
                        if (notInOldAppleStudents)
                        {
                            //Crée le nouvel identifiant
                            //Vérifier que l'identifiant n'a pas été utilisé précédemment
                            do
                            {
                                personNumberMax++;
                            }
                            while (personNumberMax <= Properties.Settings.Default.highestRemovedStudentId);

                            AppleStudent newStudent = new AppleStudent("ID-ELEVE-" + personNumberMax.ToString(), "ELV-" + personNumberMax.ToString(), aSourceStudent.FirstName, "", aSourceStudent.Name, aSourceStudent.Classe, aSourceStudent.Mail, aSourceStudent.Username, "8", "ID-Location-College-Saint_Jean");
                            newAppleStudents.Add(newStudent);
                            addedStudents++;
                        }

                        notInOldAppleStudents = true; //Réinitialise
                    }
                }

                //Affiche les étudiants dans la staffDataGridView
                foreach (AppleStudent anAppleStudent in newAppleStudents)
                {
                    studentsDataGridView.Rows.Add(anAppleStudent.PersonId, anAppleStudent.PersonNumber, anAppleStudent.FirstName, anAppleStudent.MiddleName, anAppleStudent.LastName, anAppleStudent.GradeLevel, anAppleStudent.EmailAddress, anAppleStudent.SisUsername, anAppleStudent.PasswordPolicy, anAppleStudent.LocationId);
                }


                addedStudentsValueLabel.Text = addedStudents.ToString(); //Affiche le nombre d'étudiants ajoutés
                statusProgressBar.Increment(10); //Etape terminée incrémente de 10


                //CREATION DU NOUVEAU FICHIER APPLE CLASSES
                newAppleClasses = new List<AppleClasse>();
                foreach (SourceClasse aSourceClasse in sourceClasses)
                {
                    if (aSourceClasse.ClassId != "class_number")
                    {
                        AppleClasse newAppleClasse = new AppleClasse("ID-Classe-" + aSourceClasse.ClassId, aSourceClasse.ClassId, "ID-Cours-" + aSourceClasse.ClassId);
                        foreach (string anInstructor in aSourceClasse.Instructors)
                        {
                            newAppleClasse.AddInstructor(anInstructor);
                        }
                        newAppleClasses.Add(newAppleClasse);
                    }
                }

                statusProgressBar.Increment(10);

                //Affichage des classes dans la classesDataGridView
                int maxInstructors = 1;
                foreach(AppleClasse anAppleClasse in newAppleClasses)
                {
                    if(anAppleClasse.Instructors.Count() > maxInstructors)
                    {
                        maxInstructors = anAppleClasse.Instructors.Count();
                    }
                }

                for (int i = 0; i < maxInstructors; i++)
                {
                    DataGridViewColumn newCol = new DataGridViewColumn();
                    newCol.HeaderText = "instructor_id_" + i.ToString();
                    newCol.Name = "InstructorId" + i.ToString();
                    newCol.CellTemplate = new DataGridViewTextBoxCell();
                    classesDataGridView.Columns.Add(newCol);
                }


                foreach (AppleClasse anAppleClasse in newAppleClasses)
                {
                    classesDataGridView.Rows.Add(anAppleClasse.ClassId, anAppleClasse.ClassNumber, anAppleClasse.CourseId, anAppleClasse.Instructors.ToArray());
                }


                //CREATION DU FICHIER COURSES
                newAppleCourses = new List<AppleCourse>();
                foreach(AppleClasse anAppleClasse in newAppleClasses)
                {
                    AppleCourse newAppleCourse = new AppleCourse(anAppleClasse.CourseId, "", "", "ID-Location-College-Saint_Jean");
                    newAppleCourses.Add(newAppleCourse);
                }

                //Affiche les cours dans courseDataGridView
                foreach (AppleCourse anAppleCourse in newAppleCourses)
                {
                    coursesDataGridView.Rows.Add(anAppleCourse.CourseId, anAppleCourse.CourseNumber, anAppleCourse.CourseName, anAppleCourse.LocationId);
                }
                statusProgressBar.Increment(10);


                //CREATION DU FICHIER APPLE ROSTERS
                newAppleRosters = new List<AppleRoster>();
                int rosterNumber = 1; //Numéro du roster
                foreach (SourceStudent aSourceStudent in sourceStudents)
                {
                    foreach(AppleStudent anAppleStudent in newAppleStudents)
                    {
                        if(anAppleStudent.FirstName == aSourceStudent.FirstName && anAppleStudent.LastName == aSourceStudent.Name && anAppleStudent.EmailAddress == aSourceStudent.Mail && anAppleStudent.SisUsername == aSourceStudent.Username)
                        {
                            AppleRoster newAppleRoster = new AppleRoster("SAMPLE-ROSTER-ID-" + rosterNumber, "ID-Classe-" + aSourceStudent.Classe, anAppleStudent.PersonId);
                            newAppleRosters.Add(newAppleRoster);
                            rosterNumber++; //Incrémente
                        }
                    }
                }

                statusProgressBar.Increment(10);

                //Affiches les rosters dans le rostersDataGridView
                foreach(AppleRoster anAppleRoster in newAppleRosters)
                {
                    rostersDataGridView.Rows.Add(anAppleRoster.RosterId, anAppleRoster.ClassId, anAppleRoster.StudentId);
                }


                //CREATION DU NOUVEAU FICHIER APPLE LOCATIONS
                newAppleLocations = new List<AppleLocation>();
                foreach (AppleLocation anOldAppleLocation in oldAppleLocations)
                {
                    if (anOldAppleLocation.LocationId != "location_id")
                    {
                        newAppleLocations.Add(anOldAppleLocation);
                    }
                }
                //Affiche les locations dans la locationsDataGridView
                foreach(AppleLocation anAppleLocation in newAppleLocations)
                {
                    locationsDataGridView.Rows.Add(anAppleLocation.LocationId, anAppleLocation.LocationNumber);
                }

                statusProgressBar.Value = 0;
                statusLabel.Text = "Fichiers créés. Prêt pour l'exportation";

                //Affiche le nombres d'élèves et de professeurs ajoutés et supprimés
                addedStudentsValueLabel.Text = addedStudents.ToString();
                removedStudentsValueLabel.Text = removedStudents.ToString();
                addedStaffValueLabel.Text = addedStaff.ToString();
                removedStaffValueLabel.Text = removedStaff.ToString();


                loadFilesButton.Enabled = false; //Empêche d'appuyer à nouveau sur le bouton
                exportButton.Enabled = true;
            }
        }


        //Appuie sur le bouton reset
        private void resetButton_Click(object sender, EventArgs e)
        {
            classesDataGridView.Rows.Clear();
            coursesDataGridView.Rows.Clear();
            locationsDataGridView.Rows.Clear();
            staffDataGridView.Rows.Clear();
            rostersDataGridView.Rows.Clear();
            studentsDataGridView.Rows.Clear();

            addedStudents = 0;
            removedStudents = 0;
            addedStaff = 0;
            removedStudents = 0;

            addedStudentsValueLabel.Text = "0";
            removedStudentsValueLabel.Text = "0";
            addedStaffValueLabel.Text = "0";
            removedStaffValueLabel.Text = "0";

            loadFilesButton.Enabled = true;
        }




        private void exportButton_Click(object sender, EventArgs e)
        {
            if(folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                //Récupère le chemin du répertoire où seront exportés les fichiers
                string basePath = folderBrowserDialog.SelectedPath;
                string folderPath = basePath + @"\asm_export_" + DateTime.Now.ToString("yyyy");
                string originalFolderPath = folderPath;

                int folderWithTheSameName = 1;
                while(Directory.Exists(folderPath))
                {
                    folderPath = originalFolderPath + '_' + folderWithTheSameName.ToString();
                    folderWithTheSameName++;
                }

                //Crée le répertoire
                Console.WriteLine(folderPath);

                DirectoryInfo di = Directory.CreateDirectory(folderPath);

                //Sauvegarde les identifiants les plus grands supprimés
                Properties.Settings.Default.Save();

                string newLine = null;

                //CREATION DU FICHIER CLASSES.CSV
                string classesDotCsvPath = folderPath + @"\classes.csv";
                StringBuilder classesDotCsv = new StringBuilder();

                //Calcule le nombre maximum utilisé par une classe
                int maxInstructors = 1;
                foreach (AppleClasse anAppleClasse in newAppleClasses)
                {
                    if (anAppleClasse.Instructors.Count() > maxInstructors)
                    {
                        maxInstructors = anAppleClasse.Instructors.Count();
                    }
                }

                //Ajoute la première ligne
                newLine = '"' + "class_id" + '"' + ',' + '"' + "class_number" + '"' + ',' + '"' + "course_id" + '"';
                for (int i = 0; i < maxInstructors; i++)
                {
                    newLine = newLine + ',' + '"' + "instructor_id_" + i + '"';
                }
                newLine = newLine + ',' + '"' + "location_id" + '"';
                classesDotCsv.AppendLine(newLine);

                //Ajoute toutes les autres lignes
                foreach (AppleClasse anAppleClasse in newAppleClasses)
                {
                    newLine = '"' + anAppleClasse.ClassId + '"' + ',' + '"' + anAppleClasse.ClassNumber + '"' + ',' + '"' + anAppleClasse.CourseId + '"';
                    foreach (string anInstructor in anAppleClasse.Instructors)
                    {
                        newLine = newLine + ',' + '"' + anInstructor + '"';
                    }

                    newLine = newLine + ',' + '"' + "ID-Location-College-Saint_Jean" + '"';

                    classesDotCsv.AppendLine(newLine);
                }

                //Génére le fichier
                File.AppendAllText(classesDotCsvPath, classesDotCsv.ToString());


                //CREATION DU FICHIER COURSES
                string coursesDotCsvPath = folderPath + @"\courses.csv";
                StringBuilder coursesDotCsv = new StringBuilder();

                //Ajoute la première ligne
                newLine = '"' + "course_id" + '"' + ',' + '"' + "course_num" + '"' + ',' + '"' + "course_name" + '"' + ',' + '"' + "location_id" + '"';
                coursesDotCsv.AppendLine(newLine);

                //Ajoute les données
                foreach(AppleCourse anAppleCourse in newAppleCourses)
                {
                    newLine = '"' + anAppleCourse.CourseId + '"' + ',' + '"' + anAppleCourse.CourseNumber + '"' + ',' + '"' + anAppleCourse.CourseName + '"' + ',' + '"' + anAppleCourse.LocationId + '"';
                    coursesDotCsv.AppendLine(newLine);
                }

                //Génére le fichier
                File.AppendAllText(coursesDotCsvPath, coursesDotCsv.ToString());


                //CREATION DU FICHIER LOCATIONS
                string locationsDotCsvPath = folderPath + @"\locations.csv";
                StringBuilder locationsDotCsv = new StringBuilder();

                //Ajoute la première ligne
                newLine = '"' + "location_id" + '"' + ',' + '"' + "location_name";
                locationsDotCsv.AppendLine(newLine);

                //Ajoute la location Saint Jean en dur (à voir si ça va plus loin un jour)
                newLine = '"' + "ID-Location-College-Saint_Jean" + '"' + ',' + '"' + "College Saint Jean";
                locationsDotCsv.AppendLine(newLine);

                //Génére le fichier
                File.AppendAllText(locationsDotCsvPath, locationsDotCsv.ToString());


                //CREATION DU FICHIER ROSTERS
                string rostersDotCsvPath = folderPath + @"\rosters.csv";
                StringBuilder rostersDotCsv = new StringBuilder();

                //Ajoute la première ligne
                newLine = '"' + "roster_id" + '"' + ',' + '"' + "class_id" + '"' + ',' + '"' + "student_id";
                rostersDotCsv.AppendLine(newLine);

                //Ajoute les rosters
                foreach(AppleRoster anAppleRoster in newAppleRosters)
                {
                    newLine = '"' + anAppleRoster.RosterId + '"' + ',' + '"' + anAppleRoster.ClassId + '"' + ',' + '"' + anAppleRoster.StudentId + '"';
                    rostersDotCsv.AppendLine(newLine);
                }

                //Génére le fichier
                File.AppendAllText(rostersDotCsvPath, rostersDotCsv.ToString());



                //CREATION DU FICHIER STAFF
                string staffDotCsvPath = folderPath + @"\staff.csv";
                StringBuilder staffDotCsv = new StringBuilder();

                //Ajoute la première ligne
                newLine = '"' + "person_id" + '"' + ',' + '"' + "person_number" + '"' + ',' + '"' + "first_name" + '"' + ',' + '"' + "middle_name" + '"' + ',' + '"' + "last_name" + '"' + ',' + '"' + "email_address" + '"' + ',' + '"' + "sis_username" + '"' + ',' + '"' + "location_id" + '"';
                staffDotCsv.AppendLine(newLine);

                //Ajoute les professeurs
                foreach(ApplePerson anAppleStaff in newAppleStaff)
                {
                    newLine = '"' + anAppleStaff.PersonId + '"' + ',' + '"' + anAppleStaff.PersonNumber + '"' + ',' + '"' + anAppleStaff.FirstName + '"' + ',' + '"' + anAppleStaff.MiddleName + '"' + ',' + '"' + anAppleStaff.LastName + '"' + ',' + '"' + anAppleStaff.EmailAddress + '"' + ',' + '"' + anAppleStaff.SisUsername + '"' + ',' + '"' + anAppleStaff.LocationId + '"';
                    staffDotCsv.AppendLine(newLine);
                }

                //Génére le fichier
                File.AppendAllText(staffDotCsvPath, staffDotCsv.ToString());


                //CREATION DU FICHIER STUDENTS
                string studentsDotCsvPath = folderPath + @"\students.csv";
                StringBuilder studentsDotCsv = new StringBuilder();

                //Ajoute la première ligne
                newLine = '"' + "person_id" + '"' + ',' + '"' + "person_number" + '"' + ',' + '"' + "first_name" + '"' + ',' + '"' + "middle_name" + '"' + ',' + '"' + "last_name" + '"' + ',' + '"' + "grade_level" + '"' + ',' + '"' + "email_address" + '"' + ',' + '"' + "sis_username" + '"' + ',' + '"' + "password_policy" + '"' + ',' + '"' + "location_id" + '"';
                studentsDotCsv.AppendLine(newLine);

                //Ajoute les élèves
                foreach(AppleStudent anAppleStudent in newAppleStudents)
                {
                    newLine = '"' + anAppleStudent.PersonId + '"' + ',' + '"' + anAppleStudent.PersonNumber + '"' + ',' + '"' + anAppleStudent.FirstName + '"' + ',' + '"' + anAppleStudent.MiddleName + '"' + ',' + '"' + anAppleStudent.LastName + '"' + ',' + '"' + anAppleStudent.GradeLevel + '"' + ',' + '"' + anAppleStudent.EmailAddress + '"' + ',' + '"' + anAppleStudent.SisUsername + '"' + ',' + '"' + anAppleStudent.PasswordPolicy + '"' + ',' + '"' + anAppleStudent.LocationId + '"';
                    studentsDotCsv.AppendLine(newLine);
                }

                //Génére le fichier
                File.AppendAllText(studentsDotCsvPath, studentsDotCsv.ToString());

                MessageBox.Show("L'exportation des fichiers est terminée !", "Exportation terminée", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Process.Start(basePath);

            }
        }
    }
}
