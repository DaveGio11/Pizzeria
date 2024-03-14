namespace Pizzeria.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Ordini")]
    public partial class Ordini
    {
        [Key]
        public int ID_Ordine { get; set; }

        public int FK_ID_Pizza { get; set; }

        public int? FK_ID_Bibita { get; set; }

        public int FK_ID_Utente { get; set; }

        [Required]
        public string Indirizzo_Consegna { get; set; }

        public int Quantita { get; set; }

        public string Note { get; set; }

        public decimal Totale { get; set; }

        public virtual Bibite Bibite { get; set; }

        public virtual Users Users { get; set; }

        public virtual Pizze Pizze { get; set; }

        public virtual Users Users1 { get; set; }
    }
}
