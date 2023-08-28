using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdentityLogin.Models;
[Table("Order")]
public partial class Order
{
    public int OrderId { get; set; }

    public int UserId { get; set; }

    public int TotalAmount { get; set; }

    public DateTime CreatedOn { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime ModifiedOn { get; set; }

    public string ModifiedBy { get; set; } = null!;

    public virtual UserDetail User { get; set; } = null!;
}
