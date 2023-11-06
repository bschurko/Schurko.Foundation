using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

using System.Text;

namespace Schurko.Foundation.Web
{
    public static class WebUtility
    {
        // some consts copied from Char / CharUnicodeInfo since we don't have friend access to those types
        private const char HIGH_SURROGATE_START = '\uD800';
        private const char LOW_SURROGATE_START = '\uDC00';
        private const char LOW_SURROGATE_END = '\uDFFF';
        private const int UNICODE_PLANE00_END = 0x00FFFF;
        private const int UNICODE_PLANE01_START = 0x10000;
        private const int UNICODE_PLANE16_END = 0x10FFFF;

        private const int UnicodeReplacementChar = '\uFFFD';

        private static readonly UnicodeDecodingConformance s_htmlDecodeConformance;
        private static readonly UnicodeEncodingConformance s_htmlEncodeConformance;

        static WebUtility()
        {
            s_htmlDecodeConformance = UnicodeDecodingConformance.Strict;
            s_htmlEncodeConformance = UnicodeEncodingConformance.Strict;
        }



        #region UrlEncode implementation

        // *** Source: alm/tfs_core/Framework/Common/UriUtility/HttpUtility.cs
        // This specific code was copied from above ASP.NET codebase.

        private static byte[] UrlEncode(byte[] bytes, int offset, int count, bool alwaysCreateNewReturnValue)
        {
            byte[] encoded = UrlEncode(bytes, offset, count);

            return alwaysCreateNewReturnValue && encoded != null && encoded == bytes
                ? (byte[])encoded.Clone()
                : encoded;
        }

        private static byte[] UrlEncode(byte[] bytes, int offset, int count)
        {
            if (!ValidateUrlEncodingParameters(bytes, offset, count))
            {
                return null;
            }

            int cSpaces = 0;
            int cUnsafe = 0;

            // count them first
            for (int i = 0; i < count; i++)
            {
                char ch = (char)bytes[offset + i];

                if (ch == ' ')
                    cSpaces++;
                else if (!IsUrlSafeChar(ch))
                    cUnsafe++;
            }

            // nothing to expand?
            if (cSpaces == 0 && cUnsafe == 0)
                return bytes;

            // expand not 'safe' characters into %XX, spaces to +s
            byte[] expandedBytes = new byte[count + cUnsafe * 2];
            int pos = 0;

            for (int i = 0; i < count; i++)
            {
                byte b = bytes[offset + i];
                char ch = (char)b;

                if (IsUrlSafeChar(ch))
                {
                    expandedBytes[pos++] = b;
                }
                else if (ch == ' ')
                {
                    expandedBytes[pos++] = (byte)'+';
                }
                else
                {
                    expandedBytes[pos++] = (byte)'%';
                    expandedBytes[pos++] = (byte)IntToHex(b >> 4 & 0xf);
                    expandedBytes[pos++] = (byte)IntToHex(b & 0x0f);
                }
            }

            return expandedBytes;
        }

        #endregion

        #region UrlEncode public methods

        [SuppressMessage("Microsoft.Design", "CA1055:UriReturnValuesShouldNotBeStrings", Justification = "Already shipped public API; code moved here as part of API consolidation")]
        public static string UrlEncode(string value)
        {
            if (value == null)
                return null;

            byte[] bytes = Encoding.UTF8.GetBytes(value);
            byte[] encodedBytes = UrlEncode(bytes, 0, bytes.Length, false /* alwaysCreateNewReturnValue */);
            return Encoding.UTF8.GetString(encodedBytes, 0, encodedBytes.Length);
        }

        public static byte[] UrlEncodeToBytes(byte[] value, int offset, int count)
        {
            return UrlEncode(value, offset, count, true /* alwaysCreateNewReturnValue */);
        }

        #endregion

        #region UrlDecode implementation

        // *** Source: alm/tfs_core/Framework/Common/UriUtility/HttpUtility.cs
        // This specific code was copied from above ASP.NET codebase.
        // Changes done - Removed the logic to handle %Uxxxx as it is not standards compliant.

        private static string UrlDecodeInternal(string value, Encoding encoding)
        {
            if (value == null)
            {
                return null;
            }

            int count = value.Length;
            UrlDecoder helper = new UrlDecoder(count, encoding);

            // go through the string's chars collapsing %XX and
            // appending each char as char, with exception of %XX constructs
            // that are appended as bytes

            for (int pos = 0; pos < count; pos++)
            {
                char ch = value[pos];

                if (ch == '+')
                {
                    ch = ' ';
                }
                else if (ch == '%' && pos < count - 2)
                {
                    int h1 = HexToInt(value[pos + 1]);
                    int h2 = HexToInt(value[pos + 2]);

                    if (h1 >= 0 && h2 >= 0)
                    {     // valid 2 hex chars
                        byte b = (byte)(h1 << 4 | h2);
                        pos += 2;

                        // don't add as char
                        helper.AddByte(b);
                        continue;
                    }
                }

                if ((ch & 0xFF80) == 0)
                    helper.AddByte((byte)ch); // 7 bit have to go as bytes because of Unicode
                else
                    helper.AddChar(ch);
            }

            return helper.GetString();
        }

