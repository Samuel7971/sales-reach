using Microsoft.AspNetCore.Mvc;
using SalesReachAPI.Interface.Controllers.Shared;

namespace SalesReachAPI.Interface.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class CustomResponseAttribute : ProducesResponseTypeAttribute
    {
        public CustomResponseAttribute(int statusCode) : base(typeof(CustomResult), statusCode) { }
    }
}
