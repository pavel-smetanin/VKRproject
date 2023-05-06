using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace VKRproject.Models
{
    public abstract class BasePeople
    {
        public int ID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string PatrName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

    }
}
