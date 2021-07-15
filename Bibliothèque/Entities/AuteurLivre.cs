using System;
using System.Collections.Generic;

#nullable disable

namespace Bibliothèque.Entities
{
    public partial class AuteurLivre
    {
        public int IdLivre { get; set; }
        public int IdAuteur { get; set; }

        public virtual Auteur IdAuteurNavigation { get; set; }
        public virtual Livre IdLivreNavigation { get; set; }
    }
}
