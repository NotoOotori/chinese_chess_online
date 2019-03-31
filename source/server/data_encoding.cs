using System;
using System.Collections.Generic;
using System.Text;

namespace server
{
    class DataEncoding
    {
        /// <summary>
        /// Get a dictionary from the data string.
        /// </summary>
        /// <param name="str">Data string.</param>
        /// <returns></returns>
        /// <exception cref="DataEncodingException"></exception>
        public static Dictionary<String, String> get_dictionary(String str)
        {
            Dictionary<String, String> dict = new Dictionary<String, String>();
            while (str.Length > 1)
            {
                Int32 start = str.IndexOf('<');
                Int32 end = str.IndexOf('>');
                if (start == end)
                    break;
                String tkey = str.Substring(start + 1, end - start - 1);
                str = str.Substring(end + 1);
                start = str.IndexOf('<');
                String tvalue = str.Substring(0, start);
                str = str.Substring(start);
                end = str.IndexOf('>');
                if (str.Substring(1, end - 1) != $"/{tkey}")
                    throw new DataEncodingException("Name Doesn't Match!");
                if (str.IndexOf('>') == str.LastIndexOf('>'))
                {
                    try
                    {
                        dict.Add(tkey, tvalue); break;
                    }
                    catch (ArgumentException) {; }
                }
                else
                    str = str.Substring(end + 1);
                try
                {
                    dict.Add(tkey, tvalue); break;
                }
                catch (ArgumentException) {; }
            }
            if (!dict.ContainsKey("identifier"))
                throw new DataEncodingException("Identifier Not Found!");
            return dict;
        }

        /// <summary>
        /// Get a dictionary from the data bytes.
        /// </summary>
        /// <param name="bytes">Data bytes.</param>
        /// <returns></returns>
        /// <exception cref="DataEncodingException"></exception>
        public static Dictionary<String, String> get_dictionary(Byte[] bytes)
        {
            return get_dictionary(Encoding.UTF8.GetString(bytes));
        }

        /// <summary>
        /// Get the data string from a dictionary.
        /// </summary>
        /// <param name="dict"></param>
        /// <returns></returns>
        /// <exception cref="DataEncodingException"></exception>
        public static String get_string(Dictionary<String, String> dict)
        {
            if (!dict.ContainsKey("identifier"))
                throw new DataEncodingException("Identifier Not Found!");
            String mystr = "";
            foreach (var pair in dict)
            {
                String tkey = pair.Key;
                String tvalue = pair.Value;
                mystr += "<" + tkey + ">" + @tvalue + "</" + tkey + ">";
            }
            return mystr;
        }

        /// <summary>
        /// Get the data bytes from a dictionary.
        /// </summary>
        /// <param name="dict"></param>
        /// <returns></returns>
        /// <exception cref="DataEncodingException"></exception>
        public static Byte[] get_bytes(Dictionary<String, String> dict)
        {
            return Encoding.UTF8.GetBytes(get_string(dict));
        }
    }

    public class DataEncodingException : ArgumentException
    {
        public DataEncodingException()
            : base()
        {; }

        public DataEncodingException(String message)
            : base(message)
        {; }
    }
}
