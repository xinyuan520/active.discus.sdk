using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discus.SDK.Redis.Core
{
    public static class ArgumentCheck
    {
        /// <summary>
        /// Validates that <paramref name="argument"/> is not null or white space , otherwise throws an exception.
        /// </summary>
        /// <param name="argument">Argument.</param>
        /// <param name="argumentName">Argument name.</param>
        /// <exception cref="ArgumentNullException" />
        public static void NotNullOrWhiteSpace(string argument, string argumentName)
        {
            if (string.IsNullOrWhiteSpace(argument))
            {
                throw new ArgumentNullException(argumentName);
            }
        }

        /// <summary>
        /// Validates that <paramref name="argument"/> is not less or than zero , otherwise throws an exception.
        /// </summary>
        /// <param name="argument">Argument.</param>
        /// <param name="argumentName">Argument name.</param>
        public static void NotLessThanOrEqualZero(int argument, string argumentName)
        {
            if (argument <= 0)
            {
                throw new ArgumentOutOfRangeException(argumentName);
            }
        }
    }
}
