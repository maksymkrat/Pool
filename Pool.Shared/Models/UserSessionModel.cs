﻿namespace Pool.Shared.Models;

public class UserSessionModel
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public string Token { get; set; }
    public string Role { get; set; }
    public List<WordModel> Words { get; set; }
    public int ExpiryIn { get; set; }
    public DateTime ExpiryTimeStamp { get; set; }
}