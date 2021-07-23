using System;
using System.Collections.Generic;
using System.Text;

namespace WebAppSite.Domain.Entities
{
    public interface IBaseEntity<T>
    {
        T Id { get; set; }
        DateTime? DateDelete { get; set; }// может быть налом ссылочный тип
    }
    public abstract class BaseEntity<T> : IBaseEntity<T>
    {
        public virtual T Id { get; set; }
        public virtual DateTime DateCreate { get; set; }
        public virtual DateTime? DateModify { get; set; }
        public virtual DateTime? DateDelete { get; set; }
    }
}
