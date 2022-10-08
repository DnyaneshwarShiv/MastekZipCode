using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MastekDeveloperTest.Middlewares.ErrorModel
{
    public class XSSErrorResponse
    {
        /// <summary>
        /// Error Code
        /// </summary>
        public int ErrorCode { get; set; }
        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; }
    }
}
