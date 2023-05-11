using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Case.Entity.Domains;

public abstract class BaseEntity
{
    protected BaseEntity()
    {
        CreatedDate = DateTime.Now;
        UpdatedDate = DateTime.Now; 
    }
    
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; } 
    public DateTime CreatedDate { get; set; }
    public long? CreateUserId { get; set; }
    public DateTime UpdatedDate { get; set; }
    public long? UpdateUserId { get; set; } 
    public bool IsDeleted { get; set; }    
    public DateTime? DeletedDate { get; set; }
    public long? DeleteUserId { get; set; }
}