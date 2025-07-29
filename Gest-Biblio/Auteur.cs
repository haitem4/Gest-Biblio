using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gest_Biblio
{
    class Auteur
    {
        private  int Id_Auteur;

        

        private string NomA;
        private string PrenomA;
        private DateTime Date_Naiss;
        private string Nationalite;


        public Auteur(string id_Auteur, string nomA, string prenomA, string date_Naiss, string nationalite)
        {
            Id_Auteur = int.Parse(id_Auteur);
            NomA = nomA;
            PrenomA = prenomA;
            Date_Naiss = DateTime.Parse( date_Naiss);
            Nationalite = nationalite;
        }
        public int Id_Auteur1 { get => Id_Auteur; set => Id_Auteur = value; }
        public string NomA1 { get => NomA; set => NomA = value; }
        public string PrenomA1 { get => PrenomA; set => PrenomA = value; }
        public DateTime Date_Naiss1 { get => Date_Naiss; set => Date_Naiss = value; }
        public string Nationalite1 { get => Nationalite; set => Nationalite = value; }
    }
}
