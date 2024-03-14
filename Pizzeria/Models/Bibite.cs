namespace Pizzeria.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Bibite")]
    public partial class Bibite
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Bibite()
        {
            Ordini = new HashSet<Ordini>();
        }

        [Key]
        public int ID_Bibita { get; set; }

        [Required]
        [StringLength(50)]
        public string Nome { get; set; }

        public decimal Prezzo { get; set; }

        public string Immagine { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ordini> Ordini { get; set; }
    }
}
