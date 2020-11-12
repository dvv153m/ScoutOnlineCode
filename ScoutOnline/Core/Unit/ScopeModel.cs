using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScoutOnline.Core.Unit
{
    /// <summary>
    /// Группа
    /// </summary>
    public class ScopeModel
    {
        public ScopeModel()
        {
            IsAvailable = true;
        }

        /// <summary>
        /// Идентификатор скопа
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Наименование скопа
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Родительский скоп.
        /// </summary>
        public int? ParentId { get; set; }

        /// <summary>
        /// Идентификатор компании.
        /// </summary>
        public int CompanyId { get; set; }

        /// <summary>
        /// Доступен ли для пользователя (для родительских скопов)
        /// </summary>
        public bool IsAvailable { get; set; }
    }
}
