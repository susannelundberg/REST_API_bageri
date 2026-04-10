using System.ComponentModel;
using System.Runtime.InteropServices.Marshalling;
using Microsoft.EntityFrameworkCore.Query;
using Newtonsoft.Json;

namespace BageriApi.Entities;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
}
