using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConventionsAndConstraints.Infrastructure
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple =false)]
    public class ActionNamePrefixAttribute : Attribute, IActionModelConvention
    {
        private readonly string _namePrefix; 
        public ActionNamePrefixAttribute(string prefix) => _namePrefix = prefix;

        public void Apply(ActionModel action)
        {
            action.ActionName = _namePrefix + action.ActionName;
        }
    }
}
