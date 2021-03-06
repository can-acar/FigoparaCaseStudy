﻿using System;
using System.Net.Mime;
using System.Threading.Tasks;
using FigoparaCaseStudyApi.Request;
using FigoparaCaseStudyApi.Response;
using FigoparaCaseStudyApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FigoparaCaseStudyApi.Controllers
{
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Route("api/v1/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> Logger;
        private readonly IUserService UserService;

        public UserController(ILogger<UserController> logger, IUserService userService)
        {
            Logger      = logger;
            UserService = userService;
        }

        [HttpPost("add")]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> UserAdd([FromBody] UserAddRequest request)
        {
            try
            {
                var result = await UserService.Add(request);

                if (result.Status)
                {
                    return Ok(result);
                }

                return BadRequest(result);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("update")]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> UserUpdate([FromBody] UserUpdateRequest request)
        {
            try
            {
                var result = await UserService.Update(request);

                if (result.Status)
                {
                    return Ok(result);
                }

                return BadRequest(result);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("delete")]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> UserDelete([FromBody] UserDeleteRequest request)
        {
            try
            {
                var result = await UserService.Delete(request);

                if (result.Status)
                {
                    return Ok(result);
                }

                return BadRequest(result);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get/{Id}")]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> UseGet([FromRoute] UserGetRequest request)
        {
            try
            {
                var result = await UserService.Get(request);

                if (result.Status)
                {
                    return Ok(result);
                }

                return BadRequest(result);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                
                return BadRequest(ex.Message);
            }
        }

        //Güvenlik gereği n parametleri sorgularda injection denemelerinden izin vermemek   için post metodu
        [HttpPost("search")]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> UseSearch([FromBody] UserSearchRequest request)
        {
            try
            {
                var result = await UserService.Searh(request);

                if (result.Status)
                {
                    return Ok(result);
                }

                return BadRequest(result);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                
                return BadRequest(ex.Message);
            }
        }
    }
}