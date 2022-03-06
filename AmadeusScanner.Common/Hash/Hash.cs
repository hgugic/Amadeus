using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace AmadeusScanner.Common.Hash
{
    public class Hash
    {
        private static string GetHashString(string text)
        {
            if (string.IsNullOrEmpty(text))
                return string.Empty;

            using var sha = new System.Security.Cryptography.SHA256Managed();
            byte[] textData = Encoding.UTF8.GetBytes(text);
            byte[] hash = sha.ComputeHash(textData);
            return BitConverter.ToString(hash).Replace("-", string.Empty);
        }

        public static string GenerateHash<T>(T value)
        {
            if (value == null)
                return "";

            var builder = new StringBuilder();

            foreach (PropertyInfo prop in value.GetType().GetProperties())
            {

                var val = prop.GetValue(value, null);

                if (val == null)
                    continue;

                builder.Append(val.ToString());
            }

            return GetHashString(builder.ToString());
        }
    }
}
