using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TasteHub.Core.Models
{
    /// <summary>
    /// Model for category information in a database
    /// </summary>
    public class CategoryInfoViewModel
    {
        public CategoryInfoViewModel(
            int id,
            string name)
        {
            Id = id;
            Name = name;
        }

        /// <summary>
        /// Category identifier
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Category name
        /// </summary>
        public string Name { get; set; }
    }
}
