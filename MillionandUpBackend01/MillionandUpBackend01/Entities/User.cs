using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace MillionandUpBackend01.Entities {
    public class User : BaseEntity {

        [Column(TypeName = "varchar(50)")]
        public string? Username { get; set; }
    }
}
