using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace HalfFullWidth
{
    /// <summary>
    /// Defines a range of chars from the code-point of the start char until the code point of the end char.
    /// </summary>
    internal class CharRange : IEnumerable<char>, IEnumerable
    {
        public char Start { get; }
        public char End { get; }

        /// <summary>
        /// The number of chars in the range.
        /// </summary>
        public int Length => End - Start + 1;

        public CharRange(char start, char end)
        {
            if (end <= start)
            {
                throw new InvalidOperationException("Code point of End must be greater than code point of Start.");
            }
            Start = start;
            End = end;
        }

        /// <summary>
        /// Map the current range (assumed half-width) to a full-width range.
        /// </summary>
        /// <param name="full">The full-width range that should map to each char in the half-with range.</param>
        /// <returns>The mapped half-width to full-width range.</returns>
        public IEnumerable<KeyValuePair<char, char>> Map(CharRange full)
        {
            if (this.Length != full.Length) throw new ArgumentOutOfRangeException();
            return this.Zip(full, (h, f) => new KeyValuePair<char, char>(h, f));
        }

        public IEnumerator<char> GetEnumerator()
        {
            for (int i = 0; i < Length; i++)
            {
                yield return (char)(Start + i);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}