        private static byte[] UrlDecodeInternal(byte[] bytes, int offset, int count)
        {
            if (!ValidateUrlEncodingParameters(bytes, offset, count))
            {
                return null;
            }

            int decodedBytesCount = 0;
            byte[] decodedBytes = new byte[count];

            for (int i = 0; i < count; i++)
            {
                int pos = offset + i;
                byte b = bytes[pos];

                if (b == '+')
                {
                    b = (byte)' ';
                }
                else if (b == '%' && i < count - 2)
                {
                    int h1 = HexToInt((char)bytes[pos + 1]);
                    int h2 = HexToInt((char)bytes[pos + 2]);

                    if (h1 >= 0 && h2 >= 0)
                    {     // valid 2 hex chars
                        b = (byte)(h1 << 4 | h2);
                        i += 2;
                    }
                }

                decodedBytes[decodedBytesCount++] = b;
            }

            if (decodedBytesCount < decodedBytes.Length)
            {
                Array.Resize(ref decodedBytes, decodedBytesCount);
            }

            return decodedBytes;
        }

        #endregion

        #region UrlDecode public methods


        [SuppressMessage("Microsoft.Design", "CA1055:UriReturnValuesShouldNotBeStrings", Justification = "Already shipped public API; code moved here as part of API consolidation")]
        public static string UrlDecode(string encodedValue)
        {
            if (encodedValue == null)
                return null;

            return UrlDecodeInternal(encodedValue, Encoding.UTF8);
        }

        public static byte[] UrlDecodeToBytes(byte[] encodedValue, int offset, int count)
        {
            return UrlDecodeInternal(encodedValue, offset, count);
        }

        #endregion

        #region Helper methods

        // similar to Char.ConvertFromUtf32, but doesn't check arguments or generate strings
        // input is assumed to be an SMP character
        private static void ConvertSmpToUtf16(uint smpChar, out char leadingSurrogate, out char trailingSurrogate)
        {
            Debug.Assert(UNICODE_PLANE01_START <= smpChar && smpChar <= UNICODE_PLANE16_END);

            int utf32 = (int)(smpChar - UNICODE_PLANE01_START);
            leadingSurrogate = (char)(utf32 / 0x400 + HIGH_SURROGATE_START);
            trailingSurrogate = (char)(utf32 % 0x400 + LOW_SURROGATE_START);
        }

        private static int HexToInt(char h)
        {
            return h >= '0' && h <= '9' ? h - '0' :
            h >= 'a' && h <= 'f' ? h - 'a' + 10 :
            h >= 'A' && h <= 'F' ? h - 'A' + 10 :
            -1;
        }

        private static char IntToHex(int n)
        {
            Debug.Assert(n < 0x10);

            if (n <= 9)
                return (char)(n + '0');
            else
                return (char)(n - 10 + 'A');
        }

        // Set of safe chars, from RFC 1738.4 minus '+'
        private static bool IsUrlSafeChar(char ch)
        {
            if (ch >= 'a' && ch <= 'z' || ch >= 'A' && ch <= 'Z' || ch >= '0' && ch <= '9')
                return true;

            switch (ch)
            {
                case '-':
                case '_':
                case '.':
                case '!':
                case '*':
                case '(':
                case ')':
                    return true;
            }

            return false;
        }

        private static bool ValidateUrlEncodingParameters(byte[] bytes, int offset, int count)
        {
            if (bytes == null && count == 0)
                return false;
            if (bytes == null)
            {
                throw new ArgumentNullException("bytes");
            }
            if (offset < 0 || offset > bytes.Length)
            {
                throw new ArgumentOutOfRangeException("offset");
            }
            if (count < 0 || offset + count > bytes.Length)
            {
                throw new ArgumentOutOfRangeException("count");
            }

            return true;
        }

