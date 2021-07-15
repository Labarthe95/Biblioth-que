using System;
using System.Collections.Generic;

#nullable disable

namespace Bibliothèque.Entities
{
    public partial class Livre
    {
        public Livre()
        {
            AuteurLivres = new HashSet<AuteurLivre>();
        }

        public int Id { get; set; }
        public string Titre { get; set; }
        public int DateParution { get; set; }
        public string Editeur { get; set; }
        public int NbPages { get; set; }
        public string Serie { get; set; }
        public int? Tome { get; set; }
        public string Isbn { get; set; }

        public virtual ICollection<AuteurLivre> AuteurLivres { get; set; }
    }
}
