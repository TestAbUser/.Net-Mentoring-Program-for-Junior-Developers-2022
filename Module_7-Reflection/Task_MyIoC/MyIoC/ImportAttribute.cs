using System;

namespace MyIoC
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    // can be applied to properties or fields only
    public class ImportAttribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Class)] 
    // can be applied to classes only
    public class ImportConstructorAttribute : Attribute
    {
    }

    
    [AttributeUsage(AttributeTargets.Class)]
    // can be applied to classes only
    public class ExportAttribute : Attribute
    {
        public ExportAttribute()
        { }

        public ExportAttribute(Type contract)
        {
            Contract = contract;
        }

        public Type Contract { get; private set; }
    }


    [Export]
    public class ContractBLL { }

    [Export]
    public class ContractDLL { }

}
