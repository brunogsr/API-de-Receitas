using Microsoft.AspNetCore.Mvc;
using recipes_api.Services;
using recipes_api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace recipes_api.Controllers;

[ApiController]
[Route("user")]
public class UserController : ControllerBase
{
    public readonly IUserService _service;

    public UserController(IUserService service)
    {
        this._service = service;
    }

    // 6 - Sua aplicação deve ter o endpoint GET /user/:email
    [HttpGet("{email}", Name = "GetUser")]
    public IActionResult Get(string email)
    {
        var user = _service.GetUser(email);
        if (user == null)
        {
            return NotFound();
        }
        return Ok(user);
    }

    // 7 - Sua aplicação deve ter o endpoint POST /user
    [HttpPost]
    public IActionResult Create([FromBody] User user)
    {
        if (user == null)
        {
            return BadRequest();
        }
        _service.AddUser(user);
        return CreatedAtRoute("GetUser", new { email = user.Email }, user);
    }

    // "8 - Sua aplicação deve ter o endpoint PUT /user
    [HttpPut("{email}")]
    public IActionResult Update(string email, [FromBody] User user)
    {
        if (email == null || email != user.Email)
        {
            return BadRequest();
        }
        if (_service.GetUser(email) == null)
        {
            return NotFound();
        }
        _service.UpdateUser(user);
        return Ok();
    }

    // 9 - Sua aplicação deve ter o endpoint DEL /user
    [HttpDelete("{email}")]
    public IActionResult Delete(string email)
    {
        if (_service.GetUser(email) == null)
        {
            return NotFound();
        }
        _service.DeleteUser(email);
        return NoContent();
    }
}