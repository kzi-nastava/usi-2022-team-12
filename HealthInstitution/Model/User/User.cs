﻿using System;

namespace HealthInstitution.Model
{
    public abstract class User : BaseObservableEntity
    {
        private string _password;
        public string Password { get => _password; set => OnPropertyChanged(ref _password, value); }

        private string _emailAddress;
        public string EmailAddress { get => EmailAddress; set => OnPropertyChanged(ref _emailAddress, value); }

        private string _firstName;
        public string FirstName { get => _firstName; set => OnPropertyChanged(ref _firstName, value); }

        private string _lastName;
        public string LastName { get => _lastName; set => OnPropertyChanged(ref _lastName, value); }
        public string FullName => $"{_firstName} {_lastName}";

        private DateTime _dateOfBirth;
        public DateTime DateOfBirth { get => _dateOfBirth; set => OnPropertyChanged(ref _dateOfBirth, value); }

        private Role _role;
        public Role Role { get => _role; set => OnPropertyChanged(ref _role, value); }
    }
}