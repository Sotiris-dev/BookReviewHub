﻿namespace BookReviewHub.Application.Models;

public class RegisterUserDto
{
    public string Username { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;
}
