﻿namespace DocManagementSystem.Models
{
    public class User
    {
        public string Username { get; set; }
        public string PasswordHash { get; set; }  // For real app, store hashed password
    }
}
