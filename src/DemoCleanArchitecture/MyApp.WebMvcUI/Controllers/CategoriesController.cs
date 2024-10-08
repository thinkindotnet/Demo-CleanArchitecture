﻿using MediatR;

using Microsoft.AspNetCore.Mvc;

using MyApp.Application.ManageCategoriesFeature.Commands.CreateCommand;
using MyApp.Application.ManageCategoriesFeature.Commands.DeleteCommand;
using MyApp.Application.ManageCategoriesFeature.Commands.UpdateCommand;
using MyApp.Application.ManageCategoriesFeature.DTOs;
using MyApp.Application.ManageCategoriesFeature.Queries.GetAllQuery;
using MyApp.Application.ManageCategoriesFeature.Queries.GetByIdQuery;

namespace MyApp.WebMvcUI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{

    private readonly IMediator _mediator;

    public CategoriesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<AllCategoriesDto>> GetAllCategories()
    {
        return await _mediator.Send(new GetAllCategoryQuery());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CategoryDto>> GetCategory(int id)
    {
        return await _mediator.Send(new GetCategoryByIdQuery() { CategoryId = id });
    }


    [HttpPost]
    public async Task<ActionResult<int>> PostCategory(CreateCategoryCommand command)
    {
        return await _mediator.Send(command);
    }


    [HttpPut("{id}")]
    public async Task<ActionResult<int>> PutCategory(
        int id, UpdateCategoryCommand command)
    {
        if (id != command.CategoryId) 
        {
            return BadRequest();
        }

        await _mediator.Send(command);

        return Accepted();
    }


    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteCategory(int id)
    {
        await _mediator.Send(new DeleteCategoryCommand { CategoryId = id });

        return NoContent();
    }

}