        private static bool StringRequiresHtmlDecoding(string s)
        {
            if (s_htmlDecodeConformance == UnicodeDecodingConformance.Compat)
            {
                // this string requires html decoding only if it contains '&'
                return s.IndexOf('&') >= 0;
            }
            else
            {
                // this string requires html decoding if it contains '&' or a surrogate character
                for (int i = 0; i < s.Length; i++)
                {
                    char c = s[i];
                    if (c == '&' || char.IsSurrogate(c))
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        #endregion

        #region UrlDecoder nested class

        // *** Source: alm/tfs_core/Framework/Common/UriUtility/HttpUtility.cs
        // This specific code was copied from above ASP.NET codebase.

        // Internal class to facilitate URL decoding -- keeps char buffer and byte buffer, allows appending of either chars or bytes
        private class UrlDecoder
        {
            private int _bufferSize;

            // Accumulate characters in a special array
            private int _numChars;
            private char[] _charBuffer;

            // Accumulate bytes for decoding into characters in a special array
            private int _numBytes;
            private byte[] _byteBuffer;

            // Encoding to convert chars to bytes
            private Encoding _encoding;

            private void FlushBytes()
            {
                if (_numBytes > 0)
                {
                    _numChars += _encoding.GetChars(_byteBuffer, 0, _numBytes, _charBuffer, _numChars);
                    _numBytes = 0;
                }
            }

            internal UrlDecoder(int bufferSize, Encoding encoding)
            {
                _bufferSize = bufferSize;
                _encoding = encoding;

                _charBuffer = new char[bufferSize];
                // byte buffer created on demand
            }

            internal void AddChar(char ch)
            {
                if (_numBytes > 0)
                    FlushBytes();

                _charBuffer[_numChars++] = ch;
            }

            internal void AddByte(byte b)
            {
                if (_byteBuffer == null)
                    _byteBuffer = new byte[_bufferSize];

                _byteBuffer[_numBytes++] = b;
            }

            internal string GetString()
            {
                if (_numBytes > 0)
                    FlushBytes();

                if (_numChars > 0)
                    return new string(_charBuffer, 0, _numChars);
                else
                    return string.Empty;
            }
        }

        #endregion

    }

    //
    // Summary:
    //     Controls how Unicode characters are output by the Overload:System.Net.WebUtility.HtmlEncode
    //     methods.
    public enum UnicodeEncodingConformance
    {
        //
        // Summary:
        //     Use automatic behavior. The Unicode encoding behavior is determined by current
        //     application's target Framework. For .NET Framework 4.5 and later, the Unicode
        //     encoding behavior is strict.
        Auto,
        //
        // Summary:
        //     Use strict behavior. Specifies that individual UTF-16 surrogate code points are
        //     combined into a single code point when one of the Overload:System.Net.WebUtility.HtmlEncode
        //     methods is called. For example, given the input string "\uD84C\uDFB4" (or "\U000233B4"),
        //     the output of the Overload:System.Net.WebUtility.HtmlEncode methods is "&#144308;".
        //     If the input is a malformed UTF-16 string (it contains unpaired surrogates, for
        //     example), the bad code points will be replaced with U+FFFD (Unicode replacement
        //     char) before being HTML-encoded.
        Strict,
        //
        // Summary:
        //     Use compatible behavior. Specifies that individual UTF-16 surrogate code points
        //     are output as-is when one of Overload:System.Net.WebUtility.HtmlEncode methods
        //     is called. For example, given a string "\uD84C\uDFB4" (or "\U000233B4"), the
        //     output of Overload:System.Net.WebUtility.HtmlEncode is "\uD84C\uDFB4" (the input
        //     is not encoded).
        Compat
    }

    /// <summary>
    /// Controls how Unicode characters are interpreted by the WebUtility.HtmlDecode routine.
    /// </summary>
    /// <remarks>
    /// See http://www.w3.org/International/questions/qa-escapes#bytheway for more information
    /// on how Unicode characters in the SMP are supposed to be encoded in HTML.
    /// </remarks>
    internal enum UnicodeDecodingConformance
    {
        /// <summary>
        /// The Unicode encoding behavior is determined by current application's
        /// TargetFrameworkAttribute. Framework40 and earlier gets Compat behavior; Framework45
        /// and later gets Strict behavior.
        /// </summary>
        Auto,

        /// <summary>
        /// Specifies that the incoming encoded data is checked for validity before being
        /// decoded. For example, an input string of "&amp;#144308;" would decode as U+233B4,
        /// but an input string of "&amp;#xD84C;&amp;#xDFB4;" would fail to decode properly.
        /// </summary>
        /// <remarks>
        /// Already-decoded data in the string is not checked for validity. For example, an
        /// input string of "\ud800" will result in an output string of "\ud800", as the
        /// already-decoded surrogate is skipped during decoding, even though it is unpaired.
        /// </remarks>
        Strict,

        /// <summary>
        /// Specifies that incoming data is not checked for validity before being decoded.
        /// For example, an input string of "&amp;#xD84C;" would decode as U+D84C, which is
        /// an unpaired surrogate. Additionally, the decoder does not understand code points
        /// in the SMP unless they're represented as HTML-encoded surrogates, so the input
        /// string "&amp;#144308;" would result in the output string "&amp;#144308;".
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Compat", Justification = "Shorthand for 'compatibility mode'.")]
        Compat,

        /// <summary>
        /// Similar to 'Compat' in that there are no validity checks, but the decoder also
        /// understands SMP code points. The input string "&amp;#144308;" will thus decode
        /// into the character U+233B4 correctly.
        /// </summary>
        /// <remarks>
        /// This switch is meant to provide maximum interoperability when the decoder doesn't
        /// know which format the provider is using to generate the encoded string.
        /// </remarks>
        Loose,
    }
}
