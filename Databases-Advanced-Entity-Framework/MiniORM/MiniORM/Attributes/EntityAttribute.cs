using System;

namespace MiniORM.Attributes
{
    class EntityAttribute : Attribute
    {
        public string TableName { get; set; }
    }
}
