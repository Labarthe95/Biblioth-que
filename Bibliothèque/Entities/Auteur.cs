using System;
using System.Collections.Generic;

#nullable disable

namespace Bibliothèque.Entities
{
    public partial class Auteur
    {
        public Auteur()
        {
            AuteurLivres = new HashSet<AuteurLivre>();
        }

        public int Id { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Pseudonyme { get; set; }

        public virtual ICollection<AuteurLivre> AuteurLivres { get; set; }
    }
}
