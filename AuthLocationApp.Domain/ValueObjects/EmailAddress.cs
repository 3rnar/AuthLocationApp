using System.Text.RegularExpressions;
using AuthLocationApp.Domain.Exceptions;

namespace AuthLocationApp.Domain.ValueObjects
{
   public partial class EmailAddress
   {
      private static readonly Regex _emailRegex =
          EmailRegex();

      public string Value { get; }

      public EmailAddress(string email)
      {
         if (string.IsNullOrWhiteSpace(email))
            throw new DomainException("Email cannot be empty.");

         if (!_emailRegex.IsMatch(email))
            throw new DomainException("Email is not in valid format.");

         Value = email;
      }

      [GeneratedRegex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled)]
      private static partial Regex EmailRegex();

      public override bool Equals(object? obj)
      {
         return Equals(obj as EmailAddress);
      }

      public bool Equals(EmailAddress? other)
      {
         if (other is null)
            return false;

         return string.Equals(Value, other.Value, StringComparison.OrdinalIgnoreCase);
      }

      public override int GetHashCode()
      {
         return Value.ToLowerInvariant().GetHashCode();
      }

      public static bool operator ==(EmailAddress left, EmailAddress right)
      {
         if (ReferenceEquals(left, right))
            return true;
         if (left is null || right is null)
            return false;
         return left.Equals(right);
      }

      public static bool operator !=(EmailAddress left, EmailAddress right)
      {
         return !(left == right);
      }

      public override string ToString() => Value;
   }
}
