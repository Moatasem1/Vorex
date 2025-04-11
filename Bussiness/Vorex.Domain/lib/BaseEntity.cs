using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vorex.Domain.Common;

public abstract class BaseEntity
{
    [Key]
    [Required]
    public Guid Id { get; protected set; } = Guid.NewGuid();
    [Required]
    public DateTime CreatedAt { get; protected set; } = DateTime.UtcNow;
}