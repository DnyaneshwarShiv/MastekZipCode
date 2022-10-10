using AutoMapper;
using MastekDeveloperTest.Controllers;
using MastekDeveloperTest.Service;
using MastekTest.PostCodeRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;
using Xunit;


namespace MastekDeveloperTest.UnitTest
{
    public class PostCodeControllerTest
    {
        #region private vairable
        private readonly PostCodeController _postCodeController;
        #endregion
        #region Constructor
        /// <summary>
        /// Business Profile Controller Property initialization for Test
        /// </summary>
        public PostCodeControllerTest()
        {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MapperProfile());
            });
            var configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddEnvironmentVariables();
            var Configuration = configurationBuilder.Build();
            var config= new ConfigurationBuilder()
                .AddConfiguration(Configuration)
                .AddJsonFile($"appsettings.json", optional: false, reloadOnChange: true)
                .Build();
           
           var repository = new PostCodesRepository(config);
           _postCodeController = new PostCodeController(new PostCodeService(repository, mockMapper.CreateMapper()));
        }
        #endregion
        #region Test Method
        /// <summary>
        /// Get zip code list based on type ahead character A
        /// </summary>
        [Theory]
        [InlineData("A")]
        public async Task Controller_should_get_auto_completion_list(string code)
        {
           var result = await _postCodeController.GetAutoCompletionList(code);
            Assert.True(result?.Count > 0);
        }

        /// <summary>
        /// Get zip code details based on type ahead character A
        /// </summary>
        [Theory]
        [InlineData("BN7 1AD")]
        public async Task Controller_should_get_area_details(string code)
        {
            var result = await _postCodeController.Get(code);
            Assert.True(result?.Count > 0);
        }

        #endregion
    }
}
