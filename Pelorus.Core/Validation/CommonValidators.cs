﻿using System;
using System.Text.RegularExpressions;

namespace Pelorus.Core.Validation
{
    /// <summary>
    /// Validators for common string types.
    /// </summary>
    public static class CommonValidators
    {
        private readonly static Regex _emailRegex = new Regex(@"^(?("")(""[^""]+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]*\.)+[0-9a-zA-Z]{2,24}))$");
        private readonly static Regex _phoneNumberRegex = new Regex(@"[\(]{0,1}[0-9]{3}[\s]{0,}[-\.\*\)]{0,1}[\s]{0,}[0-9]{3}[\s]{0,}[-\.\*\)]{0,1}[\s]{0,}[0-9]{4}");
        private readonly static Regex _zipCodeRegex = new Regex(@"[0-9]{5}(-[0-9]{4}){0,1}");

        /// <summary>
        /// Validate an email address.
        /// </summary>
        /// <param name="emailAddress">String to validate as an email address.</param>
        public static void ValidateEmailAddress(string emailAddress)
        {
            if (string.IsNullOrWhiteSpace(emailAddress))
            {
                throw new ArgumentNullException(nameof(emailAddress));
            }

            emailAddress = emailAddress.ToLower()
                                       .Trim();
            var matches = _emailRegex.Matches(emailAddress);

            if ((1 != matches.Count) || ((0 < matches.Count) && (!matches[0].Value.Equals(emailAddress))))
            {
                throw new Exception($"Invalid email address: {emailAddress}.");
            }
        }

        /// <summary>
        /// Validate a US phone number.
        /// </summary>
        /// <param name="phoneNumber">String to validate as a phone number.</param>
        public static void ValidatePhoneNumber(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
            {
                throw new ArgumentNullException(nameof(phoneNumber));
            }

            var matches = _phoneNumberRegex.Matches(Types.Cast<string>(phoneNumber));

            if ((1 != matches.Count) || ((0 < matches.Count) && (!matches[0].Value.Equals(phoneNumber))))
            {
                throw new Exception($"Invalid phone number: {phoneNumber}.");
            }
        }

        /// <summary>
        /// Validate a US postal code.
        /// </summary>
        /// <param name="zipCode">String to validate as a US postal code.</param>
        public static void ValidateZipCode(string zipCode)
        {
            if (string.IsNullOrWhiteSpace(zipCode))
            {
                throw new ArgumentNullException(nameof(zipCode));
            }

            MatchCollection matches = _zipCodeRegex.Matches(Types.Cast<string>(zipCode));

            if ((1 != matches.Count) || ((0 < matches.Count) && (!matches[0].Value.Equals(zipCode))))
            {
                throw new Exception($"Invalid zip code: {zipCode}.");
            }
        }
    }
}
