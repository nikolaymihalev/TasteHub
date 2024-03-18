using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasteHub.Core.Contracts;

namespace TasteHub.Core.Models.Category
{
    public class CategoryForAllModel
    {
        public CategoryForAllModel(
            int id,
            string name,
            bool isInUse)
        {
            Id = id;
            Name = name;
            IsInUse = isInUse;
        }

        /// <summary>
        /// Category identifier
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Category name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Property for checking if category is in use
        /// </summary>
        public bool IsInUse { get; set; }
    }
}
