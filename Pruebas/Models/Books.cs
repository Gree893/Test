using Pruebas.Modelos;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Books
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }


    public string Title { get; set; }


    public string Description { get; set; }


    public int Pages { get; set; }

    [ForeignKey("Author")]
    public int AuthorId { get; set; }
    public Author Author { get; set; }

    [ForeignKey("Category")]
    public int CategoryId { get; set; }
    public Category Category { get; set; }
}
