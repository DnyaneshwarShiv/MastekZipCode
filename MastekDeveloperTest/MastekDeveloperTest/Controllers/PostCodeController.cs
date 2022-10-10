using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MastekDeveloperTest.DTO;
using MastekDeveloperTest.Service;
using Microsoft.AspNetCore.Mvc;

namespace MastekDeveloperTest.Controllers
{
    [Route("api/[controller]")]
    public class PostCodeController : ControllerBase
    {
        private readonly IPostCodeService _postCodeService;
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="postCodeService"></param>
        public PostCodeController(IPostCodeService postCodeService)
        {
            _postCodeService = postCodeService;
        }
       /// <summary>
       /// Get Auto Completion for typeSearch Code
       /// </summary>
       /// <param name="code"></param>
       /// <returns></returns>
        [HttpGet("autoCompletion")]
        public async Task<IList<string>> GetAutoCompletionList(string code)
        {
            return await _postCodeService.GetPostCodeForAutoCompletion(code);
        }

        /// <summary>
        /// Get post code object for selected Code
        /// </summary>
        /// <param name="selectedCode"></param>
        /// <returns></returns>
        [HttpGet("GetPostCodeDetails")]
        public async Task<IList<MastekArea>> Get(string selectedCode)
        {
            return await _postCodeService.GetPostCodeDetails(selectedCode);
        }
    }
}
