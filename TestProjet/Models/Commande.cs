//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TestProjet.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Commande
    {
        public int id { get; set; }
        public int id_utilisateur { get; set; }
        public string etat { get; set; }
        public string date_commande { get; set; }
        public string date_livraison { get; set; }
        public string date_expedition { get; set; }
        public int id_livraison { get; set; }
        public string fret { get; set; }
        public int id_methode_livraison { get; set; }
    }
}
