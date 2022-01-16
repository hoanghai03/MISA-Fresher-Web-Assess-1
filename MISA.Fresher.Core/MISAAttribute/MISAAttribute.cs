using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Fresher.Core.MISAAttribute
{
    [AttributeUsage(AttributeTargets.Property)]
    public class NotEmpty: Attribute
    {

    }
  
    
    [AttributeUsage(AttributeTargets.Property)]
    public class NotDuplicate : Attribute
    {

    }

    [AttributeUsage(AttributeTargets.Property)]
    public class PropertyName : Attribute
    {
        public string name;
        public PropertyName(string name)
        {
            this.name = name;
        }
    }


    [AttributeUsage(AttributeTargets.Property)]
    public class CheckDate : Attribute
    {
        public CheckDate()
        { 

        }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class CheckInsertCode : Attribute
    {
        public CheckInsertCode()
        {

        }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class Ignore : Attribute
    {
        public Ignore()
        {

        }
    }    
    
    [AttributeUsage(AttributeTargets.Property)]
    public class Id : Attribute
    {
        public Id()
        {

        }
    }    
    
    [AttributeUsage(AttributeTargets.Property)]
    public class ExportExcel : Attribute
    {
        public ExportExcel()
        {

        }
    }
}
