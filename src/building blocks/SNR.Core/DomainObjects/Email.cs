﻿using DSC.Core.DomainObjects;
using System.Text.RegularExpressions;

namespace SNR.Core.DomainObjects
{
    public class Email
    {
        public const int AddressMaxLength = 254;
        public const int AddressMinLength = 5;
        public string Address { get; private set; }

        public Email(string address)
        {
            if (!Validate(address)) throw new DomainException("E-mail inválido");
            Address = address;
        }

        public static bool Validate(string email)
        {
            var regexEmail = new Regex(@"^(?("")("".+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$");
            return regexEmail.IsMatch(email);
        }

        public void Update(string address)
        {
            Address = address;
        }

    }
}