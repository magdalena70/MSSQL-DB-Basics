using System;

namespace MiniORM.Attributes
{
    class ColumnAttribute : Attribute
    {
        public string ColumnName { get; set; }
    }
}
