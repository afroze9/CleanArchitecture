﻿namespace CleanArchitecture.Application.Common.Abstractions;

public interface ICurrentUserService
{
    string CurrentUserId { get; }
}