﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker.Domain.Enumerables
{
    public enum RegistrationResult
    {
        Success,
        PasswordsDoNotMatch,
        EmailAlreadyExists,
        InputFieldIsNull,
        NameContainsSpecialCharacter,
        EmailFormatIsInvalid
    }
}