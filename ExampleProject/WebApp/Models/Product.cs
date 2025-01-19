using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WebApp.Models
{
    public class Product
    {
        public long ProductId { get; set; }
        public string Name { get; set; } = string.Empty;
        
        [Column(TypeName = "decimal(8,2)")]

        [DisplayFormat(DataFormatString = "{0:c2}", ApplyFormatInEditMode = true)]
        public decimal Price { get; set; }

        public long CategoryId { get; set; }
        //[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Category? Category { get; set; }

        public long SupplierId { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)] //-> general patern in program.cs
        //builder.Services.AddControllers();
        //builder.Services.AddControllers().AddJsonOptions(opts =>{opts.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;});
        //builder.Services.Configure<JsonOptions>(opts =>{opts.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;});

        public Supplier? Supplier { get; set; }
}
}