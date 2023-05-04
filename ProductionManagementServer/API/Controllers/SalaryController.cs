﻿using API.Models;
using AutoMapper;
using BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("salary/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    public class SalaryController : ControllerBase
    {
        private readonly ISalaryService _salaryService;
        private readonly IMapper _mapper;
        public SalaryController(ISalaryService salaryService, IMapper mapper)
        {
            _salaryService = salaryService;
            _mapper = mapper;
        }

        [HttpGet]
        [ActionName("all")]
        public async Task<ActionResult<IEnumerable<SalaryModel>>> GetSalaries()
        {
            var items = _mapper.Map<List<SalaryModel>>(_salaryService.GetList());

            return await Task.FromResult(items);
        }
    }
}
